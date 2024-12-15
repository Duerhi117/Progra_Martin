using UnityEngine;
using UnityEngine.UI;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vida = 100;

    public Vector3 puntoDeInicio;

    public Image barraDeVida;

    void Start()
    {
        puntoDeInicio = transform.position;

        ActualizarBarraDeVida();
    }

    // Método para reducir la vida
    public void QuitarVida(int cantidad)
    {
        vida -= cantidad;
        vida = Mathf.Clamp(vida, 0, vidaMaxima);
        Debug.Log("Vida restante: " + vida);


        ActualizarBarraDeVida();


        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Debug.Log("¡El jugador ha muerto!");
        Reaparecer();
    }

    private void Reaparecer()
    {
        vida = vidaMaxima;

        transform.position = puntoDeInicio;

        ActualizarBarraDeVida();
    }

    private void ActualizarBarraDeVida()
    {
        if (barraDeVida != null)
        {
            barraDeVida.fillAmount = (float)vida / vidaMaxima;
        }
    }
}