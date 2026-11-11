
using System.Collections;
using UnityEngine;

public class FeedbackDanio : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Renderer renderPersonaje;

    [Header("Configuracion del parpadeo")]
    [SerializeField] private Color colorImpacto = Color.white;
    [SerializeField] private float duracion = 0.15f;

    private Coroutine parpadeoActual;
    private Sombra sombra;
    private Color colorOriginal;

    private void Awake()
    {
        sombra = GetComponent<Sombra>();

        if (renderPersonaje != null)
        {
            colorOriginal = renderPersonaje.material.color;
        }
    }

    public void MostrarImpacto()
    {
        if (renderPersonaje == null)
            return;

        if (parpadeoActual != null)
        {
            StopCoroutine(parpadeoActual);
        }

        parpadeoActual = StartCoroutine(Parpadear());
    }

    private IEnumerator Parpadear()
    {
        renderPersonaje.material.color = colorImpacto;

        yield return new WaitForSeconds(duracion);

        if (sombra != null)
        {
            switch (sombra.EstadoActual)
            {
                case EstadoSombra.Conviccion:
                    renderPersonaje.material.color = Color.red;
                    break;

                case EstadoSombra.Duda:
                    renderPersonaje.material.color = Color.magenta;
                    break;

                case EstadoSombra.Vulnerabilidad:
                    renderPersonaje.material.color = Color.cyan;
                    break;
            }
        }
        else
        {
            renderPersonaje.material.color = colorOriginal;
        }

        parpadeoActual = null;
    }
}

