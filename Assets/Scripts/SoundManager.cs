using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    private AudioSource audioSource;
    private  AudioSource audioSource2;
    private Slider volumeSlider;
    private Slider volumeSlider2;
    private const string SOUND_VOLUME_KEY = "SoundVolume";
    private const string SOUND_VOLUME_KEY2 = "SoundVolume :";
    private float soundVolume =1;
    private float soundVolume2=1;
    public AudioSource DestroyMonsterSound;
    public AudioSource BirdCollidedSound;
    public AudioSource SlingShotSound;
    public AudioSource BirdShoot;
    public AudioSource BirdFlySound;
    public AudioSource BirdDestroySound;


    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            
        SlingShotSound = GameObject.Find("SlingShotStreched").GetComponent<AudioSource>();
        BirdShoot = GameObject.Find("BirdShoot").GetComponent<AudioSource>();
        BirdFlySound = GameObject.Find("BirdFlySound").GetComponent<AudioSource>();
        DestroyMonsterSound = GameObject.Find("MonsterDestroy").GetComponent<AudioSource>();
        BirdCollidedSound = GameObject.Find("Impact").GetComponent<AudioSource>();
        BirdDestroySound = GameObject.Find("DestroyBird").GetComponent<AudioSource>();

        
        DestroyMonsterSound.volume = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY2, soundVolume2);
        BirdCollidedSound.volume = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY2, soundVolume2);
       
        SlingShotSound.volume = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY2, soundVolume2);
        BirdShoot.volume = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY2, soundVolume2);
        BirdFlySound.volume = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY2, soundVolume2);
            BirdDestroySound.volume = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY2, soundVolume2);
        }

        if (SceneManager.GetActiveScene().buildIndex < 1)
        {
            volumeSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
            audioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
            soundVolume = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY, soundVolume);
            volumeSlider.value = soundVolume;
            SetVolume1(soundVolume);
            //Debug.Log("Player 1 :" + PlayerPrefs.GetFloat(SOUND_VOLUME_KEY, soundVolume));
            //////////////////
            volumeSlider2 = GameObject.FindGameObjectWithTag("EffectSlider").GetComponent<Slider>();
            audioSource2 = GameObject.FindGameObjectWithTag("BTN").GetComponent<AudioSource>();
            soundVolume2 = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY2, soundVolume2);
            volumeSlider2.value = soundVolume2;
            SetVolume2(soundVolume2);
            // Debug.Log("Player 2 :" + PlayerPrefs.GetFloat(SOUND_VOLUME_KEY2, soundVolume2));
        }
    }
    void Update()
    {
       
    }
    public void SetVolume1(float volume)
    {
        audioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();

        volumeSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        soundVolume = volume;
        PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, soundVolume);
        volumeSlider.value = soundVolume;
        audioSource.volume = volume;
       
    } 
    public void SetVolume2(float volume2)
    {
        if (SceneManager.GetActiveScene().buildIndex < 1)
        {
            audioSource2 = GameObject.FindGameObjectWithTag("BTN").GetComponent<AudioSource>();
            volumeSlider2 = GameObject.FindGameObjectWithTag("EffectSlider").GetComponent<Slider>();
            soundVolume2 = volume2;
            PlayerPrefs.SetFloat(SOUND_VOLUME_KEY2, soundVolume2);
            volumeSlider2.value = soundVolume2;
            audioSource2.volume = volume2;
        }
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            soundVolume2 = volume2;
         
            DestroyMonsterSound.volume = soundVolume2;
            BirdCollidedSound.volume = soundVolume2;
            SlingShotSound.volume = soundVolume2;
            BirdShoot.volume = soundVolume2;
            BirdFlySound.volume = soundVolume2;
            BirdDestroySound.volume = soundVolume2;
        }
    }
}
