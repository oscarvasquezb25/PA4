using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -8f); // para que la cámara siga al jugador con un offset
    public float smoothSpeed = 5f; // para suavizar el movimiento de la cámara

    private void LateUpdate()
    {
        if (target == null) return; // para evitar errores si el target no está asignado

        Vector3 desiredPosition = target.position + offset; // para que la cámara siga al jugador con un offset
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); // para suavizar el movimiento de la cámara
        transform.position = smoothedPosition; // para actualizar la posición de la cámara
        transform.LookAt(target); // para que la cámara mire al jugador
    }
}
