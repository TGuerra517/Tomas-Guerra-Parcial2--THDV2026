
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 3f;
    [SerializeField] private float minAngle = -30f;
    [SerializeField] private float maxAngle = 60f;

    private float verticalRotation;

    void Start()
    {
        verticalRotation = transform.localEulerAngles.x;

        if (verticalRotation > 180f)
        {
            verticalRotation -= 360f;
        }
    }

    void Update()
    {
        float mouseY = Input.GetAxisRaw("Mouse Y");

        verticalRotation -= mouseY * sensitivity;

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            minAngle,
            maxAngle
        );

        transform.localRotation = Quaternion.Euler(
            verticalRotation, 0f, 0f
        );
    }
}

