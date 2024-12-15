using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryMessage;

    public void ShowVictoryMessage()
    {
        victoryMessage.SetActive(true);
        Debug.Log("¡Victoria! Has usado todos los objetos y completado el nivel.");
    }
}
