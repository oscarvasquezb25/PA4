using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Visual")]
    public float rotationSpeed = 50f;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //FindAnyObjectByType<CollectibleManager>().AddCollectible();
            CollectibleManager.Instance.AddCollectible();
            Destroy(gameObject);

        }
    }

}
