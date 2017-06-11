using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Duck : MonoBehaviour {

    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private Text dialogueText;
    private GameObject player;
    private SoundManager soundMan;
    private bool talking = false;
    private bool introduced = false;

    private Special2Controller special2Controller;

    // Use this for initialization
    void Start () {
        dialogueBox.SetActive(false);
        player = GameObject.Find("Player");
        soundMan = FindObjectOfType<SoundManager>();
        special2Controller = FindObjectOfType<Special2Controller>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e"))
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 3)
            {
                if (!talking)
                {
                    dialogueBox.SetActive(true);

                    if(special2Controller.solved == true)
                    {
                        StartCoroutine(talk3());
                    }
                    else if (!introduced)
                    {
                        StartCoroutine(talk1());
                    }
                    else
                    {
                        StartCoroutine(talk2());
                    }
                }

            }
        }
    }

    IEnumerator talk1()
    {
        talking = true;

        dialogueText.text = "Hey Partner, howdy-do?";
        soundMan.playAudioClip("DuckExcited");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "You've made it to the whoppin' world of Duckey's riddles!";
        soundMan.playAudioClip("DuckNormal");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "If you get this one right, there'll be a rich reward for you and your budgeting needs!";
        soundMan.playAudioClip("DuckExcited");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "But be warned!";
        soundMan.playAudioClip("DuckShort");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "You only have one chance to solve the riddle! Place a number on the wrong spot and you lose! There'll be no goin' back in time!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "So you gotta place them numbers real good, ya hear!";
        soundMan.playAudioClip("DuckShort");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "But I'm heckin' sure you'll succed if ye kept yer yapper shut in IT class!";
        soundMan.playAudioClip("DuckSympathetic");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        introduced = true;
        dialogueBox.SetActive(false);
        talking = false;
    }

    IEnumerator talk2()
    {
        talking = true;

        dialogueText.text = "I'm heckin' sure you'll succed if ye kept yer yapper shut in IT class!";
        soundMan.playAudioClip("DuckExcited");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "I sure as hell gave you enough hints boy!";
        soundMan.playAudioClip("DuckShort");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        talking = false;
        dialogueBox.SetActive(false);
    }

    IEnumerator talk3()
    {
        talking = true;

        dialogueText.text = "I knew you'd get it right this whole time!";
        soundMan.playAudioClip("DuckExcited");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Oh boy, you're heckin' rich now!";
        soundMan.playAudioClip("DuckShort");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        talking = false;
        dialogueBox.SetActive(false);

    }
}
