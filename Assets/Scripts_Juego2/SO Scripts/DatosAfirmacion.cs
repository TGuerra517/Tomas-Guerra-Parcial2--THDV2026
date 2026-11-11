
using UnityEngine;

public enum TipoAfirmacion
{
    Absoluta,
    Desvalorizacion
}

[CreateAssetMenu(fileName = "NuevaAfirmacion", menuName = "Duelo Interior/Afirmacion")]
public class DatosAfirmacion : ScriptableObject
{
    [Header("Tipo de afirmacion")]
    public TipoAfirmacion tipoAfirmacion;

    [Header("Dialogos de la sombra")]

    [TextArea(2, 4)]
    public string dialogoConviccion;

    [TextArea(2, 4)]
    public string dialogoDuda;

    [TextArea(2, 4)]
    public string dialogoVulnerabilidad;

    [Header("Respuesta ante una palabra ineficaz")]

    [TextArea(2, 4)]
    public string dialogoResistencia;

    [Header("Palabra mas efectiva")]
    public TipoPalabra palabraEfectiva;
}
