using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float suavidade = 8f; // suavidade pra seguir o sobrevivente

    void LateUpdate()
    {
        Vector3 alvo = new Vector3(target.position.x, target.position.y, -10f);
        float t = 1f - Mathf.Exp(-suavidade * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, alvo, t);
    }
}