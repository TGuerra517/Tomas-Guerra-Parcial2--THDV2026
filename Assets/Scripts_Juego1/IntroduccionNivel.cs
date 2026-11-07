
using UnityEngine;

public class IntroduccionNivel : MonoBehaviour
{
    public DatosPensamiento introduccion;
    public DatosPensamiento controles;
    public UIpensamiento interfaz;

    void Start()
    {
        DatosPensamiento[] mensajes =
        {
            introduccion,
            controles
        };

        interfaz.MostrarIntroduccion(mensajes);
    }
}

