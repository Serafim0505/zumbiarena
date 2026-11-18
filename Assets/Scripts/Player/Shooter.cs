using UnityEngine;
using static WeaponAmmo;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletOffset = 0.5f;
    public Transform firePoint; 
    public AudioClip somTiro;

    MuzzleFlash flash;
    AudioSource audioSrc;
    WeaponAmmo municao;
    float proximoTiro;

    void Awake()
    {
        if (firePoint != null) flash = firePoint.GetComponent<MuzzleFlash>();
        audioSrc = GetComponent<AudioSource>();
        municao = GetComponent<WeaponAmmo>();
    }

    void Update()
    {
        if (GameManager.isGameOver || Time.timeScale == 0f) return;

        Arma arma = municao != null ? municao.Atual : null;
        bool automatica = arma != null && arma.automatica;
        bool pediu = automatica ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);

        if (pediu && Time.time >= proximoTiro)
        {
            proximoTiro = Time.time + (arma != null ? 1f / arma.cadencia : 0f);
            Shoot();
        }
    }

    void Shoot()
    {
        
        if (municao != null && !municao.TentarAtirar()) return;

        Transform ponta = (municao != null && municao.Atual.pontaArma != null) ? municao.Atual.pontaArma : firePoint;
        GameObject prefab = (municao != null && municao.Atual.bala != null) ? municao.Atual.bala : bulletPrefab;

        Vector3 spawn = ponta != null ? ponta.position : transform.position + transform.right * bulletOffset;
        Quaternion rot = ponta != null ? ponta.rotation : transform.rotation;
        Instantiate(prefab, spawn, rot);

        Vector2 direcao = ponta != null ? (Vector2)ponta.right : (Vector2)transform.right;
        if (flash != null) flash.Disparar(direcao, spawn);

        if (somTiro != null && audioSrc != null) audioSrc.PlayOneShot(somTiro, 0.2f);
    }
}