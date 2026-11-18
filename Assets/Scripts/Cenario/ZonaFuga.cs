using UnityEngine;

public class ZonaFuga : MonoBehaviour
{
    public AudioClip somVitoria;
    [Range(0f, 1f)] public float volume = 0.8f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (somVitoria != null)
            {
                AudioSource.PlayClipAtPoint(somVitoria, transform.position, volume);
            }

            GameManager gm = FindAnyObjectByType<GameManager>();
            if (gm != null)
            {
                gm.ShowVictory();
            }
        }
    }
}
