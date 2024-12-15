using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAExplosion : MonoBehaviour
{
    public float radioDeDetecci�n;
    public LayerMask layer;


    private void Update()
    {
        Detectar();
    }

    private void Detectar()
    {
        if (Physics.CheckSphere(transform.position, radioDeDetecci�n, layer))
        {
            AudioManager.AudioInstance.Play("Explosion");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDetecci�n);
    }
}
