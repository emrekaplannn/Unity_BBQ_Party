using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletToggle : MonoBehaviour
{
    public GameObject walletContentsPanel;

    public void ToggleWalletContents()
    {
        walletContentsPanel.SetActive(!walletContentsPanel.activeSelf);
    }
}

