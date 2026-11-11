
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
    }

    public void CambiarEstado(EstadoSombra nuevoEstado)
    {
        estadoActual = nuevoEstado;

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
}

