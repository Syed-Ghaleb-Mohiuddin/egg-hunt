using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScenes : MonoBehaviour
{
    public GameObject bg;
    public Animator animator;
    public Animator lvlAmim;
    public GameObject BackToHS;
    public GameObject BackToFirstPage;
    public GameObject GoTo2Page;
    public Button soundButton;
    private AudioSource soundSource;
    AudioClip audioClip;
    //private SoundManager soundManager;
    public bool SceneOpenedOnce = true;
    int sceneID1=1;
    public GameObject lvlBird;
    public GameObject PauseScreen;
    public AudioSource BGSound;
    public GameObject LevelCompleteScreen;
    void Start()
    {
        BGSound = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        GoTo2Page = GameObject.Find("NextBTN");
        LevelCompleteScreen = GameObject.Find("LevelCompleteManager");
        // SoundManager.audioSource2  = GameObject.FindGameObjectWithTag("BTN").GetComponent<AudioSource>();
        int activeSceneNumber = SceneManager.GetActiveScene().buildIndex;
        soundSource = GameObject.FindWithTag("BTN").GetComponent<AudioSource>();
        LoadAudioClip("BTNCLICK");
        //soundClip = GetComponent<AudioClip>();
        //Debug.Log(sceneID1);
        if (sceneID1  ==0)
        {
            SceneOpenedOnce = false;
        }
        else if (activeSceneNumber == 1 && SceneOpenedOnce)
        {
            lvlBird = GameObject.FindGameObjectWithTag("FlyingBird");
            lvlBird.GetComponent<Animator>().SetTrigger("SceneOpened");
            //Debug.Log(lvlBird.name);
            SceneOpenedOnce = false;
        }
        
    }
    private float soundVolume;

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            LevelCompleteScreen = GameObject.Find("LevelCompleteManager");

            if (LevelCompleteScreen!= null && LevelCompleteScreen.activeSelf == true)
            {
               
                BGSound.volume = 0;
            }
            else
            {
                if(SceneManager.GetActiveScene().buildIndex < 1)
                {

                BGSound.volume = PlayerPrefs.GetFloat("SoundVolume", soundVolume);
                }
            }
        }
    }
    public void MoveToScene(int sceneID){
        soundSource.PlayOneShot(audioClip);
        SceneManager.LoadScene(sceneID);
        
        if(sceneID == 0)
        {
            sceneID1 = sceneID;

            SceneOpenedOnce = false;
        }
        else if (sceneID == 1 && SceneOpenedOnce)
        {
            SceneOpenedOnce=false;
        }
        sceneID1 = sceneID;
    
        }
    public void quitGame(){
        soundSource.PlayOneShot(audioClip);
        Application.Quit();
    }
    public void showOptionsScreen(GameObject obj){

        // soundSource.volume = SoundManager.audioSource2.volume;
        animator.SetTrigger("OptionsBTNClick");
        soundSource.PlayOneShot(audioClip);
        animator.SetBool("CrossMarkSelected", false);
        obj.SetActive(true);
        bg.SetActive(true);
    }
    void OptnBG()
    {
        bg.SetActive(false);
    }
    public void hideOptionsScreen(GameObject obj){
      //  soundSource.volume = SoundManager.audioSource2.volume;

        soundSource.PlayOneShot(audioClip);

        animator.SetBool("CrossMarkSelected", true);
        Invoke("OptnBG", 0.3f);
    }
    public void goToNextPage()
    {
       // Debug.Log(SoundManager.audioSource2.volume);
       // soundSource.volume = SoundManager.audioSource2.volume;

        soundSource.PlayOneShot(audioClip);

        BackToFirstPage.SetActive(true);
        BackToHS.SetActive(false);
        lvlAmim.SetTrigger("NextPageTrig");
        GoTo2Page.GetComponent<Button>().interactable = false;


    }
    public void goToFirstPage()
    {
       // soundSource.volume = SoundManager.audioSource2.volume;

        soundSource.PlayOneShot(audioClip);

        BackToHS.SetActive(true);
        BackToFirstPage.SetActive(false);
        lvlAmim.SetTrigger("BackToFirstPage");
        GoTo2Page.GetComponent<Button>().interactable = true;
    }
 

    private void LoadAudioClip(string clipName)
    {
         audioClip = Resources.Load<AudioClip>(clipName);
        if (audioClip == null)
        {
            Debug.LogError("Failed to load audio clip: " + clipName);
            return;
        }
        soundSource.clip = audioClip;
       // Debug.Log(SoundManager.audioSource2.volume);
      //  soundSource.volume = SoundManager.audioSource2.volume;

    }

    public void OpenPauseScreen()
    {
        soundSource.PlayOneShot(audioClip);
        PauseScreen.GetComponent<Animator>().SetBool("OptionsScreenCloesed", false);

        PauseScreen.SetActive(true);
        bg.SetActive(true );
    }
    public void Resume() 
    {
        soundSource.PlayOneShot(audioClip);

        PauseScreen.GetComponent<Animator>().SetBool("OptionsScreenCloesed", true);
        Invoke("ResumeInvoke", 0.5f);

    } 
    public void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    } 
    void ResumeInvoke()
    {
        
        bg.SetActive(false);

    }
    public void GoToNextLevel()
    {
        BGSound.Play();
        int BuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(BuildIndex + 1);
    }

}
