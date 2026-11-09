

using UnityEngine;

public class ObstaculoMovil : MonoBehaviour
{
    public DatosObstaculo datos;

    public Vector3 direccion = Vector3.right;

    [SerializeField] private bool movimientoCentrado = false;

    Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        if (datos == null || direccion == Vector3.zero)
            return;

        float movimiento = Mathf.PingPong(
            Time.time * datos.velocidad,
            datos.distancia
        );

        if (movimientoCentrado)
        {
            // Recorre desde -distancia hasta +distancia.
            float desplazamiento = movimiento * 2f - datos.distancia;

            transform.position = posicionInicial
                + direccion.normalized * desplazamiento;
        }
        else
        {
            // Movimiento original: desde 0 hasta distancia.
            transform.position = posicionInicial
                + direccion.normalized * movimiento;
        }
    }
}
