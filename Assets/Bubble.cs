using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    private GameObject player;
    private int buffer;
    private Collider2D _collider;
    private bool triggered = false;
    private SpriteRenderer sr;
    public bool playerInArena;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        _collider = GetComponent<Collider2D>();
        transform.position = player.transform.position;
        sr = GetComponent<SpriteRenderer>();

        // Bubble is bought in shop, this is not an arena, thus, the bubble has to be transparent
        fadeout();
        playerInArena = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position;

        if (triggered || !playerInArena)
        {
            _collider.enabled = false;
        }
        if (!triggered)
        {
            _collider.enabled = true;
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
                    StartCoroutine(untrigger());
                    break;
                case "DatDolan":
                    collision.GetComponent<DatDolan>().health -= 1;
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
                case "Doge":
                    collision.GetComponent<Doge>().health -= 1;
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
                case "Dolan":
                    collision.GetComponent<Dolan>().health -= 1;
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
                case "NyanCat":
                    collision.GetComponent<NyanCat>().health -= 1;
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
                case "NyanDoge":
                    collision.GetComponent<NyanDoge>().health -= 1;
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
                case "NyanDogeCat":
                    collision.GetComponent<NyanDogeBroken>().health -= 1;
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
                case "NyanDogeDoge":
                    collision.GetComponent<NyanDogeBroken>().health -= 1;
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
                case "Trollface":
                    collision.GetComponent<Trollface>().health -= 1;
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
                case "Turtle":
                    collision.GetComponent<Turtle>().health -= 1;
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
                case "EnemyBullet":
                    Destroy(collision.gameObject);
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
                case "MainEnemyProjectile":
                    Destroy(collision.gameObject);
                    triggered = true;
                    StartCoroutine(untrigger());
                    break;
            }
        }
    }

    IEnumerator untrigger()
    {
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
