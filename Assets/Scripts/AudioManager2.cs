using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    private static AudioManager2 instance1;
    void Awake()
    {
        if (instance1 != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            instance1 = this;
        }

    }
}
