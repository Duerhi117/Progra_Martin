using System;
using UnityEngine;



/// <summary>
/// 
/// NECESITAMOS
/// Tener paginas en el inventario, cada pagina se conforma por 8 items como maximo
/// Conforme se agregan items al inventario, se deben de ir desbloqueando paginas
///
/// 
/// </summary>
/// 

namespace Profe
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryCanvas;
        [SerializeField] private GameObject uiItemPrefab;
        [SerializeField] private GameObject displayArea;
        [SerializeField] private Page[] pages = new Page[3]; // 24 items

        public int actualPage = 0;
        private int maxItemsPerPage = 2;

        private InventoryHandler inventoryRef;

        public bool inventoryOpened = false;

        private void Awake()
        {          
            inventoryRef = FindAnyObjectByType<InventoryHandler>();

            for(int i= 0; i<pages.Length; i++)
            {
                pages[i].items = new GameObject[maxItemsPerPage];
                pages[i].itemsDeployed = 0;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I)) // Abrir inventario
            {
                OpenInventory();
            }
        }

        private void OpenInventory()
        {
            inventoryOpened = !inventoryOpened;
            inventoryCanvas.SetActive(inventoryOpened);

            if (inventoryOpened)
            {
                // Liberar el cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                // Bloquear el cursor nuevamente
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (inventoryRef.inventory.Count <= 0) // Revisa si hay cosas en el inventario
            {
                // Si no hay nada, aqu� termina
                return;
            }
            else
            {
                // Resto del c�digo de OpenInventory (manejo de p�ginas, �tems, etc.)
                HideAllItems();
                ShowItems(actualPage);
            }
        }





        [ContextMenu("Show Items in Page")]
        private void ShowItems()
        {
            for (int i = 0; i < pages[actualPage].itemsDeployed; i++)
            {
                pages[actualPage].items[i].SetActive(true);
            }
        }

        [ContextMenu("Hide Items in Page")]
        private void HideItems()
        {
            for (int i = 0; i < pages[actualPage].itemsDeployed; i++)
            {
                pages[actualPage].items[i].SetActive(false);
            }
        }

        // Este metodo ahorita me lo guardo para cuando tenga el boton de cambiar pagina
        private void ShowItems(int page)
        {
            for (int i = 0; i < pages[page].itemsDeployed; i++)
            {
                pages[page].items[i].SetActive(true);
            }
        }

        // Este metodo ahorita me lo guardo para cuando tenga el boton de cambiar pagina
        private void HideItems(int page)
        {
            for (int i = 0; i < pages[page].itemsDeployed; i++)
            {
                pages[page].items[i].SetActive(false);
            }
        }

        [ContextMenu("Hide All Items")]
        private void HideAllItems()
        {
            for(int page = 0; page <= actualPage; page++) // Este for recorre las paginas
            {
                Debug.Log(page);
                for(int item = 0; item < pages[page].itemsDeployed; item++)
                {
                    Debug.Log(item);
                    pages[page].items[item].SetActive(false);
                }
                Debug.Log("Siguiente pagina");
            }
        }

        public void NextPage()
        {
            // Asegurarte de no exceder las p�ginas disponibles
            if (actualPage < pages.Length - 1 && pages[actualPage + 1].itemsDeployed > 0)
            {
                HideItems(actualPage); // Oculta los �tems de la p�gina actual
                actualPage++;          // Cambia a la siguiente p�gina
                ShowItems(actualPage); // Muestra los �tems de la nueva p�gina
            }
            else
            {
                Debug.Log("No hay m�s p�ginas disponibles.");
            }
        }

        public void PreviousPage()
        {
            // Aseg�rate de no ir m�s atr�s de la primera p�gina
            if (actualPage > 0)
            {
                HideItems(actualPage); // Oculta los �tems de la p�gina actual
                actualPage--;          // Cambia a la p�gina anterior
                ShowItems(actualPage); // Muestra los �tems de la nueva p�gina
            }
            else
            {
                Debug.Log("Ya est�s en la primera p�gina.");
            }
        }

    }

    [Serializable]
    public struct Page
    {
        public int itemsDeployed;
        public GameObject[] items; // en este arreglo me guarda los 8 items que pertenecen a esa pagina
    }

}



