using UnityEngine;

public class FloatingEnemy : MonoBehaviour
{
    public Transform[] patrolpoints; // Array de puntos de patrulla

    public float speed = 3f; // Velocidad de movimiento
    public float rotationSpeed = 5f; // Velocidad de rotación
    public float reachDistance = 0.2f; // Distancia mínima para considerar que se ha alcanzado el punto de patrulla
    public int currentPoint = 0; // Índice del punto de patrulla actual


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < patrolpoints.Length; i++)
        {
            patrolpoints[i].parent = null;
            //if (patrolpoints[i] == null)
            //{
            //    Debug.LogError("Patrol point " + i + " is not assigned in the inspector.");
            //}
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (patrolpoints.Length == 0) { return; }
        Transform targetPoint = patrolpoints[currentPoint];
        
        //Direccion hacia el punto de patrulla
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        
        // Movimiento hacia el punto de patrulla
        transform.position += direction * speed * Time.deltaTime;
        
        // Rotación hacia el punto de patrulla
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //cambio de punto
        float distance = Vector3.Distance(transform.position, targetPoint.position);
        if (distance < reachDistance)
        {
            currentPoint++; // Pasar al siguiente punto de patrulla

            if (currentPoint == patrolpoints.Length)
            {
                currentPoint = 0; // Reiniciar al primer punto de patrulla
            }
        }

    }
}
