using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    [SerializeField]
    private GameObject pausePanel, dialogueBox;
    private bool dialogueWasActive;

	// Use this for initialization
	void Start () {
        pausePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeSelf)
            {
                if(dialogueBox.activeSelf == true)
                {
                    dialogueWasActive = true;
                    dialogueBox.SetActive(false);
                }
                else
                {
                    dialogueWasActive = false;
                }
                Time.timeScale = 100;
                pausePanel.SetActive(true);
            }
            else
            {
                if(dialogueWasActive == true)
                {
                    dialogueBox.SetActive(true);
                }
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
        }
	}

    public void exitGame()
    {
        Application.Quit();
    }
}
