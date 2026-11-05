using UnityEngine;
using TMPro;

public class UIpensamiento : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text textoTitulo;
    public TMP_Text textoContenido;

    bool mostrando = false;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (mostrando && Input.GetKeyDown(KeyCode.E))
        {
            CerrarPensamiento();
        }
    }

    public void MostrarPensamiento(DatosPensamiento datos)
    {
        textoTitulo.text = datos.titulo;
        textoContenido.text = datos.contenido;

        panel.SetActive(true);
        mostrando = true;

        Time.timeScale = 0f;
    }

    void CerrarPensamiento()
    {
        panel.SetActive(false);
        mostrando = false;

        Time.timeScale = 1f;
    }
}