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

    private SoundManager soundMan;

    // Use this for initialization
    void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.lineSpacing = 1.25f;
        player = GameObject.Find("Player");
        soundMan = GameObject.FindObjectOfType<SoundManager>();
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

        dialogueText.text = "Hey Player. I welcome you.";
        soundMan.playAudioClip("ShopClerkSurprised");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "How do you know my name ?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "I dont know if you've noticed yet but let me make something abundantly clear.";
        soundMan.playAudioClip("ShopClerkSympathetic"); 
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "We are inside a simulation.";
        soundMan.playAudioClip("ShopClerkSurprised");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "Oh, well. Why not.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Everything in here consists of code which I can read and manipulate, hence there is nothing I don't know.";
        soundMan.playAudioClip("ShopClerkWondering");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "I do this for a living. So if you want me to manipulate some code in your favor, just pick the upgrade of your choice and I'll manipulate some code for you.";
        soundMan.playAudioClip("ShopClerkSympathetic");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "If you have enough yen, of course.";
        soundMan.playAudioClip("ShopClerkWondering");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);
        introductionFinished = true;
    }

    IEnumerator back()
    {
        talk = true;
        dialogueText.text = "Hey Player. Do you wanna go back to the arena?";
        soundMan.playAudioClip("ShopClerkSurprised");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Press \"y\" for yes, \"n\" for no.";
        yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
        if(Input.GetKey("y"))
        {
            player.transform.position = new Vector2(12.4f, -13.2f);
            soundMan.playAudioClip("Teleport");
        }
        dialogueBox.SetActive(false);
        talk = false;
    }
}
