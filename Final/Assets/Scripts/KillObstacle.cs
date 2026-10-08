using UnityEngine;

public class KillObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Buscar el componente playerController en el col lider o en sus padres
        playerController player = other.GetComponent<playerController>();

        if (player != null)
        {
            player.Die();
        }
    }


}
