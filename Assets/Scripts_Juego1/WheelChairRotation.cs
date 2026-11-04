using UnityEngine;

public class WheelChairRotation : MonoBehaviour
{
    [Header("Rotación")]
    [SerializeField] private float rotationAmount = 30f;
    [SerializeField] private float rotationSpeed = 150f;

    private float targetRotation;


    void Start()
    {
        targetRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetRotation -= rotationAmount;
        }

        if (Input.GetMouseButtonDown(1))
        {
            targetRotation += rotationAmount;
        }

        Quaternion target = Quaternion.Euler(0f, targetRotation, 0f);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            target,
            rotationSpeed * Time.deltaTime
        );
    }
}

