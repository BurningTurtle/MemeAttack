using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    private GameObject player;
    private int buffer;
    private Collider2D _collider;
    private bool triggered = false;
    private SpriteRenderer sr;
    public bool playerInArena;
    private HubworldController[] hubWorldCtrl;
    private bool untriggerRunning;
    private SoundManager soundMan;

    // Coroutine Object so that Coroutine can be stopped
    Coroutine co;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        _collider = GetComponent<Collider2D>();
        transform.position = player.transform.position;
        sr = GetComponent<SpriteRenderer>();
        soundMan = FindObjectOfType<SoundManager>();

        // Bubble is bought in shop, this is not an arena, thus, the bubble has to be transparent
        fadeout();
        playerInArena = false;

        // For reference to bubble in HWController
        hubWorldCtrl = FindObjectsOfType<HubworldController>();

        for (int i = 0; i < hubWorldCtrl.Length; i++)
        {
            hubWorldCtrl[i].GetComponent<HubworldController>().getBubble();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        if (triggered || !playerInArena)
        {
            _collider.enabled = false;
        }
        if (!triggered)
        {
            _collider.enabled = true;
        }
        if (!playerInArena && untriggerRunning)
        {
            StopCoroutine(co);
            untriggerRunning = false;
            triggered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            switch (collision.gameObject.tag)
            {
                case "DatBoi":
                    collision.GetComponent<DatBoi>().health -= 1;
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "DatDolan":
                    collision.GetComponent<DatDolan>().health -= 1;
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "Doge":
                    collision.GetComponent<Doge>().health -= 1;
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "Dolan":
                    collision.GetComponent<Dolan>().health -= 1;
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "NyanCat":
                    collision.GetComponent<NyanCat>().health -= 1;
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "NyanDoge":
                    collision.GetComponent<NyanDoge>().health -= 1;
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "NyanDogeCat":
                    collision.GetComponent<NyanDogeBroken>().health -= 1;
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "NyanDogeDoge":
                    collision.GetComponent<NyanDogeBroken>().health -= 1;
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "Trollface":
                    collision.GetComponent<Trollface>().health -= 1;
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "Turtle":
                    collision.GetComponent<Turtle>().health -= 1;
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "EnemyBullet":
                    Destroy(collision.gameObject);
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
                case "MainEnemyProjectile":
                    Destroy(collision.gameObject);
                    triggered = true;
                    co = StartCoroutine(untrigger());
                    soundMan.playAudioClip("Bubble");
                    break;
            }
        }
    }

    IEnumerator untrigger()
    {
        untriggerRunning = true;

        for (float f = 1; f >= 0; f -= 0.1f)
        {
            Color colour = sr.material.color;
            colour.a = f;
            sr.material.color = colour;
            yield return null;
        }

        yield return new WaitForSeconds(5);
        triggered = false;

        for (float f = 0; f < 1; f += 0.1f)
        {
            Color colour = sr.material.color;
            colour.a = f;
            sr.material.color = colour;
            yield return null;
        }

        untriggerRunning = false;
    }

    IEnumerator fadeoutCoroutine()
    {
        for (float f = 1; f >= 0; f -= 0.1f)
        {
            Color colour = sr.material.color;
            colour.a = f;
            sr.material.color = colour;
            yield return null;
        }
    }

    IEnumerator fadeinCoroutine()
    {
        for (float f = 0; f < 1; f += 0.1f)
        {
            Color colour = sr.material.color;
            colour.a = f;
            sr.material.color = colour;
            yield return null;
        }
    }

    public void fadeout()
    {
        StartCoroutine(fadeoutCoroutine());
    }

    public void fadein()
    {
        StartCoroutine(fadeinCoroutine());
    }
}
