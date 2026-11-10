using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelsManager : MonoBehaviour
{
    public Button[] lvlButtons;
    public GameObject monster;
    public GameObject LevelScreen;
    private void Start()
    {
       monster = GameObject.Find("Moster");
        int levelAt = PlayerPrefs.GetInt("levelAt", 2); /* < Change this int value to whatever your
                                                             level selection build index is on your
                                                             build settings */

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 2 > levelAt)
                lvlButtons[i].interactable = false;
        }

    }
    private void Update()
    {
        if (monster = null)
        {
            LevelScreen.SetActive(true);
            LevelScreen.GetComponent<Animator>().SetBool("lvlScreenClosed", false);
        }
    }

}
