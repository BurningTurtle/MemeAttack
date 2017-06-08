using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopClerk : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private Text dialogueText;
    private GameObject player;

    private bool introduced = false;
    private bool introductionFinished = false;
    private bool talk = false;

    // Use this for initialization
    void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.lineSpacing = 1.25f;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            if(Vector2.Distance(transform.position, player.transform.position) < 3) {
                if(!introduced)
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
        introduced = true;
        player.GetComponent<Player>().canMove = false;
        dialogueText.text = "Hey Player. I welcome you.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "How do you know my name ?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "I dont know if you've noticed yet but let me make something abundantly clear.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "We are inside a simulation.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Oh, well. Why not.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Everything in here consists of code which I can read and manipulate, hence there is nothing I don't know.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "I do this for a living. So if you want me to manipulate some code in your favor, just pick the upgrade of your choice and I'll manipulate some code for you.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "If you have enough yen, of course.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);
        player.GetComponent<Player>().canMove = true;
        introductionFinished = true;
    }

    IEnumerator back()
    {
        talk = true;
        player.GetComponent<Player>().canMove = false;
        dialogueText.text = "Hey Player. Do you wanna go back to the arena?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Press \"y\" for yes, \"n\" for no";
        yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
        if(Input.GetKey("y"))
        {
            player.transform.position = new Vector2(12.4f, -13.2f);
        }
        dialogueBox.SetActive(false);
        talk = false;
        player.GetComponent<Player>().canMove = true;
    }
}
