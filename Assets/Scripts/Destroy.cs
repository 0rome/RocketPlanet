using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnDestroy",timeToDestroy);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
