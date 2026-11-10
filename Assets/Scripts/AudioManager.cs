using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    void Awake()
    {
        if (instance !=null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            instance = this;
        }
        
    }


    void Update()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(sceneNumber);
        if (sceneNumber > 1)
        {
            //Debug.Log(PlayerPrefs.GetFloat("SoundVolume"));
            
            instance.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundVolume") - 0.7f;
        }
        else if (sceneNumber <=1) 
        {
            instance.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundVolume");
        }
    }

}
