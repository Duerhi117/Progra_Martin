using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public float rangoXMin;
    public float rangoXMax;
    public float rangoZMin;
    public float rangoZMax;
    public float intervaloSpawn;

    private void Start()
    {
        StartCoroutine(GenerarEnemigos());
    }

    private IEnumerator GenerarEnemigos()
    {
        while (true)
        {
            float lugarEnX = Random.Range(rangoXMin, rangoXMax);
            float lugarEnZ = Random.Range(rangoZMin, rangoZMax);
            Vector3 posicionEnemigo = new Vector3(lugarEnX, 5f, lugarEnZ);

            Instantiate(enemigoPrefab, posicionEnemigo, Quaternion.identity);

            yield return new WaitForSeconds(intervaloSpawn);
        }
    }

  
}

