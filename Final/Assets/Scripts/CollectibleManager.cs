using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;
    private int collectibleCount;

    [SerializeField] private CollectibleUI collectibleUI; // Reference to the CollectibleUI script



    private void Awake()
    {
        Instance = this;
    }

    public void AddCollectible()
    {
        collectibleCount++;

        Debug.Log("Coleccionables: " + collectibleCount);
        
        collectibleUI.UpdateCounter(collectibleCount); // Autualizar el contador en la UI

    }

    public int GotCollectibleCount()
    {
        return collectibleCount;
    }

}