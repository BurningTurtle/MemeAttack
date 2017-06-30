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

    private bool talking = false;

    private ArenaController arenaController;

    private SoundManager soundMan;

    [SerializeField] GameObject mentorParticles;
    private SpriteRenderer sr;

    private bool disappeared = false;

    // Use this for initialization
    void Start () {
        dialogueBox.SetActive(false);
        player = GameObject.Find("Player");

        // Player should only be able to enter the Arena after talking to the Mentor
        arenaController = FindObjectOfType<ArenaController>();
        arenaController.activateCantEscape();

        sr = GetComponent<SpriteRenderer>();
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
                else if (!talking && introduced && player.GetComponent<Player>().canTalkToMentor && !disappeared)
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

        dialogueText.color = Color.white;
        dialogueText.text = "Haha! Feel lost?";
        soundMan.playAudioClip("MentorSurprised");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Worry not! This, my friend, is a simulation.";
        soundMan.playAudioClip("MentorWise");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "You seem upset. Try to calm down a little.";
        soundMan.playAudioClip("MentorThinking");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "I know, that thought doesn't really seem comforting at first. But let me tell you, I've been in here for quite a long time now.";
        soundMan.playAudioClip("MentorWeird");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Well, at least I think so ...";
        soundMan.playAudioClip("MentorWondering");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Anyway, there are also other people in here and they're all very nice. If you haven't already, why don't you have a look at the Gallery?";
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Oh, and don't forget to stop by at the shop. The clerk can surely give you some deeper insight into the simulation.";
        soundMan.playAudioClip("MentorWise");
       // yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Once you're done exploring our world, you can jump right into the first arena behind me.";
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Yes. I said first arena. So there must be more than one! There are also special areas which are super fun!";
        soundMan.playAudioClip("MentorWeird");
       // yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "In the arenas you can fight against a broad variety of funny and challenging - ...";
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "ENOUGH!";
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "I want to leave. Right now.";
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Oh. I'm afraid that won't work.";
        soundMan.playAudioClip("MentorSurprised");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "I have seen many people like you. But they all eventually entered the arena.";
        soundMan.playAudioClip("MentorWise");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "I wonder where they are ...";
        soundMan.playAudioClip("MentorWondering");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Enough said. I wish you good luck on your journey! And don't forget to visit the Gallery and the Shop!";
        soundMan.playAudioClip("MentorWeird");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "This is unbelievable! Let me go right now! I'm not even thinking about putting a foot inside your stupid arena. I'm gonna find a way out of here on my own.";
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "I see. Let me know when you've changed your mind. I'm then going to open the arena for you.";
        soundMan.playAudioClip("MentorWise");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);
        FindObjectOfType<HubworldController>().mentorIntroductionFinished = true;

        player.GetComponent<Player>().StartFreakingOut();
    }

    IEnumerator talk()
    {
        talking = true;
        dialogueText.color = Color.white;
        dialogueText.text = "Changed your mind?";
        soundMan.playAudioClip("MentorSurprised");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "I knew it. Feel free to enter!";
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        arenaController.deactivateCantEscape();

        dialogueText.text = "Oh, before you go, let me explain to you some more things. You don't wanna die in there, do you?";
        soundMan.playAudioClip("MentorWise");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "To kill the enemies, simply shoot at them. Some die faster than others.";
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Each enemy has its own special attack. Try to figure out patterns!";
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "You will earn money in there. In order to succeed in further arenas, you'll also have to spend some of it in the Shop. Otherwise it'll get pretty difficult.";
        soundMan.playAudioClip("MentorWise");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Sometimes, enemies drop items that help you! But I've heard Arena Bosses are immune to some!";
        soundMan.playAudioClip("MentorWise");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Oh, and the most important thing: try not to die!";
        soundMan.playAudioClip("MentorWeird");
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);
        talking = false;

        // So that a new dialoguebox can't be called right after he finished talking which would result in bugs
        disappeared = true;

        mentorParticles.GetComponent<PlayerParticleSystem>().enableParticleSystem();

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

        mentorParticles.GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject);
    }
}
