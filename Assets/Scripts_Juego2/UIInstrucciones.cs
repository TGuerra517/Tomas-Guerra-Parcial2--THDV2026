

using UnityEngine;
using UnityEngine.UI;

public class UIInstrucciones : MonoBehaviour
{
    [Header("Pantalla inicial")]
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private Button botonComenzar;

    [Header("Guia de palabras")]
    [SerializeField] private GameObject panelGuiaPalabras;

    [SerializeField] private GameObject textoAyudaTAB;

    [Header("Interfaz del combate")]
    [SerializeField] private GameObject panelPlayer;
    [SerializeField] private GameObject panelEnemigo;
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private GameObject panelAcciones;

    [Header("Secuencia narrativa")]
    [SerializeField] private UICombate uiCombate;

    private bool combateIniciado = false;

    private void Start()
    {
        // Ocultamos la interfaz del combate.
        panelPlayer.SetActive(false);
        panelEnemigo.SetActive(false);
        panelDialogo.SetActive(false);
        panelAcciones.SetActive(false);

        textoAyudaTAB.SetActive(false);

        // Mostramos el tutorial inicial.
        panelTutorial.SetActive(true);

        // La guia comienza oculta.
        panelGuiaPalabras.SetActive(false);

        // Configuramos el boton para comenzar.
        botonComenzar.onClick.AddListener(ComenzarCombate);
    }

    private void Update()
    {
        // No permitimos abrir la guia antes de comenzar.
        if (!combateIniciado)
            return;

        // La guia aparece mientras TAB esta presionado.
        if (Input.GetKey(KeyCode.Tab))
        {
            panelGuiaPalabras.SetActive(true);
        }
        else
        {
            panelGuiaPalabras.SetActive(false);
        }
    }

    private void ComenzarCombate()
    {
        panelTutorial.SetActive(false);

        textoAyudaTAB.SetActive(true);

        panelPlayer.SetActive(true);
        panelEnemigo.SetActive(true);
        panelDialogo.SetActive(true);
        panelAcciones.SetActive(true);

        combateIniciado = true;

        uiCombate.IniciarPrologo();

        Debug.Log("El prologo ha comenzado.");
    }
}

