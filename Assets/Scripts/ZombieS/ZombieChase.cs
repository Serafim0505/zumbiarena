using UnityEngine;
using System.Collections.Generic;

public class ZombieChase : MonoBehaviour
{
    public float speed = 2f;
    public float intervaloCaminho = 0.5f;
    public float limiteX = 40f;
    public float limiteY = 24f;

private Transform target;
    private Rigidbody2D rb;
    private List<Vector2> caminho = new List<Vector2>();
    private float timer;
    private float tempoRecuo = 0f;

    static readonly Vector2Int[] direcoes =
    {
    new Vector2Int(1, 0), new Vector2Int(-1, 0),
    new Vector2Int(0, 1), new Vector2Int(0, -1),
    new Vector2Int(1, 1), new Vector2Int(1, -1),
    new Vector2Int(-1, 1), new Vector2Int(-1, -1)
};

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        if (tempoRecuo > 0f)
        {
            tempoRecuo -= Time.fixedDeltaTime;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, 10f * Time.fixedDeltaTime);
            return;
        }

        timer -= Time.fixedDeltaTime;
        if (timer <= 0f)
        {
            caminho = Achar(rb.position, target.position);
            timer = intervaloCaminho;
        }

        Vector2 pos = rb.position;
        Vector2 alvo = Proximo(pos);

        Vector2 dir = alvo - pos;
        if (dir.magnitude > 0.01f)
        {
            dir = dir.normalized;
            rb.MovePosition(pos + dir * speed * Time.fixedDeltaTime);
            rb.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }
    }

    Vector2 Proximo(Vector2 pos)
    {
        while (caminho.Count > 0 && Vector2.Distance(pos, caminho[0]) < 0.2f)
            caminho.RemoveAt(0);

        if (caminho.Count > 0) return caminho[0];

        if (Vector2.Distance(pos, target.position) < 1f) return target.position;
        return pos;
    }

    List<Vector2> Achar(Vector2 inicio, Vector2 fim)
    {
        Vector2Int s = new Vector2Int(Mathf.FloorToInt(inicio.x), Mathf.FloorToInt(inicio.y));
        Vector2Int g = new Vector2Int(Mathf.FloorToInt(fim.x), Mathf.FloorToInt(fim.y));

        var veio = new Dictionary<Vector2Int, Vector2Int>();
        var fila = new Queue<Vector2Int>();
        fila.Enqueue(s);
        veio[s] = s;

        while (fila.Count > 0)
        {
            Vector2Int c = fila.Dequeue();
            if (c == g) break;

            foreach (var d in direcoes)
            {
                Vector2Int n = c + d;
                if (veio.ContainsKey(n)) continue;
                if (n != g && !Livre(n)) continue;

                if (d.x != 0 && d.y != 0)
                {
                    if (!Livre(new Vector2Int(c.x + d.x, c.y)) || !Livre(new Vector2Int(c.x, c.y + d.y)))
                        continue;
                }

                veio[n] = c;
                fila.Enqueue(n);
            }
        }

        Vector2Int destino = g;
        if (!veio.ContainsKey(g))
        {
            int melhorDist = int.MaxValue;
            foreach (var c in veio.Keys)
            {
                int dist = Mathf.Max(Mathf.Abs(c.x - g.x), Mathf.Abs(c.y - g.y));
                if (dist < melhorDist)
                {
                    melhorDist = dist;
                    destino = c;
                }
            }
        }

        var resultado = new List<Vector2>();
        Vector2Int atual = destino;
        while (atual != s)
        {
            resultado.Add(new Vector2(atual.x + 0.5f, atual.y + 0.5f));
            atual = veio[atual];
        }
        resultado.Reverse();
        return resultado;
    }

    bool Livre(Vector2Int c)
    {
        if (c.x < -limiteX || c.x >= limiteX || c.y < -limiteY || c.y >= limiteY) return false;

        Vector2 centro = new Vector2(c.x + 0.5f, c.y + 0.5f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(centro, new Vector2(0.8f, 0.8f), 0f);
        foreach (var h in hits)
        {
            if (h.isTrigger) continue;
            if (h.attachedRigidbody == rb) continue;
            if (h.CompareTag("Muro")) return false;
            if (h.transform.parent != null && h.transform.parent.CompareTag("Muro")) return false;
        }
        return true;
    }

    public void Empurrar(Vector2 direcao, float forca)
    {
        if (rb != null)
        {
            tempoRecuo = 0.18f; 
            rb.linearVelocity = direcao * forca; 
        }
    }
}