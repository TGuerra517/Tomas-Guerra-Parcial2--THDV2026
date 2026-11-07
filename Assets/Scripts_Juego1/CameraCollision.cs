
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform cameraPivot;
    public LayerMask capasObstaculos;

    public float distancia = 4f;
    public float radio = 0.2f;
    public float margen = 0.1f;

    void LateUpdate()
    {
        Vector3 origen = cameraPivot.position;
        Vector3 direccion = -cameraPivot.forward;

        float distanciaFinal = distancia;

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

        transform.position =
            origen + direccion * distanciaFinal;
    }
}

