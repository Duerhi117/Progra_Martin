using UnityEngine;

public class Rifle : Arma
{
    public float velocidadDisparo = 0.2f; // Tiempo entre disparos
    public GameObject balaPrefab;
    public float velocidadBala;

    private bool disparando = false; // Control de disparo continuo
    private float tiempoUltimoDisparo = 0f; // Control de tiempo entre disparos

    void Update()
    {
        // Si el jugador está disparando y hay balas disponibles
        if (disparando && balasActuales > 0)
        {
            if (Time.time >= tiempoUltimoDisparo + velocidadDisparo) // Controla el tiempo entre disparos
            {
                Disparar();
                tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del último disparo
            }
        }
    }

    public override void ComenzarDisparo()
    {
        disparando = true; // Marca el inicio del disparo continuo
    }

    public override void DetenerDisparo()
    {
        disparando = false; // Marca el final del disparo continuo
    }

    private void Disparar()
    {
        if (balasActuales > 0)
        {
            balasActuales--;

            AudioManager.AudioInstance.Play("Disparo");
            GameObject bala = Instantiate(balaPrefab, shootPoint.position, shootPoint.rotation);
            bala.GetComponent<Rigidbody>().AddForce(shootPoint.forward * velocidadBala, ForceMode.Impulse);
            Destroy(bala, 5f); // Destruye la bala después de 5 segundos

            Debug.Log($"Disparando automáticamente. Balas restantes: {balasActuales}");
        }
        else
        {
            Debug.Log("Sin balas. Recarga necesaria.");
            DetenerDisparo(); // Detenemos el disparo automático si no hay balas
        }
    }

    public override void Recargar()
    {
        balasActuales = balasPorCargador; // Recarga el cargador completo
        Debug.Log("Recargando arma automática.");
    }
}


