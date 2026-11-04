
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WheelchairMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float impulseForce = 2f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float deceleration = 2f;

    private Rigidbody rb;
    private float scrollInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Detectamos el movimiento de la rueda
        scrollInput += Input.mouseScrollDelta.y;
    }

    void FixedUpdate()
    {
        // Aplicamos el impulso
        if (scrollInput != 0)
        {
            rb.AddForce(
                transform.forward * scrollInput * impulseForce,
                ForceMode.VelocityChange
            );

            scrollInput = 0;
        }

        // Obtenemos la velocidad horizontal
        Vector3 velocity = rb.linearVelocity;
        Vector3 horizontalVelocity = new Vector3(
            velocity.x, 0f, velocity.z
        );

        // Limitamos la velocidad maxima
        horizontalVelocity = Vector3.ClampMagnitude(
            horizontalVelocity, maxSpeed
        );

        // Aplicamos desaceleracion progresiva
        horizontalVelocity = Vector3.MoveTowards(
            horizontalVelocity,
            Vector3.zero,
            deceleration * Time.fixedDeltaTime
        );

        // Conservamos la velocidad vertical (gravedad)
        rb.linearVelocity = new Vector3(
            horizontalVelocity.x,
            velocity.y,
            horizontalVelocity.z
        );
    }
}
