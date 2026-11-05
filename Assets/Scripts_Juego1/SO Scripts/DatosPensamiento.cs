using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPensamiento", menuName = "Datos/Pensamiento")]
public class DatosPensamiento : ScriptableObject
{
    public string titulo;

    [TextArea(3, 6)]
    public string contenido;
}