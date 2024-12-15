using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAExplosion : MonoBehaviour
{
    public float radioDeDetecci�n;
    public LayerMask layer;
    private bool sonidoReproducido = false; // Bandera para asegurarse de que el sonido solo se reproduzca una vez

    private void Update()
    {
        Detectar();
    }

    private void Detectar()
    {
        // Comprobar si el jugador est� dentro del rango de detecci�n
        if (Physics.CheckSphere(transform.position, radioDeDetecci�n, layer))
        {
            if (!sonidoReproducido) // Reproducir el sonido solo si no ha sido reproducido a�n
            {
                AudioManager.AudioInstance.Play("Explosion");
                sonidoReproducido = true; // Establecer la bandera para evitar que el sonido se repita
            }
        }
        else
        {
            // Si el jugador sale del rango, permitir que el sonido se reproduzca nuevamente
            sonidoReproducido = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDetecci�n);
    }
}

