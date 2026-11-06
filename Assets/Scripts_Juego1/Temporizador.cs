
using UnityEngine;
using TMPro;

public class Temporizador : MonoBehaviour
{
    public float tiempoInicial = 120f;

    public TMP_Text textoTiempo;
    public DatosPensamiento mensajeDerrota;
    public UIpensamiento interfaz;

    float tiempoRestante;

    void Start()
    {
        tiempoRestante = tiempoInicial;
    }

    void Update()
    {
        if (interfaz.JuegoTerminado())
        {
            return;
        }

        tiempoRestante -= Time.deltaTime;

        tiempoRestante = Mathf.Max(tiempoRestante, 0f);

        int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        int segundos = Mathf.CeilToInt(tiempoRestante % 60);

        if (segundos == 60)
        {
            minutos++;
            segundos = 0;
        }

        textoTiempo.text = string.Format(
            "{0:00}:{1:00}", minutos, segundos
        );

        if (tiempoRestante <= 0f)
        {
            interfaz.MostrarDerrota(mensajeDerrota);
        }
    }
}

