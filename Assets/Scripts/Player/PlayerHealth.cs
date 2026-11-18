using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int damagePerHit = 10;
    public float hitCooldown = 1f;
    public AudioClip[] sonsDano;  

    public int health;
    private float cooldownTimer;
    private bool dead;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (cooldownTimer > 0f) cooldownTimer -= Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (dead || cooldownTimer > 0f) return;
        if (collision.gameObject.CompareTag("Zombie"))
        {
            health -= damagePerHit;
            cooldownTimer = hitCooldown;
            Debug.Log("Vida: " + health);

            if (sonsDano != null && sonsDano.Length > 0)
            {
                AudioClip som = sonsDano[Random.Range(0, sonsDano.Length)];
                if (som != null) AudioSource.PlayClipAtPoint(som, transform.position, 1f);
            }
            if (CameraShake.instance != null) CameraShake.instance.Tremer();

            if (health <= 0) Die();
        }
    }

    void Die()
    {
        dead = true;
        Debug.Log("GAME OVER");
        FindAnyObjectByType<GameManager>().ShowGameOver();
    }

    public void Curar(int valor)
    {
        if (dead) return;
        health = Mathf.Min(maxHealth, health + valor);
    }
}