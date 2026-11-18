using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    Vector3 posOriginal;
    float duracaoRestante;
    float intensidade;

    void Awake()
    {
        instance = this;
    }

    void LateUpdate()
    {
        if (duracaoRestante > 0f)
        {
            transform.localPosition = posOriginal + (Vector3)Random.insideUnitCircle * intensidade;
            duracaoRestante -= Time.deltaTime;
            if (duracaoRestante <= 0f) transform.localPosition = posOriginal;
        }
        else
        {
            posOriginal = transform.localPosition;
        }
    }

    public void Tremer(float duracao = 0.15f, float forca = 0.2f)
    {
        duracaoRestante = duracao;
        intensidade = forca;
    }
}