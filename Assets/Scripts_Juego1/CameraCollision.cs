
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform cameraPivot;
    public LayerMask capasObstaculos;

    public float distancia = 4f;
    public float radio = 0.35f;
    public float margen = 0.2f;

    void LateUpdate()
    {
        if (cameraPivot == null) return;

        Vector3 origen = cameraPivot.position;
        Vector3 direccion = -cameraPivot.forward;

        float distanciaFinal = distancia;

        // Comprueba si el origen está dentro de una pared.
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
            // Busca obstáculos entre el jugador y la cámara.
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

        transform.position = origen + direccion * distanciaFinal;
    }
}

