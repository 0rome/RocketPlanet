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

        // Инициализация значений в PlayerPrefs
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
            // Обновляем текст с количеством монет каждую секунду
            coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            UpdateRocketSkin();

            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateRocketSkin()
    {
        // Получаем значение "Player" из PlayerPrefs
        int playerIndex = PlayerPrefs.GetInt("Player");

        // Отключаем все ракеты
        for (int i = 0; i < Rockets.Length; i++)
        {
            Rockets[i].SetActive(i == playerIndex);
        }
    }
}
