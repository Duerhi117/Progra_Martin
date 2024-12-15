using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAExplosion : MonoBehaviour
{
    public float radioDeDetección;
    public LayerMask layer;
    public GameObject particulasPrefab;
    public float dañoJugador = 10f;

    private bool explosionInstanciada = false;

    private void Update()
    {
        Detectar();
    }

    private void Detectar()
    {
        if (Physics.CheckSphere(transform.position, radioDeDetección, layer))
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

                AplicarDañoAlJugador();

                Destroy(gameObject);

                explosionInstanciada = true;
            }
        }
        else
        {

            explosionInstanciada = false;
        }
    }

    private void AplicarDañoAlJugador()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radioDeDetección, layer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {

                hitCollider.GetComponent<VidaJugador>().QuitarVida((int)dañoJugador);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDetección);
    }
}