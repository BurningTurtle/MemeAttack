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
    private SoundManager soundMan;

    private bool introduced = false;
    private bool introductionFinished = false;
    private bool talk = false;


    // Use this for initialization
    void Start () {
        dialogueBox.SetActive(false);
        player = GameObject.Find("Player");
        soundMan = FindObjectOfType<SoundManager>();
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
        introduced = true;

        dialogueText.color = Color.white;
        dialogueText.text = "Hey! I welcome you as well.";
        soundMan.playAudioClip("GalleryClerkHappy");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Odds are high that you've already met my twin brother. He's the salesclerk.";
        soundMan.playAudioClip("GalleryClerkWondering");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "My job is to guard our Gallery.";
        soundMan.playAudioClip("GalleryClerkHappy");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Here you can see every Meme inside this simulation portrayed as a grey statue.";
        soundMan.playAudioClip("GalleryClerkWondering");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Once you've killed at least one of its kind, the statue will assume colour.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Then you'll also see the Meme's name.";
        soundMan.playAudioClip("GalleryClerkHappy");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "If you'd like to go back, just talk to me. Have a nice time!";
        soundMan.playAudioClip("GalleryClerkGoodbye");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);
        introductionFinished = true;
    }

    IEnumerator back()
    {
        talk = true;
        dialogueText.text = "Wanna go back?";
        soundMan.playAudioClip("GalleryClerkWondering");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Press \"y\" for yes, \"n\" for no.";
        soundMan.playAudioClip("galleryClerk");
        yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
        if (Input.GetKey("y"))
        {
            player.transform.position = new Vector2(12.4f, -13.2f);
            soundMan.playAudioClip("Teleport");
        }
        dialogueBox.SetActive(false);
        talk = false;
    }
}
