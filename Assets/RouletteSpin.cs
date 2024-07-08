using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RouletteSpin : MonoBehaviour
{
    public Image[] itemImages; // Array of item images on the roulette wheel
    public Sprite goldSquareImage; // Sprite for the gold square
    public Sprite blueSquareImage; // Sprite for the blue square
    private Sprite[] originalImages; // Array to store the original sprites of the item images
    public float spinDuration = 2f; // Duration for the spin animation
    public int spinCycles = 3; // Number of cycles for the spin
    public PlayerWallet playerWallet; // Reference to the player wallet script
    public Button spinButton; // Reference to the spin button
    public GameObject winPopup; // Reference to the win popup
    public TextMeshProUGUI winMessage; // Reference to the win message text
    public Transform rewardContainer; // Reference to the reward container
    public Image tickSymbol; // Reference to the tick symbol image
    public int blinkCount = 4; // Number of times the final result blinks
    public float blinkInterval = 0.5f; // Interval for the blinking effect
    public float previousGoldSquareStayDuration = 0.2f; // Duration for the gold square to stay

    // Prefabs for the items
    public GameObject beerPrefab;
    public GameObject candyPrefab;
    public GameObject coconutPrefab;
    public GameObject croissantPrefab;
    public GameObject donutPrefab;
    public GameObject eggPrefab;
    public GameObject figPrefab;
    public GameObject godFoodPrefab;
    public GameObject hotChocolatePrefab;
    public GameObject hotDogPrefab;
    public GameObject mushroomPrefab;
    public GameObject noodlePrefab;
    public GameObject pineapplePrefab;
    public GameObject shrimpPrefab;

    private int currentIndex; // Store the current index

    private void Start()
    {
        // Store the original images of the item squares
        originalImages = new Sprite[itemImages.Length];
        for (int i = 0; i < itemImages.Length; i++)
        {
            originalImages[i] = itemImages[i].sprite;
        }
    }

    public void StartSpin()
    {
        // Start the spin coroutine
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        spinButton.gameObject.SetActive(false); // Disable the spin button during the spin

        float elapsedTime = 0f; // Elapsed time for the spin
        currentIndex = 0; // Start at the first item
        int totalItems = itemImages.Length; // Total number of items
        int cycles = totalItems * spinCycles; // Total cycles to spin

        // Add randomness to the final stopping point
        int randomStopIndex = Random.Range(0, totalItems);
        int totalSpins = cycles + randomStopIndex;
        int previousIndex = -1; // To keep track of the previous index

        while (elapsedTime < spinDuration)
        {
            // If there's a previous index, set it back to original after a delay
            if (previousIndex >= 0)
            {
                StartCoroutine(ResetPreviousSquare(previousIndex));
            }

            previousIndex = currentIndex;

            itemImages[currentIndex].sprite = goldSquareImage; // Set current square to gold
            currentIndex = (currentIndex + 1) % totalItems; // Move to the next square

            elapsedTime += spinDuration / totalSpins; // Increment elapsed time
            yield return new WaitForSeconds(spinDuration / totalSpins); // Wait for the next frame
        }

        // Reset all items to their original images
        for (int i = 0; i < itemImages.Length; i++)
        {
            itemImages[i].sprite = originalImages[i];
        }

        // Blink the final result
        for (int i = 0; i < blinkCount; i++)
        {
            itemImages[currentIndex].sprite = goldSquareImage;
            yield return new WaitForSeconds(blinkInterval);
            itemImages[currentIndex].sprite = originalImages[currentIndex];
            yield return new WaitForSeconds(blinkInterval);
        }

        // Final result
        itemImages[currentIndex].sprite = blueSquareImage;

        // Position the tick symbol over the selected item
        tickSymbol.rectTransform.position = itemImages[currentIndex].rectTransform.position;
        tickSymbol.transform.SetAsLastSibling(); // Ensure it's rendered on top
        tickSymbol.gameObject.SetActive(true);

        string itemName = itemImages[currentIndex].gameObject.name;

        // Show the reward in the popup
        ShowReward(itemName);

        playerWallet.AddReward(itemName);

        // Do not re-enable the spin button here
    }

    private IEnumerator ResetPreviousSquare(int index)
    {
        // Wait for the specified duration before resetting the previous square
        yield return new WaitForSeconds(previousGoldSquareStayDuration);
        itemImages[index].sprite = originalImages[index];
    }

    private void ShowReward(string itemName)
    {
        // Clear existing children in the reward container
        foreach (Transform child in rewardContainer)
        {
            Destroy(child.gameObject);
        }

        // Get the reward prefab based on the item name
        GameObject rewardPrefab = GetRewardPrefab(itemName);
        if (rewardPrefab != null)
        {
            // Instantiate the reward prefab in the reward container
            GameObject rewardInstance = Instantiate(rewardPrefab, rewardContainer);
            rewardInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        winMessage.text = "You won " + itemName + "!"; // Update the win message
        winPopup.SetActive(true); // Show the win popup
    }

    private GameObject GetRewardPrefab(string itemName)
    {
        // Return the corresponding prefab based on the item name
        switch (itemName)
        {
            case "Beer":
                return beerPrefab;
            case "Candy":
                return candyPrefab;
            case "Coconut":
                return coconutPrefab;
            case "Croissant":
                return croissantPrefab;
            case "Donut":
                return donutPrefab;
            case "Egg":
                return eggPrefab;
            case "Fig":
                return figPrefab;
            case "God-Food":
                return godFoodPrefab;
            case "Hot-Chocolate":
                return hotChocolatePrefab;
            case "Hot-Dog":
                return hotDogPrefab;
            case "Mushroom":
                return mushroomPrefab;
            case "Noodle":
                return noodlePrefab;
            case "Pineapple":
                return pineapplePrefab;
            case "Shrimp":
                return shrimpPrefab;
            default:
                Debug.LogWarning("Unknown reward: " + itemName);
                return null;
        }
    }

    public void CloseWinPopup()
    {
        // Hide the win popup
        winPopup.SetActive(false);

        // Reset the blue square to the original image
        itemImages[currentIndex].sprite = originalImages[currentIndex];

        // Hide the tick symbol
        tickSymbol.gameObject.SetActive(false);

        // Re-enable the spin button
        spinButton.gameObject.SetActive(true);
    }
}
