
using UnityEngine;

public class Combatiente : MonoBehaviour
{
    [Header("Vida del combatiente")]
    [SerializeField] private int vidaMaxima = 100;

    private int vidaActual;

    public int VidaActual => vidaActual;
    public int VidaMaxima => vidaMaxima;

    public bool EstaDerrotado => vidaActual <= 0;

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDanio(int cantidad)
    {
        if (EstaDerrotado || cantidad <= 0)
            return;

        vidaActual -= cantidad;
        vidaActual = Mathf.Max(vidaActual, 0);

        Debug.Log($"{gameObject.name} recibió {cantidad} de daño. Vida: {vidaActual}/{vidaMaxima}");
    }

    public void Curar(int cantidad)
    {
        if (EstaDerrotado || cantidad <= 0)
            return;

        vidaActual += cantidad;
        vidaActual = Mathf.Min(vidaActual, vidaMaxima);

        Debug.Log($"{gameObject.name} recuperó vida. Vida: {vidaActual}/{vidaMaxima}");
    }
}

