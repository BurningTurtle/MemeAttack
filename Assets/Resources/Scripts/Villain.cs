using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Villain : MonoBehaviour {

    private GameObject hubworldCtrl;
    private bool canTalk = true;
    private SoundManager soundMan;
    private Text dialogueText;
    private GameObject dialogueBox;
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
        dialogueBox = GameObject.Find("GameObjectManager").GetComponent<GameObjectManager>().dialogueBox;
        dialogueText = GameObject.Find("GameObjectManager").GetComponent<GameObjectManager>().dialogueText;
        dialogueBox.SetActive(false);
        villainParticles = GameObject.Find("VillainParticles");
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void startTalking()
    {
        if (canTalk)
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

        dialogueText.text = "I'm sorry for absorbing all the color, my presence is too strong.";
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
        sr.sprite = villainSmiling;
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
        sr.sprite = villainAngry;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "I'm speechless. I have no clue what's going on!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Oh? It appears you've lost your memory.";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Let my little friend help you remember!";
        soundMan.playAudioClip("VillainAttack2");
        sr.sprite = villainAngry;
        yield return new WaitForSeconds(0.5f);
        sr.sprite = villainAngry;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);

        soundMan.playAudioClip("VillainAttack1");
        GameObject turtle = Instantiate(turtlePrefab) as GameObject;
        turtle.transform.position = new Vector2(transform.position.x, transform.position.y + 10);



        StartCoroutine(fadeVillainOut());

        

        
    }

    IEnumerator turtleDead()
    {
        
        
        StartCoroutine(fadeVillainIn());

        canTalk = false;

        yield return new WaitForSeconds(1);

        dialogueBox.SetActive(true);

        dialogueText.text = "You did it.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "You've truly beaten my turtle...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "That's a sign of true strength.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "I see, you've grown mentally. Take this key and go home.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "As long as it's not too late...";
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(fadeVillainOut());
        yield return new WaitForSeconds(0.5f);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "WAIT!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        StartCoroutine(fadeVillainIn());

        dialogueText.color = Color.white;
        dialogueText.text = "Sure, I have time.";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "Why did you do all this...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "I wanted to teach you a lesson.";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Don't you remember what you did to your family?!";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "....";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "They just wanted to have fun with you.";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Camping, swimming, going on vacation and so on.";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "And you? You sat in front of your PC the whole day!";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "For years!";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Can't you imagine how they feel like?!";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "I... I've never realized...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "You fool! I hope you've learned your lesson!";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Now go back and do something with them. Even though they've grown old...";
        soundMan.playAudioClip("VillainArrogant");
        sr.sprite = villainNormal;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);

        StartCoroutine(fadeVillainOut());
        
    }

    IEnumerator collapse()
    {
        StartCoroutine(fadeVillainIn());

        canTalk = false;

        yield return new WaitForSeconds(1);

        dialogueBox.SetActive(true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "WHAT IS HAPPENING?!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "I dont know! My simulation seems to collapse!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Hurry! Run back to the portal!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

      
        dialogueBox.SetActive(false);

        StartCoroutine(fadeVillainOut());

        yield return new WaitForSeconds(20);
        soundMan.playAudioClip("Teleport");
        player.transform.position = transform.position = new Vector2(12.3f, -13.2f);

        yield return new WaitForSeconds(10);
        soundMan.playAudioClip("Teleport");
        player.transform.position = transform.position = new Vector2(12.3f, -13.2f);

        yield return new WaitForSeconds(5);
        soundMan.playAudioClip("Teleport");
        player.transform.position = transform.position = new Vector2(12.3f, -13.2f);

        yield return new WaitForSeconds(4);
        soundMan.playAudioClip("Teleport");
        player.transform.position = transform.position = new Vector2(12.3f, -13.2f);

        yield return new WaitForSeconds(3);
        soundMan.playAudioClip("Teleport");
        player.transform.position = transform.position = new Vector2(12.3f, -13.2f);

        yield return new WaitForSeconds(2);
        soundMan.playAudioClip("Teleport");
        player.transform.position = transform.position = new Vector2(12.3f, -13.2f);
        
        yield return new WaitForSeconds(1);
        soundMan.playAudioClip("Teleport");
        player.transform.position = transform.position = new Vector2(12.3f, -13.2f);


        for(int i = 0; i < 50; i++)
        {
            int ranX = Random.Range(-61, 26);
            int ranY = Random.Range(-28, 330);
            Vector2 pos = new Vector2(ranX, ranY);
            soundMan.playAudioClip("Teleport");
            player.transform.position = pos;
            yield return new WaitForSeconds(0.1f);
        }

        player.transform.position = transform.position = new Vector2(12.3f, -13.2f);

        StartCoroutine(fadeVillainIn());

        canTalk = false;

        yield return new WaitForSeconds(1);

        dialogueBox.SetActive(true);

        dialogueText.color = Color.white;
        dialogueText.text = "My simulation... It's out of control!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "AAAAAAH I have to leave immediately!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "It was nice to meet you.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);

        StartCoroutine(fadeVillainOut());
    }

    IEnumerator fadeVillainIn()
    {
        transform.position = player.transform.position + new Vector3(0, 3, 0);
        villainParticles.GetComponent<PlayerParticleSystem>().enableParticleSystem();
        for (float f = 0f; f <= 1; f += 0.025f)
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

    IEnumerator fadeVillainOut()
    {
        villainParticles.GetComponent<PlayerParticleSystem>().enableParticleSystem();
        for (float f = 1f; f >= 0; f -= 0.025f)
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
        transform.position = new Vector2(150, 450);
    }

    public void afterTurtle()
    {
        StartCoroutine(turtleDead());
    }
    
    public void villainTalkAboutCollapse()
    {
        StartCoroutine(collapse());
    }
}
