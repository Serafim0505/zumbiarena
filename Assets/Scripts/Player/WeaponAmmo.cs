using System.Collections;
using UnityEngine;
//using UnityEngine.InputSystem;
using TMPro;

public class WeaponAmmo : MonoBehaviour
{
    [System.Serializable]
    public class Arma
    {
        public string nome;
        public int tamanhoCarregador = 8;
        public int reservaInicial = 24;   
        public float cadencia = 3f;      
        public bool automatica = false;  
        public Sprite desenho;          
        public Transform pontaArma;     
        public GameObject bala;         
        [HideInInspector] public int noCarregador;
        [HideInInspector] public int reserva;
    }

    public Arma[] armas = new Arma[2];    // 0 = pistola, 1 = fuzil
    public SpriteRenderer desenhoArma;  
    public TextMeshProUGUI textoMunicao;  
    public bool modoInfinito = true;      
    public float tempoRecarga = 1.5f;
    public AudioClip somRecarga;  
    AudioSource audioSrc;

    int atual = 0;
    public Arma Atual => armas[atual];
    bool recarregando = false;

    

    void Start()

    {
        audioSrc = GetComponent<AudioSource>();
        foreach (var a in armas)
        {
            a.noCarregador = a.tamanhoCarregador;
            a.reserva = a.reservaInicial;
            AplicarVisual();
        }
        Atualizar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Trocar(1); 
        if (Input.GetKeyDown(KeyCode.Alpha2)) Trocar(0); 
        if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(Recarregar());
    }

    void Trocar(int index)
    {
        if (index == atual) return;
        recarregando = false;   // p cancelar recarregamento ao trocar
        StopAllCoroutines();
        atual = index;
        AplicarVisual();
        Atualizar();
    }

    
    public bool TentarAtirar()
    {
        if (recarregando) return false;
        Arma a = armas[atual];
        if (a.noCarregador <= 0)
        {
            StartCoroutine(Recarregar());
            return false;
        }
        a.noCarregador--;
        Atualizar();
        return true;
    }

    IEnumerator Recarregar()
    {
        
            Arma a = armas[atual];
            if (recarregando || a.noCarregador == a.tamanhoCarregador) yield break;

            int falta = a.tamanhoCarregador - a.noCarregador;
            if (!modoInfinito && a.reserva <= 0) yield break;

            recarregando = true;
            textoMunicao.text = "Recarregando...";

            if (somRecarga != null && audioSrc != null)
                audioSrc.PlayOneShot(somRecarga);

            yield return new WaitForSeconds(tempoRecarga);

            if (modoInfinito)
            {
                a.noCarregador = a.tamanhoCarregador;
            }
            else
            {
                int usar = Mathf.Min(falta, a.reserva);
                a.reserva -= usar;
                a.noCarregador += usar;
            }

            recarregando = false;
            Atualizar();
        
    }

    void Atualizar()
    {
        Arma a = armas[atual];
        string reservaTexto = modoInfinito ? "∞" : a.reserva.ToString();
        textoMunicao.text = $"{a.nome}\n{a.noCarregador} / {reservaTexto}";
    }

    void AplicarVisual()
    {
        if (desenhoArma != null && Atual.desenho != null)
            desenhoArma.sprite = Atual.desenho;
    }

    public void AdicionarMunicao(int indexArma, int quantidade)
    {
        if (indexArma >= 0 && indexArma < armas.Length)
        {
            armas[indexArma].reserva += quantidade;
            Atualizar();
        }
    }
}
