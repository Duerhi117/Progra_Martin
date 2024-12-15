using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAExplosion : MonoBehaviour
{
    public float radioDeDetección;
    public LayerMask layer;
    public GameObject particulasPrefab;  // Prefab de las partículas
    public float dañoJugador = 10f;  // Daño que se le hace al jugador

    private bool explosionInstanciada = false;  // Para verificar si la explosión ya fue instanciada

    private void Update()
    {
        Detectar();
    }

    private void Detectar()
    {
        // Verificamos si el jugador está dentro del rango de detección
        if (Physics.CheckSphere(transform.position, radioDeDetección, layer))
        {
            // Si las partículas no han sido instanciadas
            if (!explosionInstanciada)
            {
                // Instanciar las partículas
                GameObject particulas = Instantiate(particulasPrefab, transform.position, Quaternion.identity);

                // Obtener el sistema de partículas y destruir el objeto después de su duración
                ParticleSystem ps = particulas.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    // Destruir después de la duración de las partículas
                    float duracionParticulas = ps.main.duration;
                    Destroy(particulas, duracionParticulas);  // Destruir después de la duración de las partículas
                }

                // Reproducir el sonido de la explosión
                AudioManager.AudioInstance.Play("Explosion");

                // Aplicar daño al jugador (suponiendo que el jugador tenga un sistema de vida)
                AplicarDañoAlJugador();

                // Destruir el enemigo
                Destroy(gameObject);  // Destruye el enemigo

                // Marcar que las partículas han sido instanciadas
                explosionInstanciada = true;
            }
        }
        else
        {
            // Si el jugador sale del rango, permitir que las partículas puedan ser instanciadas de nuevo
            explosionInstanciada = false;
        }
    }

    private void AplicarDañoAlJugador()
    {
        // Detectamos al jugador en el rango de explosión y le aplicamos daño
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radioDeDetección, layer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
            {
                // Usamos el método QuitarVida que ya tienes en VidaJugador
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