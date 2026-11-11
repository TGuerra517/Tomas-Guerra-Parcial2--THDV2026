
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UICombate : MonoBehaviour
{
    [Header("Referencias del combate")]
    [SerializeField] private GestorCombate gestorCombate;
    [SerializeField] private Combatiente jugador;
    [SerializeField] private Combatiente enemigo;
    [SerializeField] private Sombra sombra;

    [Header("Interfaz del jugador")]
    [SerializeField] private TMP_Text textoVida;
    [SerializeField] private Slider barraVida;

    [Header("Interfaz de la sombra")]
    [SerializeField] private TMP_Text textoEstado;
    [SerializeField] private TMP_Text textoDialogo;

    [Header("Botones de palabras")]
    [SerializeField] private Button botonRecordar;
    [SerializeField] private Button botonCuestionar;
    [SerializeField] private Button botonAceptar;
    [SerializeField] private Button botonPerdonar;
    [SerializeField] private Button botonAfirmar;

    [Header("Boton de dialogo")]
    [SerializeField] private Button botonContinuar;

    [Header("Boton de reinicio")]
    [SerializeField] private Button botonReiniciar;

    private int etapaDialogo;
    private TipoPalabra ultimaPalabra;
    private bool palabraFueEfectiva;
    private string respuestaSombra;
    private bool esperandoDialogo;

    private void Start()
    {

        botonReiniciar.onClick.AddListener(ReiniciarCombate);
        botonReiniciar.gameObject.SetActive(false);



        barraVida.minValue = 0;
        barraVida.maxValue = jugador.VidaMaxima;

        botonRecordar.onClick.AddListener(() => SeleccionarPalabra(TipoPalabra.Recordar));
        botonCuestionar.onClick.AddListener(() => SeleccionarPalabra(TipoPalabra.Cuestionar));
        botonAceptar.onClick.AddListener(() => SeleccionarPalabra(TipoPalabra.Aceptar));
        botonPerdonar.onClick.AddListener(() => SeleccionarPalabra(TipoPalabra.Perdonar));
        botonAfirmar.onClick.AddListener(() => SeleccionarPalabra(TipoPalabra.Afirmar));

        botonContinuar.onClick.AddListener(ContinuarDialogo);

        botonContinuar.gameObject.SetActive(false);

        ActualizarDatos();
        textoDialogo.text = "SOMBRA: " + sombra.ObtenerDialogoActual();
    }

    private void SeleccionarPalabra(TipoPalabra tipo)
    {
        if (gestorCombate.CombateTerminado || esperandoDialogo)
            return;

        ultimaPalabra = tipo;

        DatosPalabra palabra = ObtenerDatosPalabra(tipo);

        if (palabra == null)
            return;

        DatosAfirmacion afirmacion = sombra.AfirmacionActual;

        palabraFueEfectiva = afirmacion != null &&
                            tipo == afirmacion.palabraEfectiva;

        if (tipo == TipoPalabra.Afirmar)
        {
            palabraFueEfectiva =
                sombra.EstadoActual == EstadoSombra.Vulnerabilidad;
        }

        gestorCombate.UsarPalabra(tipo);

        if (tipo == TipoPalabra.Recordar || tipo == TipoPalabra.Cuestionar)
        {
            if (palabraFueEfectiva)
                respuestaSombra = sombra.ObtenerDialogoActual();
            else
                respuestaSombra = afirmacion.dialogoResistencia;
        }
        else
        {
            respuestaSombra = sombra.ObtenerDialogoActual();
        }

        string frase = palabraFueEfectiva ?
                       palabra.fraseEfectiva :
                       palabra.fraseNormal;

        textoDialogo.text = "PROTAGONISTA: " + frase;

        esperandoDialogo = true;
        etapaDialogo = 0;

        ActivarBotonesPalabras(false);
        botonContinuar.gameObject.SetActive(true);

        ActualizarDatos();
    }

    private void ContinuarDialogo()
    {
        if (!esperandoDialogo)
            return;

        etapaDialogo++;

        if (etapaDialogo == 1)
        {
            textoDialogo.text = "SOMBRA: " + respuestaSombra;
        }
        else if (etapaDialogo == 2)
        {
            if (!gestorCombate.CombateTerminado)
            {
                gestorCombate.TurnoEnemigo();
                textoDialogo.text = "SOMBRA: ¡Todavía no terminamos!";
            }

            ActualizarDatos();
        }
        else
        {
            esperandoDialogo = false;
            botonContinuar.gameObject.SetActive(false);


            if (gestorCombate.CombateTerminado)
            {
                if (enemigo.EstaDerrotado)
                {
                    textoDialogo.text = "¡VICTORIA! Has derrotado a la sombra.";
                }
                else
                {
                    textoDialogo.text = "DERROTA. La sombra ha vencido.";
                }

                ActivarBotonesPalabras(false);

                botonReiniciar.gameObject.SetActive(true);
            }

            else
            {
                textoDialogo.text = "SOMBRA: " + sombra.ObtenerDialogoActual();
                ActivarBotonesPalabras(true);
            }
        }
    }

    private void ActualizarDatos()
    {
        textoVida.text = jugador.VidaActual + " / " + jugador.VidaMaxima;
        barraVida.value = jugador.VidaActual;
        textoEstado.text = sombra.EstadoActual.ToString();
    }

    private void ActivarBotonesPalabras(bool activar)
    {
        botonRecordar.interactable = activar;
        botonCuestionar.interactable = activar;
        botonAceptar.interactable = activar;
        botonPerdonar.interactable = activar;
        botonAfirmar.interactable = activar;
    }

    private DatosPalabra ObtenerDatosPalabra(TipoPalabra tipo)
    {
        switch (tipo)
        {
            case TipoPalabra.Recordar:
                return recordarDatos;

            case TipoPalabra.Cuestionar:
                return cuestionarDatos;

            case TipoPalabra.Aceptar:
                return aceptarDatos;

            case TipoPalabra.Perdonar:
                return perdonarDatos;

            case TipoPalabra.Afirmar:
                return afirmarDatos;

            default:
                return null;
        }
    }

    [Header("Datos de las palabras")]
    [SerializeField] private DatosPalabra recordarDatos;
    [SerializeField] private DatosPalabra cuestionarDatos;
    [SerializeField] private DatosPalabra aceptarDatos;
    [SerializeField] private DatosPalabra perdonarDatos;
    [SerializeField] private DatosPalabra afirmarDatos;



    private void ReiniciarCombate()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
