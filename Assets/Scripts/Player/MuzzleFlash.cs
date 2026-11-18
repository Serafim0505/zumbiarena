using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public int particulas = 12;
    public float velocidade = 8f;
    public float duracao = 0.12f;
    public Color cor = new Color(1f, 0.85f, 0.3f);

    ParticleSystem ps;

    void Awake()
    {
        ps = gameObject.AddComponent<ParticleSystem>();

        var main = ps.main;
        main.playOnAwake = false;
        main.loop = false;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.maxParticles = particulas * 3;

        var emission = ps.emission;
        emission.enabled = false;

        var rend = GetComponent<ParticleSystemRenderer>();
        rend.material = new Material(Shader.Find("Sprites/Default"));
        rend.sortingOrder = 10;
    }

    public void Disparar(Vector2 direcao, Vector3 origem)
    {
        for (int i = 0; i < particulas; i++)
        {
            Vector2 d = (Vector2)(Quaternion.Euler(0, 0, Random.Range(-20f, 20f)) * (Vector3)direcao.normalized);

            var p = new ParticleSystem.EmitParams();
            p.position = origem;
            p.velocity = d * Random.Range(velocidade * 0.6f, velocidade);
            p.startLifetime = duracao;
            p.startSize = Random.Range(0.08f, 0.18f);
            p.startColor = cor;
            ps.Emit(p, 1);
        }
    }
}