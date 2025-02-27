using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMuerte : MonoBehaviour
{
    public static int enemigosVencidos = 0;
    public static int enemigosTotales = 45;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            enemigosVencidos ++;
            Debug.Log("1 Enemigo Vencido");
            Debug.Log("Disparo Recibido");
            AudioManager.AudioInstance.Play("Eliminación");
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
