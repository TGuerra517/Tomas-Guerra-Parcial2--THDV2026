using UnityEngine;

public class PickupPensamiento : MonoBehaviour
{
    public DatosPensamiento pensamiento;
    public UIpensamiento interfaz;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interfaz.MostrarPensamiento(pensamiento);

            Destroy(gameObject);
        }
    }
}