using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    public bool levelCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if (levelCompleted)
            return;
        if (!other.CompareTag("Player"))
            return;
        levelCompleted = true;
        Debug.Log("Nivel Completado!");

        //Aqui va el codigo para pasar al siguiente nivel, como cargar la siguiente escena o mostrar un mensaje de victoria.
        if (levelCompleted)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Cargar la siguiente escena en el índice de construcción
        }
    }

}
