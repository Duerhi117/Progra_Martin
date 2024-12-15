using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAExplosion : MonoBehaviour
{
    public float radioDeDetección;
    public LayerMask layer;


    private void Update()
    {
        Detectar();
    }

    private void Detectar()
    {
        if (Physics.CheckSphere(transform.position, radioDeDetección, layer))
        {
            AudioManager.AudioInstance.Play("Explosion");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDetección);
    }
}
