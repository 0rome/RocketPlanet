using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    public GameObject[] Players;
    void Awake()
    {
        if (Players != null)
        {
            switch (PlayerPrefs.GetInt("Player"))
            {
                case 0:
                    Spawn(0);
                    break;
                case 1:
                    Spawn(1);
                    break;
                case 2:
                    Spawn(2);
                    break;
                case 3:
                    Spawn(3);
                    break;
                case 4:
                    Spawn(4);
                    break;
                case 5:
                    Spawn(5);
                    break;
                case 6:
                    Spawn(6);
                    break;
                case 7:
                    Spawn(7);
                    break;
                case 8:
                    Spawn(8);
                    break;
                case 9:
                    Spawn(9);
                    break;
            }
        }
        
    }
    private void Spawn(int number)
    {
        Instantiate(Players[number],transform.position,Quaternion.identity);
    }
}
