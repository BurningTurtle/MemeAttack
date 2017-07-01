﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    // Show this variable in inspector
    public float speed = 3f;
    public int damage;
    public int health;

    public bool readyForDamage = true;

    private SpriteRenderer sr;
    public Animator anim;
    [SerializeField] private GameObject projectilePrefab;
    public int crazy = 0;

    [SerializeField] private Sprite link;
    [SerializeField] private GameObject MasterSword;

    public bool isLink, isDarkLink;
    private bool hasSword;
    private bool attack;
    public float bass;

    public bool hasKey = false;
    public bool hasCoinMagnet = false;

    public Collider2D normalCollider;
    public Collider2D linkCollider;

    // Kleines Yen
    public static int kleinesYen;
    private SoundManager soundMan;

    [SerializeField]
    private GameObject special1HUD;
    private GameObject special1Controller;

    [SerializeField]
    private GameObject dialogueBox;

    private GameObject hubworldController;

    [SerializeField]
    private GameObject arenaController, arena2Controller, arena3Controller, tunnelController, villainCtrl;

    // This is for bubble
    private Bubble bubble = null;

    public bool dead;

    [SerializeField]
    private Text dialogueText;
    public bool canTalkToMentor;

    private bool canMove = true;

    [SerializeField] private GameObject keyPrefab, alternateKeyTrigger, particles;
    public bool triedToCollectKey = false;
    private CameraShaking camShake;
    public bool openedPortalRoom = false;
    private GameObject portal;


    private void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        soundMan = FindObjectOfType<SoundManager>();

        dialogueBox.SetActive(false);
        if(Application.loadedLevelName == "Arena")
        {
            special1Controller = GameObject.Find("Special1Controller");
            hubworldController = GameObject.Find("HubworldController");
            //StartCoroutine(whatsGoingOn());
        }
        else if (Application.loadedLevelName == "Arena 1")
        {
            StartCoroutine(whatsGoingOn1());
            particles.GetComponent<ParticleSystem>().Stop();
            camShake = FindObjectOfType<CameraShaking>();
            portal = GameObject.Find("Big Portal");
        }

    }

    private void Update()
    {
        // Uncomment this for testing
        health = 100;

        if(dialogueBox.activeSelf == true && Input.GetKeyDown("e"))
        {
            soundMan.playAudioClip("DialoguePress");
        }

        if(Application.loadedLevelName == "Arena")
        {
            if (hubworldController.GetComponent<HubworldController>().area == "special2" || hubworldController.GetComponent<HubworldController>().area == "special1")
            {
                health = 100;
            }
        }

        float[] spectrum = AudioListener.GetSpectrumData(64, 0, FFTWindow.Hamming);
        bass = spectrum[0] + spectrum[1] + spectrum[2] + spectrum[3] + spectrum[4] + spectrum[5];

        //Debug.Log("Bass." + bass);

        if (health > 100)
        {
            health = 100;
        }

        float xValue = Input.GetAxis("Horizontal") * speed;
        float yValue = Input.GetAxis("Vertical") * speed;
        Vector2 movement = new Vector2(xValue, yValue);


        // Limit diagonal movement to the same speed as movement along an axis.
        movement = Vector2.ClampMagnitude(movement, speed);

        movement *= Time.deltaTime;

        if (canMove)
        {
            if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && dialogueBox.activeSelf == false)
            {
                // Switches from idle to walk animation.
                anim.SetBool("isWalking", true);
                transform.Translate(movement);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isDarkLink)
            {
                StartCoroutine(shoot());
            }
            else
            {

            }

        }

        if (health <= 0)
        {
            health = 100;
            StartCoroutine(undeadify());
            if (hubworldController.GetComponent<HubworldController>().area == "arena1")
            {
                arenaController.GetComponent<ArenaController>().resetWaves();
            }
            else if (hubworldController.GetComponent<HubworldController>().area == "arena2")
            {
                arena2Controller.GetComponent<Arena2Controller>().resetWaves();
            }
            else if(hubworldController.GetComponent<HubworldController>().area == "arena3")
            {
                arena3Controller.GetComponent<Arena3Controller>().resetWaves();
            }
            else if (hubworldController.GetComponent<HubworldController>().area == "tunnel")
            {
                tunnelController.GetComponent<TunnelController>().diedInTunnel();
            }
            else if(hubworldController.GetComponent<HubworldController>().area == "finalRoom")
            {
                villainCtrl.GetComponent<VillainArenaController>().diedInVillainArena();
            }
            transform.position = new Vector2(12.5f, -13);
            if(bubble != null)
            {
                bubble.GetComponent<Bubble>().fadeout();
            }
        }

        if (!hasSword && isDarkLink)
        {
            MasterSword = Instantiate(MasterSword) as GameObject;
            MasterSword.transform.Rotate(0, 0, -90);
            MasterSword.transform.localScale += new Vector3(0.3f, 0.3f, 0);

            hasSword = true;
        }

        if (isDarkLink)
        {
            MasterSword.transform.position = new Vector2(transform.position.x + .3f, transform.position.y - .2f);
        }
    }

    public void startPortalFixing()
    {
        StartCoroutine(portalFixing());
    }

    IEnumerator portalFixing()
    {
        yield return new WaitForSeconds(1);

        dialogueBox.SetActive(true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "Are you still there!?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "The portal is still broken!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Oh no! I totally forgot about that!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        yield return new WaitForSeconds(1);

        dialogueBox.SetActive(true);
        dialogueText.text = "Let me see ... ";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        portal.GetComponent<SpriteRenderer>().enabled = false;
        portal.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3);

        dialogueBox.SetActive(true);
        dialogueText.text = "God dammit.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "WHAT ARE YOU DOING!?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        yield return new WaitForSeconds(3);
        portal.GetComponent<SpriteRenderer>().enabled = true;

        for (int i = 0; i <= 10; i++)
        {
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = portal.GetComponent<SpriteRenderer>().color;
                colour.a -= 0.05f;
                portal.GetComponent<SpriteRenderer>().color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = portal.GetComponent<SpriteRenderer>().color;
                colour.a += 0.05f;
                portal.GetComponent<SpriteRenderer>().color = colour;
                yield return new WaitForSeconds(0.01f);
            }
        }

        dialogueBox.SetActive(true);
        dialogueText.color = Color.white;
        dialogueText.text = "Now it should work!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "It was nice meeting you, Player.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "All of us in here will miss you. Have a good time with your family.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Bye.";
        soundMan.playAudioClip("ShopClerkSympathetic");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        yield return new WaitForSeconds(1);

        portal.GetComponent<Teleport>().canFinallyEnter = true;
        portal.GetComponent<Collider2D>().enabled = true;
    }

    public void finalDialogue()
    {
        StartCoroutine(startFinalDialogue());
    }

    IEnumerator startFinalDialogue()
    {
        dialogueBox.SetActive(true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "I have no key!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "I don't wanna die in here!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        yield return new WaitForSeconds(5);

        GameObject key = Instantiate(keyPrefab, new Vector3(transform.position.x - 10, transform.position.y + 1, transform.position.z), Quaternion.identity);
        key.GetComponent<Collider2D>().enabled = false;
        soundMan.playAudioClip("Key");
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 10; i++)
        {
            int ranX = Random.Range(-5, 5);
            int ranY = Random.Range(-5, 5);
            key.transform.position = new Vector2(transform.position.x + ranX, transform.position.y + ranY);
            soundMan.playAudioClip("Teleport");
            yield return new WaitForSeconds(0.4f);
        }

        soundMan.playAudioClip("Teleport");
        key.transform.position = new Vector2(13, 343);
        yield return new WaitForSeconds(3);

        dialogueBox.SetActive(true);
        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "I am screwed.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        yield return new WaitForSeconds(3);
        
        for (int i = 0; i < 50; i++)
        {
            int ranX = Random.Range(-5, 5);
            int ranY = Random.Range(-5, 5);
            key.transform.position = new Vector2(transform.position.x + ranX, transform.position.y + ranY);
            soundMan.playAudioClip("Teleport");
            yield return new WaitForSeconds(0.1f);
        }

        key.transform.position = new Vector2(13, 343);
        soundMan.playAudioClip("Teleport");

        yield return new WaitForSeconds(10);

        for (int i = 0; i < 30; i++)
        {
            soundMan.playAudioClip("Teleport");
            yield return null;
        }

        key.transform.position = new Vector2(12, 333);
        key.GetComponent<SpriteRenderer>().flipY = true;
        alternateKeyTrigger.transform.position = key.transform.position;
        soundMan.playAudioClip("Teleport");

        dialogueBox.SetActive(true);
        dialogueText.color = Color.white;
        dialogueText.text = "Psst! In front of the portal room!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        yield return new WaitUntil(() => Input.GetKeyDown("w") == true || Input.GetKeyDown("a") == true || Input.GetKeyDown("s") == true || Input.GetKeyDown("d") == true);
        dialogueBox.SetActive(true);
        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "... who was that?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        GameObject.Find("AlternateKeyTrigger").GetComponent<FinalTrigger>().canTrigger = true;

        yield return new WaitUntil(() => triedToCollectKey == true);

        yield return new WaitForSeconds(20);

        transform.position = new Vector2(key.transform.position.x, key.transform.position.y - 3.5f);
        soundMan.playAudioClip("Teleport");
        yield return new WaitForSeconds(0.1f);

        soundMan.playAudioClip("ShopClerkSympathetic");
        dialogueBox.SetActive(true);
        dialogueText.color = Color.white;
        dialogueText.text = "(you stay here...) Hey there, hope you remember me!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "The shop clerk!?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "That's right! Would you take that key already? I didn't give you that for nothing!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "Awesome! You're helping me?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Of course I am! Take the key, we don't have much time!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "But I can't! It doesn't work!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Oops, hold on!";
        soundMan.playAudioClip("ShopClerkSurprised");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "...";

        yield return new WaitForSeconds(1);
        particles.transform.position = key.transform.position;
        particles.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3);
        particles.GetComponent<ParticleSystem>().Stop();

        dialogueText.text = "Try again now!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        key.GetComponent<Collider2D>().enabled = true;
        dialogueBox.SetActive(false);

        yield return new WaitUntil(() => hasKey == true);
        dialogueBox.SetActive(true);
        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "Awesome! It worked! Thank you so much!";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.color = Color.white;
        dialogueText.text = "Get to the portal already!!!";
        camShake.ShakeCamera(2f, 2f);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);
    }

    IEnumerator whatsGoingOn1()
    {
        yield return new WaitForSeconds (3);
        dialogueBox.SetActive(true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "What the heck!?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        // Give Player some time inside teleport room
        yield return new WaitForSeconds(15);

        soundMan.playAudioClip("Teleport");
        transform.position = new Vector2(0, 0);
        yield return new WaitForSeconds(1);

        soundMan.playAudioClip("Teleport");
        transform.position = new Vector2(0, 13);
        yield return new WaitForSeconds(1);

        soundMan.playAudioClip("Teleport");
        transform.position = new Vector2(12.3f, -13.2f);

        GameObject villain = GameObject.Find("Villain");
        villain.GetComponent<Villain>().villainTalkAboutCollapse();
    }

    IEnumerator whatsGoingOn()
    {
        yield return new WaitUntil(() => (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d")) == true);
        yield return new WaitForSeconds(0.75f);
        dialogueBox.SetActive(true);

        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "Umm...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "What's going on?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "Where the hell am I?";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueText.text = "And why is that creepy old dude at the end of the hall staring at me ...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);
    }

    public void StartFreakingOut()
    {
        StartCoroutine(freakingOut());
    }

    IEnumerator freakingOut()
    {
        canTalkToMentor = false;

        yield return new WaitUntil(() => (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d")) == true);
        canMove = false;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2);
        yield return new WaitForSeconds(3f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        anim.SetBool("isWalking", false);

        dialogueBox.SetActive(true);
        dialogueText.color = new Color(0, 202, 232);
        dialogueText.text = "There must be a way out.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        anim.SetBool("isWalking", true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2) * 3;
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        anim.SetBool("isWalking", false);

        dialogueBox.SetActive(true);
        dialogueText.text = "...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        anim.SetBool("isWalking", true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2) * 1.5f;
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        anim.SetBool("isWalking", false);

        dialogueBox.SetActive(true);
        dialogueText.text = "...";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        dialogueBox.SetActive(true);
        dialogueText.text = "Ugh.";
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);

        canMove = true;
        canTalkToMentor = true;
    }

    IEnumerator undeadify()
    {
        dead = true;
        yield return null;
        dead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (readyForDamage)
        {
            switch (collision.gameObject.tag)
            {
                // Apply colliding damage according to enemy.
                case "MainEnemy":
                    health -= 5;
                    StartCoroutine(getReadyForDamage());
                    break;
                case "Dolan":
                    health -= 5;
                    StartCoroutine(getReadyForDamage());
                    break;
                case "DatBoi":
                    health -= 5;
                    StartCoroutine(getReadyForDamage());
                    break;
                case "Doge":
                    health -= 5;
                    StartCoroutine(getReadyForDamage());
                    break;
                case "Turtle":
                    health -= 20;
                    StartCoroutine(getReadyForDamage());
                    break;
                case "DatDolan":
                    if (collision.gameObject.GetComponent<DatDolan>().activated)
                    {
                        StartCoroutine(getReadyForDamage());
                        health -= 20;
                    }
                    break;
                case "NyanDogeDoge":
                    StartCoroutine(getReadyForDamage());
                    health -= 10;
                    break;
                case "NyanDogeCat":
                    StartCoroutine(getReadyForDamage());
                    health -= 10;
                    break;
            }
            if (collision.gameObject.tag == "NyanCat")
            {
                health -= 20;
                StartCoroutine(getReadyForDamage());
                Destroy(collision.gameObject);
            }
        }
    }

    public void GetReadyForDamage()
    {
        StartCoroutine(getReadyForDamage());
    }

    IEnumerator getReadyForDamage()
    {
        if(hubworldController.GetComponent<HubworldController>().area != "special1" && hubworldController.GetComponent<HubworldController>().area != "special2")
        {
            readyForDamage = false;
            soundMan.playAudioClip("PlayerDamage");

            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = sr.color;
                colour.a -= 0.05f;
                sr.color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = sr.color;
                colour.a += 0.05f;
                sr.color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = sr.color;
                colour.a -= 0.05f;
                sr.color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = sr.color;
                colour.a += 0.05f;
                sr.color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = sr.color;
                colour.a -= 0.05f;
                sr.color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = sr.color;
                colour.a += 0.05f;
                sr.color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = sr.color;
                colour.a -= 0.05f;
                sr.color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = sr.color;
                colour.a += 0.05f;
                sr.color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = sr.color;
                colour.a -= 0.05f;
                sr.color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            for (float f = 1; f >= 0.1; f -= 0.1f)
            {
                Color colour = sr.color;
                colour.a += 0.05f;
                sr.color = colour;
                yield return new WaitForSeconds(0.01f);
            }
            readyForDamage = true;
        }
    }

    IEnumerator shoot()
    {
        // Get mouse position.
        Vector3 mousePos = Input.mousePosition;

        // Get player position.
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        // Get mouse position relative to the player.
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        // Convert x and y into an angle using built in functions.
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // Creating a direction vector for our projectile.
        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

        // Creating the projectile and move it into the player.
        GameObject projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = this.transform.position;

        // Add the calculated force.
        projectile.GetComponent<Rigidbody2D>().AddForce(dir / 10);

        // Destory the projectile if it didn't hit anything after 2 seconds.
        yield return new WaitForSeconds(2f);
        Destroy(projectile.gameObject);
    }

    public void transformToLink()
    {
        anim.SetBool("isLink", true);
        isLink = true;
        StartCoroutine(waitForDarkLink());
        transform.localScale += new Vector3(.25f, .25f, 0);
        Debug.Log("transformed");
        normalCollider.enabled = false;
    }

    IEnumerator waitForDarkLink()
    {
        yield return new WaitForSeconds(34.2f);
        anim.SetBool("isLink", false);
        anim.SetBool("isDarkLink", true);
        yield return new WaitForSeconds(0.3f);
        special1HUD.SetActive(true);
        isDarkLink = true;
        isLink = false;
        yield return new WaitForSeconds(181);
        isDarkLink = false;
        transform.localScale -= new Vector3(.25f, .25f, 0);
        anim.SetBool("isDarkLink", false);
        kleinesYen += Mathf.RoundToInt(special1Controller.GetComponent<Special1Controller>().critRate * 4000);
        special1Controller.GetComponent<Special1Controller>().canEscape();
        special1Controller.GetComponent<Special1Controller>().activateExit();
        normalCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "1Yen":
                kleinesYen += 1;
                soundMan.playAudioClip("Money");
                Destroy(collision.gameObject);
                break;
            case "5Yen":
                kleinesYen += 5;
                soundMan.playAudioClip("Money");
                Destroy(collision.gameObject);
                break;
            case "10Yen":
                kleinesYen += 10;
                soundMan.playAudioClip("Money");
                Destroy(collision.gameObject);
                break;
            case "50Yen":
                kleinesYen += 50;
                soundMan.playAudioClip("Money");
                Destroy(collision.gameObject);
                break;
            case "100Yen":
                kleinesYen += 100;
                soundMan.playAudioClip("Money");
                Destroy(collision.gameObject);
                break;
            case "500Yen":
                kleinesYen += 500;
                soundMan.playAudioClip("Money");
                Destroy(collision.gameObject);
                break;
        }
    }

    public int returnKleinesYen()
    {
        return kleinesYen;
    }

    public void payYen(int payment)
    {
        kleinesYen = kleinesYen - payment;
        soundMan.playAudioClip("Buy");
    }
}
