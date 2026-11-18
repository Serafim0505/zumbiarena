using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float spawnInterval = 1.5f;
    public int zombiesPerRound = 6;
    public float roundPause = 3f;
    public float spawnDistance = 7f; 

    public int round = 1;

    [Header("Modo Fuga (Desafio)")]
    public bool modoFuga = false;
    public int roundFinal = 10;
    public GameObject objetoPorta;
    public TMPro.TMP_Text textoAviso;

    private int toSpawn;
    private float timer;
    private float pauseTimer;
    private bool pausing;
    private bool portaAberta = false;

    void Start()
    {
        toSpawn = QuantidadeDoRound();
    }

    void Update()
    {
        if (pausing)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0f)
            {
                pausing = false;
                round++;
                toSpawn = QuantidadeDoRound();
            }
            return;
        }

        if (toSpawn > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Spawn();
                toSpawn--;
                timer = IntervaloDoRound();
            }
        }
        else if (GameObject.FindGameObjectsWithTag("Zombie").Length == 0)
        {
            if (modoFuga && round >= roundFinal)
            {
                if (!portaAberta)
                {
                    portaAberta = true;
                    if (objetoPorta != null) objetoPorta.SetActive(false);
                    if (textoAviso != null)
                    {
                        textoAviso.text = "A PORTA SE ABRIU! FUJA PELA SAIDA!";
                        textoAviso.gameObject.SetActive(true);
                        StartCoroutine(EsconderAviso(2.5f));
                    }
                }
                return; 
            }

            pausing = true;
            pauseTimer = roundPause;
        }
    }

    System.Collections.IEnumerator EsconderAviso(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        if (textoAviso != null)
        {
            textoAviso.gameObject.SetActive(false);
        }
    }

    void Spawn()
    {
        Vector2 dir = Random.insideUnitCircle.normalized;
        if (dir == Vector2.zero) dir = Vector2.up;
        Vector2 pos = (Vector2)transform.position + dir * spawnDistance;

        GameObject z = Instantiate(zombiePrefab, pos, Quaternion.identity);

        ZombieHealth vidaZ = z.GetComponent<ZombieHealth>();
        if (vidaZ != null) vidaZ.vida = VidaDoRound();

        ZombieChase chase = z.GetComponent<ZombieChase>();
        if (chase != null) chase.speed = VelocidadeDoRound();
    }

    int QuantidadeDoRound()
    {
        return zombiesPerRound + (round - 1) * 4;
    }

    float IntervaloDoRound()
    {
        return Mathf.Max(0.4f, spawnInterval - (round - 1) * 0.12f);
    }

    int VidaDoRound()
    {
        int vida = 2 + (round - 1) / 2;
        return Mathf.Min(vida, 6);
    }

    float VelocidadeDoRound()
    {
        float vel = 2.2f + (round - 1) * 0.3f;
        return Mathf.Min(vel, 4f);
    }
}