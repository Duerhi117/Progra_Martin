using System.Collections;
using UnityEngine;

namespace Martín
{
    public class Deteccion : MonoBehaviour
    {
        public float radioDeDetección;
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
                if (Physics.CheckSphere(transform.position, radioDeDetección, layer))
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
                        Debug.Log("Se perdió de vista al intruso");
                    }
                }
                yield return null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radioDeDetección);
        }
    }
}

