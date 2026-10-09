using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target; // El objetivo que la cámara seguirá

    public float distance = 6.0f; // La distancia desde la cámara al objetivo
    public float height = 2.0f; // La altura de la cámara respecto al objetivo

    public float rotationSpeed = 120f; // La velocidad de rotación de la cámara
    private float currentYaw = 0; // El ángulo de rotación actual de la cámara
    private bool isDragging = false; // Indica si el usuario está arrastrando el ratón

    // Update is called once per frame
    void Update()
    {
        //Detectar click del mouse derecho para activar la rotación de la cámara
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isDragging = true;
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }
        if (isDragging)
        {
            float mouseX = Mouse.current.delta.x.ReadValue();
            currentYaw += mouseX * rotationSpeed * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        if (target == null) return; // Para evitar errores si el target no está asignado

        // para calcular la posición deseada de la cámara en función del ángulo de rotación actual
        Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);
       
        Vector3 offset = new Vector3(0, height, -distance);

        // Actualizar la posición de la cámara
        transform.position = target.position + rotation * offset; // para que la cámara siga al objetivo con un offset
        // Hacer que la cámara mire al objetivo
        transform.rotation = Quaternion.LookRotation(target.position - transform.position); // para que la cámara mire al objetivo
    }
}

    
