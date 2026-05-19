using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Follow Settings")]
    public float smoothSpeed = 5f;

    [Header("Offset")]
    public Vector3 offset;

    private float fixedZ;

    void Start()
    {
        // Store original camera Z position
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Desired camera position
        Vector3 desiredPosition =
            target.position + offset;

        // Keep camera Z fixed
        desiredPosition.z = fixedZ;

        // Smooth follow
        Vector3 smoothedPosition =
            Vector3.Lerp(
                transform.position,
                desiredPosition,
                smoothSpeed * Time.deltaTime
            );

        // Apply final position
        transform.position = smoothedPosition;
    }
}