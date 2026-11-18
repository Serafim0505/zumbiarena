using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TMP_Text vidaText;
    public TMP_Text ondaText;
    public TMP_Text pontosText;
    public UnityEngine.UI.Slider barraVida;

    public static int pontos;

    private PlayerHealth player;
    private ZombieSpawner spawner;

    void Start()
    {
        pontos = 0;
        player = FindAnyObjectByType<PlayerHealth>();
        spawner = FindAnyObjectByType<ZombieSpawner>();
    }

    void Update()
    {
        if (player != null)
        {
            vidaText.text = "Vida: " + player.health;
            barraVida.value = player.health;
        }
        if (spawner != null) ondaText.text = "Onda: " + spawner.round;
        pontosText.text = "Pontos: " + pontos;
    }
    
    
}