using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Villain : MonoBehaviour {

    private GameObject hubworldCtrl;
    private bool canTalk = true;
    private SoundManager soundMan;
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject dialogueBox;
    private SpriteRenderer sr;
    [SerializeField] private Sprite villainSmiling, villainAngry, villainNormal;

    [SerializeField] private GameObject turtlePrefab;

    private GameObject villainParticles;

    private GameObject player;

	// Use this for initialization
	void Start () {
        hubworldCtrl = GameObject.Find("HubworldController");
        sr = GetComponent<SpriteRenderer>();
        soundMan = FindObjectOfType<SoundManager>();
        dialogueBox.SetActive(false);
        villainParticles = GameObject.Find("VillainParticles");
	}
	
	// Update is called once per frame
	void Update () {
		if(hubworldCtrl.GetComponent<HubworldController>().area == "finalRoom" && canTalk)
        {
            StartCoroutine(talk());
        }
	}

    IEnumerator talk()
    {
        canTalk = false;

        yield return new WaitForSeconds(1);

        dialogueBox.SetActive(true);

        dialogueText.text = "So we meet again.";
        soundMan.playAudioClip("VillainArrogant");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "It's been a long time. It might not appear to you as if it has, but I'm confident when I say so.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "You're here, too!? What is all of this!?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Yes, silly. Don't you remember?";
        soundMan.playAudioClip("VillainLaugh");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "Remember what?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "You seem so cold!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Is this...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Yes, this is my simulation. Everything in here has been created by me.";
        soundMan.playAudioClip("VillainLaugh");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "So you are responsible for all this?! Why would you do that to -";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "HOW DARE YOU ASK THAT QUESTION!";
        soundMan.playAudioClip("VillainAttack2");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "I'm speechless. I have no clue what's going on!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Oh? It appears you've lost your memory.";
        soundMan.playAudioClip("VillainArrogant");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Let my little friend help you remember!";
        soundMan.playAudioClip("VillainAttack2");
        yield return new WaitForSeconds(0.5f);
        sr.sprite = villainAngry;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);

        soundMan.playAudioClip("VillainAttack1");
        GameObject turtle = Instantiate(turtlePrefab) as GameObject;
        turtle.transform.position = transform.position;

        villainParticles.GetComponent<PlayerParticleSystem>().enableParticleSystem();

        for (float f = 5f; f >= 0; f -= 0.1f)
        {
            // The colour of Villain's sprite.
            Color colour = sr.material.color;

            // Reduce colour's alpha by 0.1f for every f >= 0.
            colour.a = f;

            // Apply colour with new alpha value to DatBoi's SpriteRenderer Component.
            sr.material.color = colour;

            // Wait until next frame.
            yield return null;
        }

        villainParticles.GetComponent<ParticleSystem>().Stop();
    }
}
