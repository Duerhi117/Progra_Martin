using UnityEngine;

public class Rifle : Arma
{
    public float velocidadDisparo = 0.2f;
    public GameObject balaPrefab;
    public float velocidadBala;

    private bool disparando = false;
    private float tiempoUltimoDisparo = 0f;

    void Update()
    {
        
        if (disparando && balasActuales > 0)
        {
            if (Time.time >= tiempoUltimoDisparo + velocidadDisparo)
            {
                Disparar();
                tiempoUltimoDisparo = Time.time;
            }
        }
    }

    public override void ComenzarDisparo()
    {
        disparando = true;
    }

    public override void DetenerDisparo()
    {
        disparando = false;
    }

    private void Disparar()
    {
        if (balasActuales > 0)
        {
            balasActuales--;

            AudioManager.AudioInstance.Play("Disparo");
            GameObject bala = Instantiate(balaPrefab, shootPoint.position, shootPoint.rotation);
            bala.GetComponent<Rigidbody>().AddForce(shootPoint.forward * velocidadBala, ForceMode.Impulse);
            Destroy(bala, 5f);

            Debug.Log($"Disparando automáticamente. Balas restantes: {balasActuales}");
        }
        else
        {
            Debug.Log("Sin balas. Recarga necesaria.");
            DetenerDisparo();
        }
    }

    public override void Recargar()
    {
        balasActuales = balasPorCargador;
        Debug.Log("Recargando arma automática.");
    }
}


