using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAExplosion : MonoBehaviour
{
    public float radioDeDetecci�n;
    public LayerMask layer;
    public GameObject particulasPrefab;
    public float da�oJugador = 10f;

    private bool explosionInstanciada = false;

    private void Update()
    {
        Detectar();
    }

    private void Detectar()
    {
        if (Physics.CheckSphere(transform.position, radioDeDetecci�n, layer))
        {
            if (!explosionInstanciada)
            {
                GameObject particulas = Instantiate(particulasPrefab, transform.position, Quaternion.identity);

                ParticleSystem ps = particulas.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    float duracionParticulas = ps.main.duration;
                    Destroy(particulas, duracionParticulas);
                }

                AudioManager.AudioInstance.Play("Explosion");

                AplicarDa�oAlJugador();

                Destroy(gameObject);

                explosionInstanciada = true;
            }
        }
        else
        {

            explosionInstanciada = false;
        }
    }

    private void AplicarDa�oAlJugador()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radioDeDetecci�n, layer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {

                hitCollider.GetComponent<VidaJugador>().QuitarVida((int)da�oJugador);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDetecci�n);
    }
}