using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moster : MonoBehaviour
{
    [SerializeField]private ParticleSystem cloudParticle;
    private Animator amin;
    public GameObject monster;
    public GameObject LvlBirdflying;
    public GameObject NumberOfBirds;
    public GameObject Eggs;
    public GetTheEggs getTheEggs;
    public GameObject NumberOfBirdsText;
    public GameObject LevelCompleteScreen;
    public GameObject AliveBirds;
    public GameObject GameOverScreen;

    public AudioSource DestroyMonsterSound;
    public AudioSource  BirdCollidedSound;
    public AudioSource  GameOverSound;

    public static int remainingMonsters;
    int col;
    public int nextSceneLoad;
    TextMeshProUGUI numberOfBirds;
    bool goForOnlyOneTime = true;
    void Start(){
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        DestroyMonsterSound = GameObject.Find("MonsterDestroy").GetComponent<AudioSource>();
        NumberOfBirds = GameObject.Find("NumberOfBird");
        NumberOfBirdsText = GameObject.Find("Text (TMP)");
        BirdCollidedSound = GameObject.Find("Impact").GetComponent<AudioSource>();
        GameOverSound = GameObject.Find("Defeat").GetComponent<AudioSource>();    
        transform.position  = new Vector3 (transform.position.x,transform.position.y,transform.position.z);
        amin=GetComponent<Animator>();
        remainingMonsters = GameObject.FindGameObjectsWithTag("Moster").Length;
        // cloudParticle.SetActive(false);
    }
    void Update()
    {
        AliveBirds = GameObject.FindGameObjectWithTag("Player");
        if (AliveBirds == null && goForOnlyOneTime && remainingMonsters != 0 )
        {
            GameOverScreen.SetActive(true);
            Debug.Log(GameOverSound.volume);
            GameOverSound.Play();
            AliveBirds = gameObject;
            NumberOfBirdsText.SetActive(false);
            NumberOfBirds.SetActive(false);
            goForOnlyOneTime = false;
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SlingShot bird = collision.collider.GetComponent<SlingShot>();

        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Spike")) 
        {
            if (bird != null)
            {
                Debug.Log("Collided:)............");
                Destroy(gameObject);
                amin.SetBool("CollidedBool", true);

                Debug.Log("ssssssssssssss");
                Invoke("destroyMon", 0.5f);


            }
            Moster enemy = collision.collider.GetComponent<Moster>();
            if (enemy != null)
            {
                Debug.Log("aaaaaaaaaaaaaaaaaaa");

                return;
            }
            if (collision.contacts[0].normal.y < 0.5 && !collision.collider.CompareTag("Spike") && collision.collider.CompareTag("Player"))
            {
                Debug.Log("Collided " + col + " Times");
                col++;
                BirdCollidedSound.Play();
                amin.SetBool("CollidedBool", true);
                Invoke("destroyMon", 1f);
                if (remainingMonsters == 0)
                {
                    GameOverScreen.SetActive(false);
                    GameOverSound.Stop();
                }

            }

        if (collision.contacts[0].normal.y < 1.5 && collision.collider.CompareTag("Spike") && !collision.collider.CompareTag("Player"))
        {
            Debug.Log("Collided " + col + " Times");
            col++;
            BirdCollidedSound.Play();
            amin.SetBool("CollidedBool", true);
            Invoke("destroyMon", 1f);
            if (remainingMonsters == 0)
            {
                GameOverScreen.SetActive(false);
                GameOverSound.Stop();
            }

        }
        }
    }

    void destroyMon()
    {
        Debug.Log("Destory MOnser");
        cloudParticle = Instantiate(cloudParticle);
        DestroyMonsterSound.Play();
        cloudParticle.transform.position = monster.transform.position;
        //  Instantiate(cloudParticle,transform.position,Quaternion.identity);
        // cloudParticle.Play();
        Destroy(gameObject);
        remainingMonsters--;
        Debug.Log("Remaining Monsters :" + remainingMonsters);
        if (remainingMonsters == 0)
        {
        GameOverScreen.SetActive(false );
        GameOverSound.Stop();
            if (SceneManager.GetActiveScene().buildIndex == 60) 
            {
                Debug.Log("You Completed ALL Levels");
            }
            else
            {

                if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
            }
            NoMoreMonsters();
            GetToEggs();

        }
        // cloudParticle.SetActive(true);
    }

    void NoMoreMonsters()
    {
        Debug.Log("NoMoreMonsters");
        NumberOfBirds.SetActive(false);
        NumberOfBirdsText.SetActive(false);
        LvlBirdflying.SetActive(true);
        //GameOverScreen.SetActive(false);
       
    }




    public void GetToEggs()
    {
        Debug.Log("GetToEggs");
        LvlBirdflying.GetComponent<GetTheEggs>().enabled = true;
    }
}
