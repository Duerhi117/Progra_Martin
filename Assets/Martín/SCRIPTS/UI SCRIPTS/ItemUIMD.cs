using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Martín;

public class ItemUIMD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;
    [SerializeField] private Image itemImagen;

    public void SetItemInfo(SOItem item)
    {
        itemNombre.text = item.name;
        itemDescripcion.text = item.description;
        itemImagen.sprite = item.sprite;
    }

}
