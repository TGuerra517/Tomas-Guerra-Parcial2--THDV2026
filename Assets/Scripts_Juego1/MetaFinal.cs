
using UnityEngine;

public class MetaFinal : MonoBehaviour
{
    public DatosPensamiento mensajeVictoria;
    public UIpensamiento interfaz;

    bool completado = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !completado)
        {
            completado = true;

            interfaz.MostrarVictoria(mensajeVictoria);
        }
    }
}

