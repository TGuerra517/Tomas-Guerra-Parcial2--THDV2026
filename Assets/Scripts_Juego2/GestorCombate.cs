
using UnityEngine;

public class GestorCombate : MonoBehaviour
{
    [Header("Participantes")]
    [SerializeField] private Combatiente jugador;
    [SerializeField] private Combatiente enemigo;
    [SerializeField] private Sombra sombra;

    [Header("Palabras disponibles")]
    [SerializeField] private DatosPalabra recordar;
    [SerializeField] private DatosPalabra cuestionar;
    [SerializeField] private DatosPalabra aceptar;
    [SerializeField] private DatosPalabra perdonar;
    [SerializeField] private DatosPalabra afirmar;

    [Header("Configuracion del enemigo")]
    [SerializeField] private int danioEnemigo = 15;

    private bool combateTerminado;

    private int defensaPendiente;

    private void Start()
    {
        combateTerminado = false;

        Debug.Log("¡Comienza el combate!");
        Debug.Log("La sombra dice: " + sombra.ObtenerDialogoActual());
    }


    public void UsarPalabra(TipoPalabra tipo)
    {
        if (combateTerminado)
            return;

        DatosPalabra palabra = ObtenerPalabra(tipo);

        if (palabra == null)
        {
            Debug.LogWarning("No se encontró la palabra seleccionada.");
            return;
        }

        defensaPendiente = 0;

        switch (tipo)
        {
            case TipoPalabra.Recordar:
            case TipoPalabra.Cuestionar:
                UsarPalabraOfensiva(palabra);
                break;

            case TipoPalabra.Aceptar:
                defensaPendiente = palabra.defensa;
                Debug.Log("El jugador se prepara para resistir.");
                break;

            case TipoPalabra.Perdonar:
                jugador.Curar(palabra.curacion);
                break;

            case TipoPalabra.Afirmar:
                UsarAfirmar(palabra);
                break;
        }

        if (enemigo.EstaDerrotado)
        {
            combateTerminado = true;
            Debug.Log("¡VICTORIA! La sombra fue derrotada.");
        }
    }


    private DatosPalabra ObtenerPalabra(TipoPalabra tipo)
    {
        switch (tipo)
        {
            case TipoPalabra.Recordar:
                return recordar;

            case TipoPalabra.Cuestionar:
                return cuestionar;

            case TipoPalabra.Aceptar:
                return aceptar;

            case TipoPalabra.Perdonar:
                return perdonar;

            case TipoPalabra.Afirmar:
                return afirmar;

            default:
                return null;
        }
    }

    private void UsarPalabraOfensiva(DatosPalabra palabra)
    {
        DatosAfirmacion afirmacion = sombra.AfirmacionActual;

        if (afirmacion == null)
        {
            Debug.LogWarning("La sombra no tiene una afirmación asignada.");
            return;
        }

        bool esEfectiva = palabra.tipoPalabra == afirmacion.palabraEfectiva;

        int danio = palabra.danioBase;

        if (esEfectiva)
        {
            danio *= 2;

            if (sombra.EstadoActual == EstadoSombra.Conviccion)
            {
                sombra.CambiarEstado(EstadoSombra.Duda);
            }
            else if (sombra.EstadoActual == EstadoSombra.Duda)
            {
                sombra.CambiarEstado(EstadoSombra.Vulnerabilidad);
            }

            Debug.Log("¡La palabra fue efectiva!");
        }
        else
        {
            Debug.Log("La palabra no logró debilitar a la sombra.");
        }

        enemigo.RecibirDanio(danio);

        Debug.Log("La sombra dice: " + sombra.ObtenerDialogoActual());
    }

    private void UsarAfirmar(DatosPalabra palabra)
    {
        int danio = palabra.danioBase;

        if (sombra.EstadoActual == EstadoSombra.Duda)
        {
            danio *= 2;
        }
        else if (sombra.EstadoActual == EstadoSombra.Vulnerabilidad)
        {
            danio *= 3;
        }

        enemigo.RecibirDanio(danio);

        Debug.Log("El jugador utiliza AFIRMAR.");

        if (sombra.EstadoActual == EstadoSombra.Vulnerabilidad &&
            !enemigo.EstaDerrotado)
        {
            sombra.SiguienteAfirmacion();
        }
    }


    public void TurnoEnemigo()
    {
        if (combateTerminado)
            return;

        int danioFinal = Mathf.Max(0, danioEnemigo - defensaPendiente);

        jugador.RecibirDanio(danioFinal);

        defensaPendiente = 0;

        Debug.Log("La sombra ataca al jugador.");

        if (jugador.EstaDerrotado)
        {
            combateTerminado = true;
            Debug.Log("DERROTA. El protagonista perdió el combate.");
        }
    }



    [ContextMenu("Probar RECORDAR")]
    public void ProbarRecordar()
    {
        UsarPalabra(TipoPalabra.Recordar);
    }

    [ContextMenu("Probar CUESTIONAR")]
    public void ProbarCuestionar()
    {
        UsarPalabra(TipoPalabra.Cuestionar);
    }

    [ContextMenu("Probar ACEPTAR")]
    public void ProbarAceptar()
    {
        UsarPalabra(TipoPalabra.Aceptar);
    }

    [ContextMenu("Probar PERDONAR")]
    public void ProbarPerdonar()
    {
        UsarPalabra(TipoPalabra.Perdonar);
    }

    [ContextMenu("Probar AFIRMAR")]
    public void ProbarAfirmar()
    {
        UsarPalabra(TipoPalabra.Afirmar);
    }


    public bool CombateTerminado => combateTerminado;
}

