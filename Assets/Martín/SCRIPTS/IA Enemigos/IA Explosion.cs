using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAExplosion : MonoBehaviour
{
    public float radioDeDetecci�n;
    public LayerMask layer;
    public GameObject particulasPrefab;  // Prefab de las part�culas
    public float da�oJugador = 10f;  // Da�o que se le hace al jugador

    private bool explosionInstanciada = false;  // Para verificar si la explosi�n ya fue instanciada

    private void Update()
    {
        Detectar();
    }

    private void Detectar()
    {
        // Verificamos si el jugador est� dentro del rango de detecci�n
        if (Physics.CheckSphere(transform.position, radioDeDetecci�n, layer))
        {
            // Si las part�culas no han sido instanciadas
            if (!explosionInstanciada)
            {
                // Instanciar las part�culas
                GameObject particulas = Instantiate(particulasPrefab, transform.position, Quaternion.identity);

                // Obtener el sistema de part�culas y destruir el objeto despu�s de su duraci�n
                ParticleSystem ps = particulas.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    // Destruir despu�s de la duraci�n de las part�culas
                    float duracionParticulas = ps.main.duration;
                    Destroy(particulas, duracionParticulas);  // Destruir despu�s de la duraci�n de las part�culas
                }

                // Reproducir el sonido de la explosi�n
                AudioManager.AudioInstance.Play("Explosion");

                // Aplicar da�o al jugador (suponiendo que el jugador tenga un sistema de vida)
                AplicarDa�oAlJugador();

                // Destruir el enemigo
                Destroy(gameObject);  // Destruye el enemigo

                // Marcar que las part�culas han sido instanciadas
                explosionInstanciada = true;
            }
        }
        else
        {
            // Si el jugador sale del rango, permitir que las part�culas puedan ser instanciadas de nuevo
            explosionInstanciada = false;
        }
    }

    private void AplicarDa�oAlJugador()
    {
        // Detectamos al jugador en el rango de explosi�n y le aplicamos da�o
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radioDeDetecci�n, layer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player")) // Aseg�rate de que el jugador tenga el tag "Player"
            {
                // Usamos el m�todo QuitarVida que ya tienes en VidaJugador
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