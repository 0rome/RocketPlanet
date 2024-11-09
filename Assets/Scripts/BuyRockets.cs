using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyRockets : MonoBehaviour
{
    public GameObject RocketButton;
    public GameObject PriceButton;

    public string KEY;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(KEY)) { PlayerPrefs.SetInt(KEY,0); }
        if (PlayerPrefs.GetInt(KEY) == 1 && PriceButton != null)
        {
            PriceButton.SetActive(false);
            RocketButton.GetComponent<Button>().interactable = true;
        }
    }
    public void BuyRocket(int Price)
    {
        if (PlayerPrefs.GetInt("Coins") > Price)
        {
            RocketButton.GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - Price);

            if (PriceButton != null)
            {
                PriceButton.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Недостаточно монет");
        }
    }
    public void PickRocket(int RocketNumber)
    {
        PlayerPrefs.SetInt("Player",RocketNumber);
        
        
        PlayerPrefs.SetInt(KEY,1);
    }
}
