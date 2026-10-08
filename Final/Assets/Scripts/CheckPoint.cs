using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Renderer checkpointRenderer;

    private void Start()
    {
        checkpointRenderer = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerController player = other.GetComponent<playerController>();
        if (player != null)
        {
            player.SetSpawnPoint(transform);
            //Debug.Log("Checkpoint reached!");

            if (checkpointRenderer != null)
            {
                checkpointRenderer.material.color = Color.yellow; // Para cambiar el color del material del checkpoint a amarillo
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
