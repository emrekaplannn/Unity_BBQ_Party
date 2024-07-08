using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWallet : MonoBehaviour
{
    public Dictionary<string, int> wallet = new Dictionary<string, int>();
    public GameObject walletContentsPanel;
    public GameObject beerPrefab; // Prefab for Beer item
    public GameObject candyPrefab; // Prefab for Candy item
    public GameObject coconutPrefab; // Prefab for Coconut item
    public GameObject croissantPrefab; // Prefab for Croissant item
    public GameObject donutPrefab; // Prefab for Donut item
    public GameObject eggPrefab; // Prefab for Egg item
    public GameObject figPrefab; // Prefab for Beer item
    public GameObject godFoodPrefab; // Prefab for Candy item
    public GameObject hotChocolatePrefab; // Prefab for Coconut item
    public GameObject hotDogPrefab; // Prefab for Croissant item
    public GameObject mushroomPrefab; // Prefab for Donut item
    public GameObject noodlePrefab; // Prefab for Egg item
    public GameObject pineapplePrefab; // Prefab for Donut item
    public GameObject shrimpPrefab;
    public GameObject quantityTextPrefab; // Prefab for displaying item quantity

    private void Start()
    {
        if (walletContentsPanel == null)
        {
            Debug.LogError("Wallet Contents Panel is not assigned in the Inspector.");
            return;
        }

        UpdateWalletUI();
    }

    public void AddReward(string itemName)
    {
        if (wallet.ContainsKey(itemName))
        {
            wallet[itemName]++;
        }
        else
        {
            wallet[itemName] = 1;
        }

        UpdateWalletUI();
    }

    private void UpdateWalletUI()
    {
        // Clear existing wallet contents
        foreach (Transform child in walletContentsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Display updated wallet contents
        foreach (var item in wallet)
        {
            GameObject quantityTextObject = Instantiate(quantityTextPrefab, walletContentsPanel.transform);
            TextMeshProUGUI quantityText = quantityTextObject.GetComponent<TextMeshProUGUI>();
            if (quantityText != null)
            {
                quantityText.text = item.Value + "x";
            }
            else
            {
                Debug.LogWarning("Text component not found in quantity prefab.");
            }
            GameObject newItem = null;
            switch (item.Key)
            {
                case "Beer":
                    newItem = beerPrefab != null ? Instantiate(beerPrefab, walletContentsPanel.transform) : null;
                    break;
                case "Candy":
                    newItem = candyPrefab != null ? Instantiate(candyPrefab, walletContentsPanel.transform) : null;
                    break;
                case "Coconut":
                    newItem = coconutPrefab != null ? Instantiate(coconutPrefab, walletContentsPanel.transform) : null;
                    break;
                case "Croissant":
                    newItem = croissantPrefab != null ? Instantiate(croissantPrefab, walletContentsPanel.transform) : null;
                    break;
                case "Donut":
                    newItem = donutPrefab != null ? Instantiate(donutPrefab, walletContentsPanel.transform) : null;
                    break;
                case "Egg":
                    newItem = eggPrefab != null ? Instantiate(eggPrefab, walletContentsPanel.transform) : null;
                    break;
                case "Fig":
                    newItem = figPrefab != null ? Instantiate(figPrefab, walletContentsPanel.transform) : null;
                    break;
                case "God-Food":
                    newItem = godFoodPrefab != null ? Instantiate(godFoodPrefab, walletContentsPanel.transform) : null;
                    break;
                case "Hot-Chocolate":
                    newItem = hotChocolatePrefab != null ? Instantiate(hotChocolatePrefab, walletContentsPanel.transform) : null;
                    break;
                case "Hot-Dog":
                    newItem = hotDogPrefab != null ? Instantiate(hotDogPrefab, walletContentsPanel.transform) : null;
                    break;
                case "Mushroom":
                    newItem = mushroomPrefab != null ? Instantiate(mushroomPrefab, walletContentsPanel.transform) : null;
                    break;
                case "Noodle":
                    newItem = noodlePrefab != null ? Instantiate(noodlePrefab, walletContentsPanel.transform) : null;
                    break;
                case "Pineapple":
                    newItem = pineapplePrefab != null ? Instantiate(pineapplePrefab, walletContentsPanel.transform) : null;
                    break;
                case "Shrimp":
                    newItem = shrimpPrefab != null ? Instantiate(shrimpPrefab, walletContentsPanel.transform) : null;
                    break;
                default:
                    Debug.LogWarning("Unknown item: " + item.Key);
                    break;
            }

            if (newItem != null)
            {
                // Optionally add code here to update other properties of the newItem if necessary
            }
            else
            {
                Debug.LogWarning("Prefab for item not assigned or found: " + item.Key);
            }
        }
    }
}
