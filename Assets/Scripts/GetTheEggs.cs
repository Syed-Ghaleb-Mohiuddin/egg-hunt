using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTheEggs : MonoBehaviour
{

    public Transform startMarker;  // The starting position of the object
    public Transform endMarker;    // The ending position of the object
    public float speed = 1f;       // The speed of the movement

    private float distance;        // The total distance between the start and end positions
    private float startTime;
    public GameObject LevelCompleteScreen;
    public AudioSource LevelCompleteSound;


    // Start is called before the first frame update
    public void Start()
    {

        LevelCompleteSound = GameObject.Find("LevelComplete").GetComponent<AudioSource>();
        
        distance = Vector3.Distance(startMarker.position, endMarker.position);
        startTime = Time.time;
        Invoke("LevelFinished", 2f);
    }

    // Update is called once per frame
    public void Update()
    {
        float journeyLength = (Time.time - startTime) * speed;
        float fractionOfJourney = journeyLength / distance;
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
    }
    public void LevelFinished()
    {
        LevelCompleteScreen.SetActive(true);
        LevelCompleteSound.Play();
    }
}
