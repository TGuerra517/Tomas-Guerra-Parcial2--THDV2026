

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

    void Start()
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
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            CerrarPensamiento();
        }
    }

    public void MostrarPensamiento(DatosPensamiento datos)
    {
        if (mostrando || juegoTerminado)
        {
            return;
        }

        textoTitulo.text = datos.titulo;
        textoContenido.text = datos.contenido;

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

        textoTitulo.text = datos.titulo;
        textoContenido.text = datos.contenido;

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
}

