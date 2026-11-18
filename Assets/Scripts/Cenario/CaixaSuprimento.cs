using UnityEngine;
using TMPro;

public class CaixaSuprimento : MonoBehaviour
{
    public enum TipoSuprimento
    {
        MunicaoPistola,
        MunicaoRifle,
        Vida
    }

    [Header("Configuracao da Caixa")]
    public TipoSuprimento tipo = TipoSuprimento.MunicaoPistola;
    public int custoPontos = 30;
    public int quantidade = 24; 
    public float raioInteracao = 1f; // metros p poder fazer a compra

    [Header("Feedback Visual e Sonoro")]
    public TMP_Text textoInteracao; 
    public AudioClip somCompra;
    [Range(0f, 1f)] public float volume = 0.6f;

    private Transform playerTransform;
    private PlayerHealth playerHealth;
    private WeaponAmmo playerAmmo;
    private bool playerPerto = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            playerHealth = playerObj.GetComponent<PlayerHealth>();
            playerAmmo = playerObj.GetComponent<WeaponAmmo>();
        }

        if (textoInteracao != null)
        {
            textoInteracao.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float dist = Vector2.Distance(transform.position, playerTransform.position);

        if (dist <= raioInteracao)
        {
            if (!playerPerto)
            {
                playerPerto = true;
                AtualizarTexto(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                TentarComprar();
            }
        }
        else
        {
            if (playerPerto)
            {
                playerPerto = false;
                AtualizarTexto(false);
            }
        }
    }

    void TentarComprar()
    {
        if (HUD.pontos < custoPontos)
        {
            if (textoInteracao != null)
            {
                textoInteracao.text = "Pontos insuficientes!";
            }
            return;
        }

        if (tipo == TipoSuprimento.Vida && playerHealth != null && playerHealth.health >= playerHealth.maxHealth)
        {
            if (textoInteracao != null)
            {
                textoInteracao.text = "Vida já está cheia!";
            }
            return;
        }

        HUD.pontos -= custoPontos;

        if (tipo == TipoSuprimento.Vida && playerHealth != null)
        {
            playerHealth.Curar(quantidade);
        }
        else if (tipo == TipoSuprimento.MunicaoPistola && playerAmmo != null)
        {
            playerAmmo.AdicionarMunicao(0, quantidade); // 0 = pistola
        }
        else if (tipo == TipoSuprimento.MunicaoRifle && playerAmmo != null)
        {
            playerAmmo.AdicionarMunicao(1, quantidade); // 1 = fuzil
        }

        if (somCompra != null)
        {
            AudioSource.PlayClipAtPoint(somCompra, transform.position, volume);
        }

        AtualizarTexto(true);
    }

    void AtualizarTexto(bool visivel)
    {
        if (textoInteracao == null) return;

        if (!visivel)
        {
            textoInteracao.gameObject.SetActive(false);
            return;
        }

        string nomeItem = "";
        switch (tipo)
        {
            case TipoSuprimento.MunicaoPistola:
                nomeItem = $"+{quantidade} Balas Pistola";
                break;
            case TipoSuprimento.MunicaoRifle:
                nomeItem = $"+{quantidade} Balas Rifle";
                break;
            case TipoSuprimento.Vida:
                nomeItem = $"+{quantidade} Vida";
                break;
        }

        textoInteracao.text = $"Tecla [E] para comprar {nomeItem}, custo: {custoPontos} pts";
        textoInteracao.gameObject.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.yellow; //amarelo pra enxergar o alcance da caixa
        Gizmos.DrawWireSphere(transform.position, raioInteracao);
    }
}
