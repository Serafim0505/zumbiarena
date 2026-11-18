using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float fps = 8f;
    public Rigidbody2D corpo; 

    SpriteRenderer sr;
    float timer;
    int atual;
    Vector2 ultimaPos;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (corpo != null) ultimaPos = corpo.position;
    }

    void Update()
    {
        if (frames.Length == 0 || corpo == null) return;

        Vector2 agora = corpo.position;
        bool andando = (agora - ultimaPos).sqrMagnitude > 0.0001f;
        ultimaPos = agora;

        if (!andando)
        {
            sr.sprite = frames[0];
            return;
        }

        timer += Time.deltaTime;
        if (timer >= 1f / fps)
        {
            timer = 0f;
            atual = (atual + 1) % frames.Length;
            sr.sprite = frames[atual];
        }
    }
}