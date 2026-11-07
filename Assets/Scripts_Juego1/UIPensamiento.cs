


using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIpensamiento : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text textoTitulo;
    public TMP_Text textoContenido;
    public GameObject textoContinuar;

    bool mostrando = false;
    bool juegoTerminado = false;

    DatosPensamiento[] mensajesInicio;
    int mensajeActual;

    void Awake()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (!mostrando)
        {
            return;
        }

        if (juegoTerminado)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().buildIndex
                );
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (mensajesInicio != null)
            {
                mensajeActual++;

                if (mensajeActual < mensajesInicio.Length)
                {
                    ActualizarTexto(mensajesInicio[mensajeActual]);
                }
                else
                {
                    mensajesInicio = null;
                    CerrarPensamiento();
                }
            }
            else
            {
                CerrarPensamiento();
            }
        }
    }

    void ActualizarTexto(DatosPensamiento datos)
    {
        textoTitulo.text = datos.titulo;
        textoContenido.text = datos.contenido;
    }

    public void MostrarIntroduccion(DatosPensamiento[] mensajes)
    {
        if (mostrando || juegoTerminado ||
            mensajes == null || mensajes.Length == 0)
        {
            return;
        }

        mensajesInicio = mensajes;
        mensajeActual = 0;

        ActualizarTexto(mensajesInicio[0]);

        textoContinuar.SetActive(true);
        panel.SetActive(true);

        mostrando = true;
        Time.timeScale = 0f;
    }

    public void MostrarPensamiento(DatosPensamiento datos)
    {
        if (mostrando || juegoTerminado)
        {
            return;
        }

        ActualizarTexto(datos);

        textoContinuar.SetActive(true);
        panel.SetActive(true);

        mostrando = true;
        Time.timeScale = 0f;
    }

    public void MostrarVictoria(DatosPensamiento datos)
    {
        MostrarFinal(datos);
    }

    public void MostrarDerrota(DatosPensamiento datos)
    {
        MostrarFinal(datos);
    }

    void MostrarFinal(DatosPensamiento datos)
    {
        if (mostrando || juegoTerminado)
        {
            return;
        }

        juegoTerminado = true;
        mostrando = true;

        ActualizarTexto(datos);

        textoContinuar.SetActive(false);
        panel.SetActive(true);

        Time.timeScale = 0f;
    }

    void CerrarPensamiento()
    {
        panel.SetActive(false);
        mostrando = false;

        Time.timeScale = 1f;
    }

    public bool JuegoTerminado()
    {
        return juegoTerminado;
    }

    public bool EstaMostrandoMensaje()
    {
        return mostrando;
    }
}


