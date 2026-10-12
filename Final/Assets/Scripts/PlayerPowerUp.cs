using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{

    [Header("Visual")]
    public float rotationSpeed = 50f;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) //para verificar si el objeto que colisiona tiene la etiqueta "Player"
            return;
        
        playerController player = other.GetComponent<playerController>(); //para obtener el componente playerController del objeto que colisiona
        
        if (player != null)
        {
            player.UnlockShottPower(); //para Collectar
        }
        
        Destroy(gameObject); //para destruir el objeto que se colisiona
    }


}
