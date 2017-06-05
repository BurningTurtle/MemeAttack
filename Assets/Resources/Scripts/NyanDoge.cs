using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyanDoge : MonoBehaviour
{


    // For rotation around player
    private float rotateSpeed = 1f;
    private float radius = 5f;
    private float _angle;
    private Vector2 _centre;
    private bool canMove = true;

    // Stuff for NyanDoge
    [SerializeField] private int health = 500;
    private GameObject player;
    private bool alive = true;
    private int speed = 200;
    private SpriteRenderer sr;
    private Animator anim;

    // Attacks
    private bool canShootNormal = true;
    private bool canShootRainbow = true;

    // Things to spawn
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private GameObject nyanDogeDogePrefab;
    [SerializeField] private GameObject nyanDogeCatPrefab;

    private GameObject statue;
    [SerializeField]
    private Sprite statueActivated;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        _centre = player.transform.position;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        statue = GameObject.Find("nyandogeStatue1");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive)
        {
            if (canMove)
            {
                // Rotate around player at all times
                _centre = player.transform.position;
                _angle += rotateSpeed * Time.deltaTime;
                var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * radius;
                transform.position = _centre + offset;
                Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
                playerVector.Normalize();
            }

            if(health > 300)
            {
                if (canShootNormal)
                {
                    StartCoroutine(shootNormal());
                }

                // Front towards player
                Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
                if (playerVector != Vector2.zero)
                {
                    float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                }
            }

            if(health <= 300 && health > 0)
            {
                // NyanDoge's Rainbow towards Player
                Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
                if (playerVector != Vector2.zero)
                {
                    float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                }

                // Bigger distance
                radius = 8f;

                if (canShootRainbow)
                {
                    StartCoroutine(shootRainbow());
                }
            }
        }
    }

    IEnumerator shootRainbow()
    {
        canShootRainbow = false;
        FindObjectOfType<RainbowShoot>().splash();
        yield return new WaitForSeconds(0.3f);
        canShootRainbow = true;
    }

    IEnumerator shootNormal()
    {
        canShootNormal = false;
        FindObjectOfType<PopTartLauncher>().launchPopTart();
        FindObjectOfType<WoofSpawner>().spawnWoof();
        yield return new WaitForSeconds(1);
        canShootNormal = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerProjectile")
        {
            health -= player.GetComponent<Player>().damage;

            if(health <= 0)
            {
                // NyanDoge GameObject gets destroyed at end of animation event
                anim.SetBool("Break", true);
                canMove = false;
                GameObject key = Instantiate(keyPrefab) as GameObject;
                key.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1);
                statue.GetComponent<SpriteRenderer>().sprite = statueActivated;

                player.GetComponent<Player>().crazy += 1;
                player.GetComponent<Player>().anim.SetInteger("Crazy", player.GetComponent<Player>().crazy);
            }

            Destroy(collision.gameObject);
        }
    }

    // Called at the end of animation
    public void broken()
    {
        GameObject nyanDogeDoge = Instantiate(nyanDogeDogePrefab) as GameObject;
        nyanDogeDoge.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
        nyanDogeDoge.transform.rotation = transform.rotation;

        GameObject nyanDogeCat = Instantiate(nyanDogeCatPrefab) as GameObject;
        nyanDogeCat.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
        nyanDogeCat.transform.rotation = transform.rotation;
        Destroy(this.gameObject);
    }
}
