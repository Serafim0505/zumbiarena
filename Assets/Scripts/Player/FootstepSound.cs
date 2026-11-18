using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] sonsPasso;      
    public float intervalo = 0.4f;     // sseg entre um passo e outro
    [Range(0f, 1f)] public float volume = 0.4f;
    public float velocidadeMinima = 0.5f; 

    private Vector3 posAnterior;
    private float timer;
    private AudioSource audioSrc;

    void Start()
    {
        posAnterior = transform.position;
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        float velocidade = (transform.position - posAnterior).magnitude / Time.deltaTime;
        posAnterior = transform.position;

        if (velocidade < velocidadeMinima)
        {
            timer = 0f;
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            TocarPasso();
            timer = intervalo;
        }
    }

    void TocarPasso()
    {
        if (sonsPasso == null || sonsPasso.Length == 0 || audioSrc == null) return;
        AudioClip som = sonsPasso[Random.Range(0, sonsPasso.Length)];
        if (som != null) audioSrc.PlayOneShot(som, volume);
    }
}