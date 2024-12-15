using UnityEngine;
using TMPro;

public class InventarioArmas : MonoBehaviour
{
    public Arma[] armas;
    private int indiceArmaActual = 0;
    private Arma armaActual;

    public TextMeshProUGUI textoBalas;

    void Start()
    {
        EquiparArma(indiceArmaActual);
    }

    void Update()
    {
        // Cambiar de arma
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquiparArma(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquiparArma(1); 

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
            ActualizarTextoBalas();
        }


        ActualizarTextoBalas();
    }

    void EquiparArma(int indice)
    {
        if (indice < 0 || indice >= armas.Length) return;

        if (armaActual != null) armaActual.gameObject.SetActive(false);

        armaActual = armas[indice];
        armaActual.gameObject.SetActive(true);

        indiceArmaActual = indice;

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


