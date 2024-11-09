using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyRockets : MonoBehaviour
{
    public GameObject RocketButton;
    public void BuyRocket(int Price)
    {
        if (PlayerPrefs.GetInt("Coins") > Price)
        {
            RocketButton.GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - Price);
        }
        else
        {
            Debug.Log("Недостаточно монет");
        }
    }
    public void PickRocket(int RocketNumber)
    {
        PlayerPrefs.SetInt("Player",RocketNumber);
    }
}
