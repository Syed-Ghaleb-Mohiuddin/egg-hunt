using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlingShot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPosition;
    public Transform center;
    public Transform IdlePosition;

    public static bool isMouseDown;
    public float maxLength;
    public float bottomBoundry;
    public Vector3 currentPosition;
    public GameObject Strip1;
    public GameObject Strip2;


    public GameObject birdPrefab;
    public GameObject bird1;
    Rigidbody2D bird;
    Collider2D birdCollider;
    public float birdPositionOffset;
    public GameObject AliveBirds;
    public float force;
   /* private int callCount = 0;
    public int maxCallCount;*/

    public Animator amin;

    public TextMeshProUGUI NumberOfBirds;

    public AudioSource SlingShotSound;
    public AudioSource BirdShoot;
    public AudioSource BirdFlySound;
    public AudioSource GameOverSound;
    public GameObject GameOverScreen;
    public GameObject[] Monsters;
   
    // public GameObject cameraPos;
    void Start()
    {
        SlingShotSound = GameObject.Find("SlingShotStreched").GetComponent<AudioSource>();
        BirdShoot = GameObject.Find("BirdShoot").GetComponent<AudioSource>();
        BirdFlySound = GameObject.Find("BirdFlySound").GetComponent<AudioSource>();
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPosition[0].position);
        lineRenderers[1].SetPosition(0, stripPosition[1].position);
        NumberOfBirds.text = maxBirdCount.ToString();
        

        createBird();
        
    }
    /* void createBird()
     {
         bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
         birdCollider = bird.GetComponent<Collider2D>();
         birdCollider.enabled = false;
         bird.isKinematic = true;
     }*/
    private int birdCount = 0;
    public int maxBirdCount = 3;

    
   
        
       

    public void createBird()
    {
     
        if (birdCount >= maxBirdCount)
        {
            Strip1.SetActive(false);
            Strip2.SetActive(false);
            SlingShotSound.volume = 0;
            BirdShoot.volume = 0;
            BirdFlySound.volume = 0;
        NumberOfBirds.text = (maxBirdCount-birdCount).ToString();
            // If the maximum bird count has been reached, exit the function
            return;
        }

       
        bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
         birdCollider = bird.GetComponent<Collider2D>();
         birdCollider.enabled = false;
         bird.isKinematic = true;

        // Attach a script to the bird object to handle destruction
        BirdMovement birdScript = birdPrefab.GetComponent<BirdMovement>();
        birdScript.OnBirdDestroyed += BirdDestroyed;
        NumberOfBirds.text = (maxBirdCount-birdCount).ToString();
        birdCount++;
    }

    private void BirdDestroyed()
        
    {
        birdCount--;
        // Create a new bird after the previous one is destroyed
        createBird();
    }

    void Update()
    {
        
        
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            currentPosition = ClampBoundary(currentPosition);
            SetStrips(currentPosition);
                   if(birdCollider){
        birdCollider.enabled = true;
                

            }
        }
        else
        {
            ResetStrips();
        }
        if (bird!=null && SceneManager.GetActiveScene().buildIndex == 17 || bird != null && SceneManager.GetActiveScene().buildIndex == 45 || bird != null && SceneManager.GetActiveScene().buildIndex == 46)
        {

            bird1 = GameObject.Find("Player");
            bird.gameObject.GetComponent<Transform>().transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
        if (bird != null && SceneManager.GetActiveScene().buildIndex == 45 && SceneManager.GetActiveScene().buildIndex == 47)
        {

            bird1 = GameObject.Find("Player");
            bird.gameObject.GetComponent<Transform>().transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
    }
    void OnMouseDown()
    {
        isMouseDown = true;
        SlingShotSound.Play();
    }
    void OnMouseUp()
    {
        isMouseDown = false;
        SlingShotSound.Stop();
        BirdShoot.Play();
        Shoot();
    }
    public float birdShootSpeed = -1;
    public float BoostSpeed = -1.3f;
    public void boostTheBird()
    {
        birdShootSpeed = BoostSpeed;
        
    }
    void Shoot()
    {
        
        if(bird != null)
        {
            BirdFlySound.Play();
        bird.isKinematic=false;

        Vector3 birdForce = (currentPosition - center.position) * force * birdShootSpeed;
        bird.velocity = birdForce;
            
        bird = null;

        birdCollider= null;
            birdShootSpeed = -1f;
            Invoke("createBird",1f);

        }
        
        
    }
    void ResetStrips()
    {
        //createBird();

        currentPosition = IdlePosition.position;
        SetStrips(currentPosition);
    }
    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);
        if (bird)
        {
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffset;
            bird.transform.right = -dir.normalized;
        }
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundry, 1000);
        return vector;
    }
    public void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("aaaaaaaaaaaaaaaaabbbbbbb");
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("coooooollllllllllllli");
            amin.SetTrigger("GotHit");
            BirdFlySound.Stop();
        }
    }
}

