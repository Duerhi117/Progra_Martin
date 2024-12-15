using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMuerte : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            Debug.Log("Disparo Recibido");
            AudioManager.AudioInstance.Play("Eliminación");
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
