using TMPro;
using UnityEngine;

public class CollectibleUI : MonoBehaviour
{
    [SerializeField] private TMP_Text collectibleText;

    public void UpdateCounter(int amount)
    {
        //collectibleText.text = $"Collectibles: {amount}"; // para contar los cristales recogidos y mostrarlos en la UI
        collectibleText.text = "0" + amount; // Para contar los cristales recogidos y mostrarlos en la UI
    }

}
