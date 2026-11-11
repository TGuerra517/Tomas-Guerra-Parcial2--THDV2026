
using UnityEngine;

public class PruebaSombra : MonoBehaviour
{
    [SerializeField] private Sombra sombra;

    private void Start()
    {
        Debug.Log("Estado inicial: " + sombra.EstadoActual);
        Debug.Log(sombra.ObtenerDialogoActual());

        sombra.CambiarEstado(EstadoSombra.Duda);
        Debug.Log(sombra.ObtenerDialogoActual());

        sombra.CambiarEstado(EstadoSombra.Vulnerabilidad);
        Debug.Log(sombra.ObtenerDialogoActual());

        sombra.SiguienteAfirmacion();
        Debug.Log(sombra.ObtenerDialogoActual());
    }
}
