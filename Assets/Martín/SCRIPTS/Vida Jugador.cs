using UnityEngine;
using UnityEngine.UI; // Necesario para usar elementos de UI

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 100;  // Vida máxima del jugador
    public int vida = 100;        // Vida actual del jugador

    public Vector3 puntoDeInicio; // Posición inicial del jugador

    public Image barraDeVida;     // Referencia a la imagen del Canvas (tipo Filled)

    void Start()
    {
        // Al inicio, establecemos el punto de inicio en la posición actual del jugador
        puntoDeInicio = transform.position;

        // Aseguramos que la barra de vida esté sincronizada al inicio
        ActualizarBarraDeVida();
    }

    // Método para reducir la vida
    public void QuitarVida(int cantidad)
    {
        vida -= cantidad;
        vida = Mathf.Clamp(vida, 0, vidaMaxima); // Nos aseguramos de que no baje de 0 ni pase de la vida máxima
        Debug.Log("Vida restante: " + vida);

        // Actualizar barra de vida
        ActualizarBarraDeVida();

        // Si la vida llega a 0, manejar la muerte del jugador aquí
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        // Lógica para cuando el jugador muere
        Debug.Log("¡El jugador ha muerto!");
        Reaparecer();
    }

    private void Reaparecer()
    {
        // Restauramos la vida del jugador
        vida = vidaMaxima;

        // Mover al jugador al punto de inicio
        transform.position = puntoDeInicio;

        // Actualizamos la barra de vida al máximo
        ActualizarBarraDeVida();
    }

    private void ActualizarBarraDeVida()
    {
        if (barraDeVida != null)
        {
            // Calculamos el porcentaje de vida
            barraDeVida.fillAmount = (float)vida / vidaMaxima;
        }
    }
}