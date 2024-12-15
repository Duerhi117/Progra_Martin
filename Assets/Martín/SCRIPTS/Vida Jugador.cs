using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int vida = 100;  // Vida del jugador

    public Vector3 puntoDeInicio;

    void Start()
    {
        // Al inicio, establecemos el punto de inicio en la posición actual del jugador
        puntoDeInicio = transform.position;
    }

    // Método para reducir la vida
    public void QuitarVida(int cantidad)
    {
        vida -= cantidad;
        Debug.Log("Vida restante: " + vida);

        // Si la vida llega a 0, puedes manejar la muerte del jugador aquí
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        // Lógica para cuando el jugador muere, como mostrar una pantalla de "Game Over"
        Debug.Log("¡El jugador ha muerto!");

        Reaparecer();
    }

    private void Reaparecer()
    {
        // Restauramos la vida del jugador (si es necesario)
        vida = 100; // O el valor que consideres

        // Mover al jugador al punto de inicio
        transform.position = puntoDeInicio;
    }
}

