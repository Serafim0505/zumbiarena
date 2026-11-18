using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 2f;
    public int dano = 1;

    [Header("Configuracao de Empurrao")]
    public bool ehRifle = false;
    public float forcaPistola = 6f;
    public float distanciaCurtaPistola = 3f;

    public float forcaMaximaRifle = 12f;
    public float forcaMinimaRifle = 1f;
    public float alcanceEfetivoRifle = 8f;

    public AudioClip somParede;
    public AudioClip somAcerto;  
    [Range(0f, 1f)] public float volume = 0.5f;

    private Vector2 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
        
    {
        Debug.Log("A bala bateu em: " + other.name);
        if (other.CompareTag("Muro"))
        {
            if (somParede != null) AudioSource.PlayClipAtPoint(somParede, transform.position, volume);
            Destroy(gameObject);
        }
        else
        {
            ZombieHealth zumbi = other.GetComponentInParent<ZombieHealth>();
            if (zumbi != null)
            {
                zumbi.LevarDano(dano);

                // calular a distanca percorrida pela bala pra dps ver se ela vai empurrar o zumbi
                float dist = Vector2.Distance(posicaoInicial, transform.position);
                float forcaFinal = 0f;

                if (!ehRifle)
                {
                    // a pistola so empurra em curta distancia
                    if (dist <= distanciaCurtaPistola)
                    {
                        forcaFinal = forcaPistola;
                    }
                }
                else
                {
                    // o rifle perde forca de forma quadratica, De perto tem impacto maximo, em media distancia ja perde boa parte da forca
                  
                    float fator = Mathf.Clamp01(1f - (dist / alcanceEfetivoRifle));
                    fator = fator * fator; 
                    forcaFinal = Mathf.Lerp(forcaMinimaRifle, forcaMaximaRifle, fator);
                }

                ZombieChase chase = other.GetComponentInParent<ZombieChase>();
                if (chase != null && forcaFinal > 0f)
                {
                    Vector2 direcaoTiro = transform.right;
                    chase.Empurrar(direcaoTiro, forcaFinal);
                }

                if (somAcerto != null) AudioSource.PlayClipAtPoint(somAcerto, transform.position, volume);
                Destroy(gameObject);
            }
        }
    }
    
}