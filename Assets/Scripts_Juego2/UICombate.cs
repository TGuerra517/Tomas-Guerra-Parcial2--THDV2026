
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
    [SerializeField] private TMP_Text textoDanio;

    [Header("Botones de palabras")]
    [SerializeField] private Button botonRecordar;
    [SerializeField] private Button botonCuestionar;
    [SerializeField] private Button botonAceptar;
    [SerializeField] private Button botonPerdonar;
    [SerializeField] private Button botonAfirmar;

    [Header("Textos de usos restantes")]
    [SerializeField] private TMP_Text textoBotonAceptar;
    [SerializeField] private TMP_Text textoBotonPerdonar;

    [Header("Boton de dialogo")]
    [SerializeField] private Button botonContinuar;

    [Header("Boton de reinicio")]
    [SerializeField] private Button botonReiniciar;


    [Header("Colores del dialogo")]
    [SerializeField] private Image fondoDialogo;

    [Header("Colores del texto")]
    [SerializeField] private Color textoColorSombra = Color.white;
    [SerializeField] private Color textoColorProtagonista = Color.cyan;

    [SerializeField]
    private Color colorSombra =
        new Color(0.17f, 0.12f, 0.24f, 0.9f);

    [SerializeField]
    private Color colorProtagonista =
        new Color(0.10f, 0.20f, 0.25f, 0.9f);



    [Header("Datos de las palabras")]
    [SerializeField] private DatosPalabra recordarDatos;
    [SerializeField] private DatosPalabra cuestionarDatos;
    [SerializeField] private DatosPalabra aceptarDatos;
    [SerializeField] private DatosPalabra perdonarDatos;
    [SerializeField] private DatosPalabra afirmarDatos;

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
        textoDanio.gameObject.SetActive(false);

        ActualizarDatos();
        MostrarDialogo("SOMBRA", sombra.ObtenerDialogoActual());
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

        int vidaAnterior = enemigo.VidaActual;

        gestorCombate.UsarPalabra(tipo);

        int danioRealizado = vidaAnterior - enemigo.VidaActual;

        if (danioRealizado > 0)
        {
            textoDanio.text = "Daño recibido sombra: " + danioRealizado;
            textoDanio.gameObject.SetActive(true);
        }
        else
        {
            textoDanio.gameObject.SetActive(false);
        }

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

        MostrarDialogo("PROTAGONISTA", frase);

        esperandoDialogo = true;
        etapaDialogo = 0;

        ActivarBotonesPalabras(false);
        botonContinuar.gameObject.SetActive(true);

        ActualizarDatos();
    }


    private void MostrarDialogo(string personaje, string mensaje)
    {
        textoDialogo.text = personaje + ": " + mensaje;

        if (personaje == "SOMBRA")
        {
            fondoDialogo.color = colorSombra;
            textoDialogo.color = textoColorSombra;
        }
        else if (personaje == "PROTAGONISTA")
        {
            fondoDialogo.color = colorProtagonista;
            textoDialogo.color = textoColorProtagonista;
        }
    }



    private void ContinuarDialogo()
    {
        if (!esperandoDialogo)
            return;

        etapaDialogo++;

        if (etapaDialogo == 1)
        {
            MostrarDialogo("SOMBRA", respuestaSombra);
        }
        else if (etapaDialogo == 2)
        {
            if (!gestorCombate.CombateTerminado)
            {
                gestorCombate.TurnoEnemigo();

                MostrarDialogo("SOMBRA", "¡Todavía no terminamos!");
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
                    textoDialogo.text =
                        "¡VICTORIA! Has derrotado a la sombra.";
                }
                else
                {
                    textoDialogo.text =
                        "DERROTA. La sombra ha vencido.";
                }

                ActivarBotonesPalabras(false);

                botonReiniciar.gameObject.SetActive(true);
            }
            else
            {
                MostrarDialogo("SOMBRA", sombra.ObtenerDialogoActual());

                textoDanio.gameObject.SetActive(false);

                ActivarBotonesPalabras(true);
            }
        }
    }


    private void ActualizarDatos()
    {
        textoVida.text = jugador.VidaActual + " / " + jugador.VidaMaxima;
        barraVida.value = jugador.VidaActual;
        textoEstado.text = "Estado Sombra: " + sombra.EstadoActual.ToString();

        ActualizarUsos();
    }


    private void ActivarBotonesPalabras(bool activar)
    {
        botonRecordar.interactable = activar;
        botonCuestionar.interactable = activar;
        botonAfirmar.interactable = activar;

        botonAceptar.interactable =
            activar && gestorCombate.UsosAceptar > 0;

        botonPerdonar.interactable =
            activar && gestorCombate.UsosPerdonar > 0;
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




    private void ReiniciarCombate()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    private void ActualizarUsos()
    {
        textoBotonAceptar.text =
            "ACEPTAR\nUsos: " + gestorCombate.UsosAceptar + "/3";

        textoBotonPerdonar.text =
            "PERDONAR\nUsos: " + gestorCombate.UsosPerdonar + "/3";
    }




}
