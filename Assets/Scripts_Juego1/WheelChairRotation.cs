using UnityEngine;

public class WheelChairRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }

        if (Input.GetMouseButton(1))
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }
}

