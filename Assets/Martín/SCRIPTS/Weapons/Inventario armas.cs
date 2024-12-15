using UnityEngine;
using TMPro;

public class InventarioArmas : MonoBehaviour
{
    public Arma[] armas; // Array de armas
    private int indiceArmaActual = 0; // Índice del arma equipada
    private Arma armaActual;

    public TextMeshProUGUI textoBalas; // Referencia al texto del Canvas

    void Start()
    {
        EquiparArma(indiceArmaActual); // Equipamos el arma inicial
    }

    void Update()
    {
        // Cambiar de arma
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquiparArma(0); // Cambiar a la primera arma
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquiparArma(1); // Cambiar a la segunda arma

        // Disparo
        if (Input.GetButtonDown("Fire1") && armaActual != null)
        {
            armaActual.ComenzarDisparo();
        }
        if (Input.GetButtonUp("Fire1") && armaActual != null)
        {
            armaActual.DetenerDisparo();
        }

        // Recargar
        if (Input.GetKeyDown(KeyCode.R) && armaActual != null)
        {
            armaActual.Recargar();
            ActualizarTextoBalas(); // Actualizamos la UI después de recargar
        }

        // Actualizar la UI cada frame
        ActualizarTextoBalas();
    }

    void EquiparArma(int indice)
    {
        if (indice < 0 || indice >= armas.Length) return;

        // Desactivar el arma anterior
        if (armaActual != null) armaActual.gameObject.SetActive(false);

        // Activar el arma nueva
        armaActual = armas[indice];
        armaActual.gameObject.SetActive(true);

        // Actualizamos el índice del arma actual
        indiceArmaActual = indice;

        // Actualizamos la UI inmediatamente
        ActualizarTextoBalas();
    }

    void ActualizarTextoBalas()
    {
        if (armaActual != null && textoBalas != null)
        {
            textoBalas.text = $"{armaActual.balasActuales} / {armaActual.balasPorCargador}";
        }
    }
}


