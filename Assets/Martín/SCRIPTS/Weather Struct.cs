using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pais
{
    public string nombre;
    public float latitud;
    public float longitud;
}

[CreateAssetMenu(fileName = "WeatherData", menuName = "Clima/WeatherData")]
public class WeatherData : ScriptableObject
{
    public List<Pais> paises = new List<Pais>(); // Lista de países en el inspector
    public string timeZone;
    public float latitud;
    public float longitud;
    public float actualTemp;
    public string description;
    public float windSpeed;
}
