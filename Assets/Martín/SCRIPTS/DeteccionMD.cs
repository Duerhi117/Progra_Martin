using System.Collections;
using UnityEngine;

namespace Mart�n
{
    public class Deteccion : MonoBehaviour
    {
        public float radioDeDetecci�n;
        public LayerMask layer;

        private Patrullaje patrullador;
        private PerseguirObjetivo perseguir;
        private bool objetivoDetectado = false;

        private void Start()
        {
            patrullador = GetComponent<Patrullaje>();
            perseguir = GetComponent<PerseguirObjetivo>();
            StartCoroutine(Detectar());
        }

        private IEnumerator Detectar()
        {
            while (true)
            {
                if (Physics.CheckSphere(transform.position, radioDeDetecci�n, layer))
                {
                    if (!objetivoDetectado)
                    {
                        objetivoDetectado = true;
                        patrullador.DejarDePatrullar();
                        perseguir.Perseguir();
                        Debug.Log("Jugador Detectado");
                    }
                }
                else
                {
                    if (objetivoDetectado)
                    {
                        objetivoDetectado = false;
                        perseguir.DejarPersecusion();
                        patrullador.RegresarUltimoPunto();
                        Debug.Log("Se perdi� de vista al intruso");
                    }
                }
                yield return null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radioDeDetecci�n);
        }
    }
}

