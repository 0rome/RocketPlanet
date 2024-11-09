using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Skin : MonoBehaviour
{
    public Text coinText;
    public GameObject[] Rockets;

    private void Start()
    {
        PlayerPrefs.SetInt("Coins", 1000); 

        // ������������� �������� � PlayerPrefs
        if (!PlayerPrefs.HasKey("Player"))
        {
            PlayerPrefs.SetInt("Player", 0);
        }
        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 0);
        }
     
        StartCoroutine(UpdateCoinsAndSkin());
    }

    IEnumerator UpdateCoinsAndSkin()
    {
        while (true)
        {
            // ��������� ����� � ����������� ����� ������ �������
            coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            UpdateRocketSkin();

            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateRocketSkin()
    {
        // �������� �������� "Player" �� PlayerPrefs
        int playerIndex = PlayerPrefs.GetInt("Player");

        // ��������� ��� ������
        for (int i = 0; i < Rockets.Length; i++)
        {
            Rockets[i].SetActive(i == playerIndex);
        }
    }
}
