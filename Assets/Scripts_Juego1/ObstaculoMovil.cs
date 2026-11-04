using UnityEngine;

public class ObstaculoMovil : MonoBehaviour
{
    public DatosObstaculo datos;

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

        transform.position = posicionInicial + Vector3.right * movimiento;
    }
}
