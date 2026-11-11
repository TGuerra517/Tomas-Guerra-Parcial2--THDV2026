
using UnityEngine;

public enum TipoPalabra
{
    Recordar,
    Cuestionar,
    Aceptar,
    Perdonar,
    Afirmar
}

[CreateAssetMenu(fileName = "NuevaPalabra", menuName = "Duelo Interior/Palabra")]
public class DatosPalabra : ScriptableObject
{
    [Header("Información de la palabra")]
    public string nombrePalabra;

    [TextArea(2, 4)]
    public string descripcion;

    [Header("Dialogos del protagonista")]

    [TextArea(2, 4)]
    public string fraseNormal;

    [TextArea(2, 4)]
    public string fraseEfectiva;

    public TipoPalabra tipoPalabra;

    [Header("Valores de combate")]
    public int danioBase;
    public int curacion;
    public int defensa;
}
