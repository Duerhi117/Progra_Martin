using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puerta : MonoBehaviour
{
    [SerializeField] private bool entrarPuerta;
    [SerializeField] private bool mouseIn;
    [SerializeField] private float radio;
    [SerializeField] private LayerMask jugadorMascara;

    private void OnMouseEnter()
    {
        mouseIn = true;
    }
    private void OnMouseExit()
    {
        mouseIn = false;
    }

    private void Update()
    {
        entrarPuerta = Physics.CheckSphere(transform.position, radio, jugadorMascara);


        if (Input.GetKeyDown(KeyCode.E) && mouseIn && entrarPuerta && EnemigoMuerte.enemigosVencidos >= EnemigoMuerte.enemigosTotales)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Fin del juego");
            AudioManager.AudioInstance.Play("Victoria");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
