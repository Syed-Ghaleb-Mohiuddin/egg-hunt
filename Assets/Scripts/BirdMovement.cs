using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdMovement : MonoBehaviour
{
    public GameObject cameraUse;
    Vector3 initialPosition;
   [SerializeField]private ParticleSystem cloudParticle;
    public bool IsFlying=false;
    Vector2 directionToInitialPosition;
    public GameObject monster;
    [SerializeField] private float LounchSpeed = 500f;
    private float timeSittingArround;
    private bool birdLounched;
    public Animator amin;
    public ParticleSystem ParticleBird;
    public GameObject Bird;
    private  Transform playerPos;

    public AudioSource BirdDestroySound;
    public AudioSource BirdFlySound;
   
    Vector3 pos ;
    private void Awake(){
        initialPosition = transform.position;
    }
    private void Start(){
       
        BirdDestroySound = GameObject.Find("DestroyBird").GetComponent<AudioSource>();
        BirdFlySound = GameObject.Find("BirdFlySound").GetComponent<AudioSource>();
        GetComponent<LineRenderer>().enabled =false;
         cameraUse = GameObject.FindGameObjectWithTag("MainCamera");
        cameraUse.GetComponent <CameraFollow> ().enabled = true;

    }
    private void Update(){
        GetComponent<LineRenderer>().SetPosition(1,new Vector3(initialPosition.x,initialPosition.y,0f));
        GetComponent<LineRenderer>().SetPosition(1,new Vector3(transform.position.x-0.5f,transform.position.y-0,0f));
        if(birdLounched && GetComponent<Rigidbody2D>().velocity.magnitude<=0.1){
            timeSittingArround+=Time.deltaTime;
         
        if(transform.position.y > 10||transform.position.y < -10||transform.position.x > 10||transform.position.x <= -10){
           Debug.Log("Got");
            Instantiate(cloudParticle,transform.position,Quaternion.identity);
            cloudParticle.Play();
           string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
        if(timeSittingArround>1){
            Invoke("DestryBird",1f);

        }
     }
    }
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color=Color.red;
        GetComponent<LineRenderer>().enabled =true;
    }
    private void OnMouseUp()
    {
        IsFlying = true;
        GetComponent<SpriteRenderer>().color=Color.white;
        Vector2 directionToInitialPosition = initialPosition-transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * LounchSpeed);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<LineRenderer>().enabled =false;
        birdLounched= true;
    }
 

    private void OnMouseDrag(){
        
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       if(newPosition.x <= -4 && newPosition.x >= -8 && newPosition.y <= 1 && newPosition.y >= -3 ){
        transform.position = new Vector3(newPosition.x,newPosition.y);
        }
    

    }
     public void DestryBird (){
        if(SlingShot.isMouseDown == false) { 
        ParticleBird = Instantiate(ParticleBird);
        ParticleBird.transform.position = Bird.transform.position;
        ParticleBird.Play();
        BirdDestroySound.Play();
      Destroy(Bird, 0.1f);
        Vector3 temp = cameraUse.transform.position;
        temp.x = 0.24f * 0.1f;
            
        }
        //Instantiate(cloudParticle,transform.position,Quaternion.identity).Play();
        // cloudParticle.Play();
        //  cloudParticle.duration = 2.0f;
        //string currentScene = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene(currentScene);
    }
    public AudioSource GameOverSound;
    public GameObject GameOverScreen;
    public GameObject[] Monsters;
    public GameObject AliveBirds;

    public void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Spike")){
            if (SlingShot.isMouseDown == false)
            {
                amin = Bird.gameObject.GetComponent<Animator>();
                amin.SetTrigger("GotHit");
                Vector3 tempPos = cameraUse.transform.position;
                playerPos = GameObject.FindGameObjectWithTag("Player").transform;
                tempPos.x = playerPos.position.x + 1f * Time.deltaTime;
                BirdFlySound.Stop();
                Invoke("DestryBird", 1f);
      
            }

        }
    }


    public delegate void BirdDestroyedDelegate();

    // Define the event
    public event BirdDestroyedDelegate OnBirdDestroyed;

    // Method to call when the bird is destroyed
    private void DestroyBird()
    {
        // Perform any necessary cleanup or actions before the bird is destroyed

        // Raise the OnBirdDestroyed event
        if (OnBirdDestroyed != null)
        {
            OnBirdDestroyed.Invoke();
        }

        // Destroy the bird object
        Destroy(gameObject);
    }
}
