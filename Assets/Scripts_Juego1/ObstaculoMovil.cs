
using UnityEngine;

public class ObstaculoMovil : MonoBehaviour
{
    public DatosObstaculo datos;

    public Vector3 direccion = Vector3.right;

    Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float movimiento = Mathf.PingPong(
            Time.time * datos.velocidad,
            datos.distancia
        );

        transform.position = posicionInicial + direccion.normalized * movimiento;
    }
}

