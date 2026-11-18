using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int vida = 3;
    public AudioClip somAcerto;
    public AudioClip somMorte;
    [Range(0f, 1f)] public float volume = 0.5f;

    public void LevarDano(int dano)
    {
        if (vida <= 0) return;

        vida -= dano;
        if (vida > 0)
        {
            Tocar(somAcerto);
        }
        else
        {
            Tocar(somMorte);
            HUD.pontos += 10;
            Destroy(gameObject);
        }
    }

    void Tocar(AudioClip clip)
    {
        if (clip != null) AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}