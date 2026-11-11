
using UnityEngine;

public enum EstadoSombra
{
    Conviccion,
    Duda,
    Vulnerabilidad
}

public class Sombra : MonoBehaviour
{
    [Header("Afirmaciones disponibles")]
    [SerializeField] private DatosAfirmacion[] afirmaciones;

    private EstadoSombra estadoActual;
    private int indiceAfirmacion;

    [Header("Colores de la sombra")]
    [SerializeField] private Renderer renderSombra;

    [SerializeField] private Color colorConviccion = Color.red;
    [SerializeField] private Color colorDuda = Color.magenta;
    [SerializeField] private Color colorVulnerabilidad = Color.cyan;


    public EstadoSombra EstadoActual => estadoActual;

    public DatosAfirmacion AfirmacionActual
    {
        get
        {
            if (afirmaciones == null || afirmaciones.Length == 0)
                return null;

            return afirmaciones[indiceAfirmacion];
        }
    }

    private void Awake()
    {
        estadoActual = EstadoSombra.Conviccion;
        indiceAfirmacion = 0;
        ActualizarColor();
    }

    public void CambiarEstado(EstadoSombra nuevoEstado)
    {
        estadoActual = nuevoEstado;

        ActualizarColor();

        Debug.Log("La sombra cambió al estado: " + estadoActual);
    }

    public string ObtenerDialogoActual()
    {
        DatosAfirmacion afirmacion = AfirmacionActual;

        if (afirmacion == null)
            return "La sombra permanece en silencio.";

        switch (estadoActual)
        {
            case EstadoSombra.Conviccion:
                return afirmacion.dialogoConviccion;

            case EstadoSombra.Duda:
                return afirmacion.dialogoDuda;

            case EstadoSombra.Vulnerabilidad:
                return afirmacion.dialogoVulnerabilidad;

            default:
                return "";
        }
    }

    public void SiguienteAfirmacion()
    {
        if (afirmaciones == null || afirmaciones.Length == 0)
            return;

        indiceAfirmacion++;

        if (indiceAfirmacion >= afirmaciones.Length)
            indiceAfirmacion = 0;

        CambiarEstado(EstadoSombra.Conviccion);

        Debug.Log("Nueva afirmación: " + ObtenerDialogoActual());
    }


    private void ActualizarColor()
    {
        if (renderSombra == null)
            return;

        Color nuevoColor = Color.white;

        switch (estadoActual)
        {
            case EstadoSombra.Conviccion:
                nuevoColor = colorConviccion;
                break;

            case EstadoSombra.Duda:
                nuevoColor = colorDuda;
                break;

            case EstadoSombra.Vulnerabilidad:
                nuevoColor = colorVulnerabilidad;
                break;
        }

        renderSombra.material.color = nuevoColor;
    }




}

