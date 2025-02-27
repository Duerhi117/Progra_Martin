using UnityEngine;


namespace Profe.Door
{
    [System.Serializable]
    // Tipos de puerta: Automatica, Normal, DeLlave, Evento, MultiplesLlaves
    public class Door : MonoBehaviour, IInteractable
    {
        public TipoDePuerta tipoDePuerta;

        //Evento
        public bool eventoActivado;

        // Llave
        public SOItem key;
        public bool showKeysNames;

        // MultiplesLlaves
        public SOItem[] keys;
        public SOItem[] swords;

        private InventoryHandler inventoryHandler;

        private void Awake()
        {
            inventoryHandler = FindObjectOfType<InventoryHandler>();
        }
        private void Update()
        {
            if (EnemigoMuerte.enemigosVencidos == EnemigoMuerte.enemigosTotales)
            {
                eventoActivado = true;
            }
        }

        public void Interact()
        {

            switch (tipoDePuerta)
            {
                case TipoDePuerta.Automatica:
                    {
                        Debug.Log("Se abre automaticamente");
                        break;
                    }

                case TipoDePuerta.Normal:
                    {
                        Normal();
                        Debug.Log("Se abre");
                        break;
                    }

                case TipoDePuerta.DeLlave:
                    {
                        DeLlave();
                        Debug.Log("Se abre con llave");
                        break;
                    }

                case TipoDePuerta.Evento:
                    {
                        Evento();

                        break;
                    }

                case TipoDePuerta.MultiplesLlaves:
                    {
                        MultiplesLlaves();
                        Debug.Log("Se abre con multiples llaves");
                        break;
                    }
            }


        }

        private void Normal()
        {
            Destroy(gameObject);
        }

        private void DeLlave()
        {
            if (inventoryHandler.inventory.Contains(key))
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("No tienes la llave");
            }
        }

        private void MultiplesLlaves()
        {
            if (keys.Length == 0)
            {
                Debug.Log("No hay llaves asignadas a esta puerta.");
                return;
            }

            bool tieneTodasLasLlaves = true;

            foreach (SOItem llave in keys)
            {
                if (!inventoryHandler.inventory.Contains(llave))
                {
                    tieneTodasLasLlaves = false;
                    break;
                }
            }

            if (tieneTodasLasLlaves)
            {
                Debug.Log("Has recolectado todas las llaves necesarias. La puerta se abre.");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Aún te faltan llaves para abrir esta puerta.");
            }
        }

        private void Evento()
        {
            if (eventoActivado == true)
            {
                Debug.Log("Puerta abierta");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("El evento aún no pasa");
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (tipoDePuerta == TipoDePuerta.Automatica && other.CompareTag("Player"))
            {
                Debug.Log("Se abrio automaticamente.");
                Destroy(gameObject);
            }
        }



        [ContextMenu("Show|Hide Keys Names")]
        public void ToggleBool()
        {
            showKeysNames = !showKeysNames;
        }

    }


    public enum TipoDePuerta // Son para crear tipos/clasificaciones/estados
    {
        Automatica, Normal, DeLlave, Evento, MultiplesLlaves
    }
}