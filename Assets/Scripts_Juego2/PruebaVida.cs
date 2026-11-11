
using UnityEngine;

public class PruebaVida : MonoBehaviour
{
    [SerializeField] private Combatiente jugador;
    [SerializeField] private Combatiente enemigo;

    private void Start()
    {
        Debug.Log("Iniciando prueba de vida");

        jugador.RecibirDanio(25);
        jugador.Curar(10);

        enemigo.RecibirDanio(40);
    }
}
