
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [Header("Referencias")]
    public Transform cameraPivot;
    public LayerMask capasObstaculos;

    [Header("Colisiones")]
    public float radio = 0.35f;
    public float margen = 0.2f;

    private Vector3 posicionLocalOriginal;

    void Start()
    {
        // Guardamos la posicion que configuraste en Unity.
        posicionLocalOriginal = transform.localPosition;
    }

    void LateUpdate()
    {
        if (cameraPivot == null)
            return;

        // Calculamos la posicion deseada de la camara.
        Vector3 posicionDeseada =
            cameraPivot.TransformPoint(posicionLocalOriginal);

        Vector3 origen = cameraPivot.position;

        Vector3 direccion = posicionDeseada - origen;

        float distancia = direccion.magnitude;

        if (distancia <= 0.01f)
            return;

        direccion.Normalize();

        float distanciaFinal = distancia;

        // Comprobamos si el origen esta dentro
        // de un obstaculo.
        Collider[] obstaculos = Physics.OverlapSphere(
            origen,
            radio,
            capasObstaculos,
            QueryTriggerInteraction.Ignore
        );

        if (obstaculos.Length > 0)
        {
            distanciaFinal = 0f;
        }
        else
        {
            // Buscamos obstaculos entre el jugador
            // y la posicion deseada de la camara.
            if (Physics.SphereCast(
                origen,
                radio,
                direccion,
                out RaycastHit hit,
                distancia,
                capasObstaculos,
                QueryTriggerInteraction.Ignore))
            {
                distanciaFinal = Mathf.Max(
                    0f,
                    hit.distance - margen
                );
            }
        }

        // Movemos la camara respetando
        // su posicion original.
        transform.position =
            origen + direccion * distanciaFinal;
    }
}

