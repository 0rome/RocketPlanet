using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] Audio;
    public void Planet(int audio)
    {
        Audio[audio].Play();
    }
}
