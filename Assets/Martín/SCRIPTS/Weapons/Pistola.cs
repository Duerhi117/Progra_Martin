using UnityEngine;

public class Pistola : Arma
{
    public GameObject balaPrefab;
    public float velocidadBala;

    public override void ComenzarDisparo()
    {
        if (balasActuales > 0)
        {
            balasActuales--;

            AudioManager.AudioInstance.Play("Disparo");
            GameObject bala = Instantiate(balaPrefab, shootPoint.position, shootPoint.rotation);
            bala.GetComponent<Rigidbody>().AddForce(shootPoint.forward * velocidadBala);
            Destroy(bala, 5f);
            Debug.Log("Disparo semiautomático. Balas restantes: " + balasActuales);
        }
        else
        {
            Debug.Log("Sin balas. Recarga necesaria.");
        }
    }

    public override void DetenerDisparo()
    {
    }

    public override void Recargar()
    {
        balasActuales = balasPorCargador;
        Debug.Log("Recargando arma semiautomática.");
    }
}

