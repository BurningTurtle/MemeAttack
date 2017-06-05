using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mentor : MonoBehaviour {

    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private Text dialogueText;
    private GameObject player;

    private bool introduced = false;
    private bool introductionFinished = false;
    private bool talking = false;

    // Use this for initialization
    void Start () {
        dialogueBox.SetActive(false);
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e"))
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 3)
            {
                if (!introduced)
                {
                    dialogueBox.SetActive(true);
                    StartCoroutine(introduction());
                }
                else if (!talking && introductionFinished)
                {
                    dialogueBox.SetActive(true);
                    StartCoroutine(talk());
                }
            }
        }
    }

    IEnumerator introduction()
    {
        introduced = true;
        dialogueText.text = "Sup nigga. Give Moritz some more time to finish me.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);
        introductionFinished = true;
    }

    IEnumerator talk()
    {
        talking = true;
        dialogueText.text = "My features will be implemented soon.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);
        talking = false;
    }
}
