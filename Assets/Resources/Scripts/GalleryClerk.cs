using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GalleryClerk : MonoBehaviour {

    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private Text dialogueText;
    private GameObject player;

    private bool introduced = false;
    private bool introductionFinished = false;
    private bool talk = false;


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
                else if (!talk && introductionFinished)
                {
                    Debug.Log("inside talk");
                    dialogueBox.SetActive(true);
                    StartCoroutine(back());
                }
            }
        }
    }

    IEnumerator introduction()
    {
        player.GetComponent<Player>().canMove = false;
        introduced = true;
        dialogueText.text = "Hey Player. I welcome you as well.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Odds are high that you've already met my brother. He's the salesclerk.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "However, my job is to guard our gallery.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Here you can see every Meme inside this simulation portrayed as a grey statue.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Once you've killed at least one of its kind, the statue will assume color.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Then you'll see the Meme's name, too.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "If you wanna go back, just talk to me. Have a nice time!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);
        introductionFinished = true;
        player.GetComponent<Player>().canMove = true;
    }

    IEnumerator back()
    {
        player.GetComponent<Player>().canMove = false;
        talk = true;
        dialogueText.text = "Hey Player. Wanna go back to the arena?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Press \"y\" for yes, \"n\" for no";
        yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
        if (Input.GetKey("y"))
        {
            player.transform.position = new Vector2(12.4f, -13.2f);
        }
        dialogueBox.SetActive(false);
        talk = false;
        player.GetComponent<Player>().canMove = true;
    }
}
