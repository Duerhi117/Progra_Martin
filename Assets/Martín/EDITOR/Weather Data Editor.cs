using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

public class WeatherDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        WeatherData data = (WeatherData)target;

        EditorGUILayout.LabelField("Lista de Países", EditorStyles.boldLabel);

        for (int i = 0; i < data.paises.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            data.paises[i].nombre = EditorGUILayout.TextField("Nombre", data.paises[i].nombre);
            data.paises[i].latitud = EditorGUILayout.FloatField("Latitud", data.paises[i].latitud);
            data.paises[i].longitud = EditorGUILayout.FloatField("Longitud", data.paises[i].longitud);
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Agregar País"))
        {
            data.paises.Add(new Pais());
        }

        serializedObject.ApplyModifiedProperties();
    }
}
