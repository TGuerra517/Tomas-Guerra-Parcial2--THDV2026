
using UnityEngine;

public class SalirDelJuego : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Salir();
        }
    }

    void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}

