
using System.Collections;
using UnityEngine;

public class FeedbackDanio : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Renderer renderPersonaje;
    [SerializeField] private SpriteRenderer spritePersonaje;

    [Header("Configuracion del parpadeo")]
    [SerializeField] private Color colorImpacto = Color.white;
    [SerializeField] private float duracion = 0.15f;

    private Coroutine parpadeoActual;
    private Sombra sombra;
    private Color colorOriginal;

    private void Awake()
    {
        sombra = GetComponent<Sombra>();

        if (spritePersonaje != null)
        {
            colorOriginal = spritePersonaje.color;
        }
        else if (renderPersonaje != null)
        {
            colorOriginal = renderPersonaje.material.color;
        }
    }

    public void MostrarImpacto()
    {
        if (spritePersonaje == null && renderPersonaje == null)
            return;

        if (parpadeoActual != null)
        {
            StopCoroutine(parpadeoActual);
        }

        parpadeoActual = StartCoroutine(Parpadear());
    }

    private IEnumerator Parpadear()
    {
        CambiarColor(colorImpacto);

        yield return new WaitForSeconds(duracion);

        if (sombra != null)
        {
            CambiarColor(sombra.ObtenerColorActual());
        }
        else
        {
            CambiarColor(colorOriginal);
        }

        parpadeoActual = null;
    }

    private void CambiarColor(Color nuevoColor)
    {
        if (spritePersonaje != null)
        {
            spritePersonaje.color = nuevoColor;
        }
        else if (renderPersonaje != null)
        {
            renderPersonaje.material.color = nuevoColor;
        }
    }
}


