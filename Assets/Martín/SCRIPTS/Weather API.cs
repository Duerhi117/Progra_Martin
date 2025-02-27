using System.Collections;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using static System.Net.WebRequestMethods;
using TMPro;

public class WeatherAPI : MonoBehaviour
{
    [SerializeField] WeatherData data;
    [SerializeField] private float latitud = 19.03793f;
    [SerializeField] private float longitud = -98.20346f;
    private static readonly string apiKey = "0aae971fbc582c2a9593c2d7cbb0e9af";

    private string url;

    private string json;

    [SerializeField] private VolumeProfile volumeProfile;

    [SerializeField] private float bloomColorTransitionSpeed;
    private Color actualColor;

    [SerializeField] private TextMeshProUGUI hudMessage;

    [SerializeField] private float exposureTransitionSpeed = 2f;
    private Exposure exposure;
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        if (volumeProfile.TryGet(out exposure))
        {
            Debug.Log("Exposure encontrado");
        }
        if (volumeProfile.TryGet(out colorAdjustments))
        {
            Debug.Log("Color Adjustments encontrado");
        }
        StartCoroutine(WeatherLoop());
        url = $"https://api.openweathermap.org/data/3.0/onecall?lat={latitud}&lon={longitud}&appid={apiKey}&lang=sp&units=metric";
        StartCoroutine(RetrieveWeatherData());

    }

    IEnumerator WeatherLoop()
    {
        int index = 0;
        while (true)
        {
            SetCountry(index);
            yield return StartCoroutine(RetrieveWeatherData());
            yield return new WaitForSecondsRealtime(90);
            index = (index + 1) % data.paises.Count;
        }
    }

    private void SetCountry(int index)
    {
        Pais pais = data.paises[index];
        latitud = pais.latitud;
        longitud = pais.longitud;
        url = $"https://api.openweathermap.org/data/3.0/onecall?lat={latitud}&lon={longitud}&appid={apiKey}&lang=sp&units=metric";

        if (hudMessage != null)
        {
            hudMessage.text = $"Escenario de {pais.nombre}";
        }

        Debug.Log($"Escenario de {pais.nombre} iniciado");
    }
    public void ChangeExposure(float newValue)
    {
        if (exposure != null)
        {
            exposure.fixedExposure.value = newValue;
        }
    }

    public void ChangeColorAdjustments(float saturation, float contrast)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value = saturation;
            colorAdjustments.contrast.value = contrast;
        }
    }

    private void ApplyWeatherEffects(float temperature)
    {
        if (temperature < 8)
        {
            ChangeExposure(-1f);
            ChangeColorAdjustments(-50f, -10f);
        }
        else if (temperature >= 8 && temperature < 24)
        {
            ChangeExposure(0f);
            ChangeColorAdjustments(0f, 0f);
        }
        else if (temperature >= 24 && temperature < 45)
        {
            ChangeExposure(1f);
            ChangeColorAdjustments(20f, 15f);
        }
        else if (temperature >= 45)
        {
            ChangeExposure(2f);
            ChangeColorAdjustments(50f, 30f);
        }
    }


    IEnumerator RetrieveWeatherData()
    {
        yield return new WaitForSecondsRealtime(5);

        UnityWebRequest request = new UnityWebRequest(url);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }

        else
        {
            Debug.Log(request.downloadHandler.text);

            json = request.downloadHandler.text;

            DecodeJson();

            yield return new WaitForSeconds(2);

            actualColor = GetColorByTemp();

            StartCoroutine(BloomColorTransition());
        }
    }

    private IEnumerator BloomColorTransition()
    {
        yield return new WaitUntil(() => TransitionColor() == actualColor);
        Debug.Log("Color Cambiado");
        while (true)
        {
            volumeProfile.TryGet(out Bloom bloom);
            bloom.tint.value = Color.Lerp(bloom.tint.value, actualColor, bloomColorTransitionSpeed * Time.deltaTime);

            exposure.fixedExposure.value = Mathf.Lerp(exposure.fixedExposure.value, data.actualTemp > 25 ? 1.5f : 0.5f, exposureTransitionSpeed * Time.deltaTime);
            colorAdjustments.saturation.value = Mathf.Lerp(colorAdjustments.saturation.value, data.actualTemp < 10 ? -20f : 10f, exposureTransitionSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private Color TransitionColor()
    {
        volumeProfile.TryGet(out Bloom bloom);
        bloom.tint.value = Color.Lerp(bloom.tint.value, actualColor, bloomColorTransitionSpeed * Time.deltaTime);

        return bloom.tint.value;
    }

    private Color GetColorByTemp()
    {
        switch (data.actualTemp)
        {
            case var color when data.actualTemp <= 8:
                {
                    actualColor = Color.cyan;
                    return actualColor;
                }

            case var color when data.actualTemp > 8 && data.actualTemp < 24:
                {
                    actualColor = new Color(176, 154, 0);
                    return actualColor;
                }

            case var color when data.actualTemp > 24 && data.actualTemp < 45:
                {
                    actualColor = new Color(255, 179, 0);
                    return actualColor;
                }

            case var color when data.actualTemp >= 45:
                {
                    actualColor = Color.red;
                    return actualColor;
                }

            default:
                {
                    return actualColor;
                }
        }
    }


    private void DecodeJson()
    {
        var weatherJson = JSON.Parse(json);

        data.timeZone = weatherJson["timezone"].Value;
        data.latitud = float.Parse(weatherJson["lat"].Value);
        data.longitud = float.Parse(weatherJson["lon"].Value);
        data.actualTemp = float.Parse(weatherJson["current"]["temp"].Value);
        data.description = weatherJson["current"]["weather"][0]["description"].Value;
        data.windSpeed = float.Parse(weatherJson["current"]["wind_speed"].Value);
    }
}