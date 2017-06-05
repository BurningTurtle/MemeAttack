using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatDolan : MonoBehaviour
{

    [SerializeField] GameObject PileOfCottonPrefab;
    private GameObject player;
    [SerializeField] private int speed;
    private float deltaX;
    private float lastposition;
    private SpriteRenderer sr;
    private bool canDrop = true;
    [SerializeField] private int health = 1000;
    private bool alive = true;
    [SerializeField] GameObject featherPrefab;
    private int featherSpeed = 20;
    private bool canShootNext = true;
    private Animator anim;
    private bool canMove = true;
    private bool canAttackRedEyes = true;
    [SerializeField] GameObject featherShuurikenPrefab;
    [SerializeField] GameObject dolanPrefab, datBoiPrefab;
    private bool canCallForHelp = true;
    [SerializeField] GameObject keyPrefab;

    private GameObject statue;
    [SerializeField]
    private Sprite statueActivated;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        statue = GameObject.Find("datdolanStatue1");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive)
        {
            if (canMove)
            {
                Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
                playerVector.Normalize();
                GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;

                deltaX = transform.position.x - lastposition;
                lastposition = transform.position.x;

                if (deltaX < 0)
                {
                    sr.flipX = false;
                }
                else if (deltaX > 0)
                {
                    sr.flipX = true;
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }

            if (health > 700)
            {
                if (canDrop)
                {
                    StartCoroutine(drop());
                }
            }

            if (health <= 700 && health > 500)
            {
                if (canShootNext)
                {
                    StartCoroutine(shootFeather());
                }
                if (canDrop)
                {
                    StartCoroutine(drop());
                }
            }

            if (health <= 500 && health > 200)
            {
                if (canAttackRedEyes)
                {
                    StartCoroutine(redEyes());
                }
                if (canShootNext)
                {
                    StartCoroutine(shootFeather());
                }
            }

            if (health <= 200 && health > 0)
            {
                if (canCallForHelp)
                {
                    StartCoroutine(callForHelp());
                }
                if (canDrop)
                {
                    StartCoroutine(drop());
                }
                if (canShootNext)
                {
                    StartCoroutine(shootFeather());
                }
            }
        }
    }

    IEnumerator callForHelp()
    {
        // Cool animation here
        Debug.Log("HALP");

        canCallForHelp = false;

        GameObject dolan1 = Instantiate(dolanPrefab) as GameObject;
        GameObject dolan2 = Instantiate(dolanPrefab) as GameObject;
        GameObject datBoi = Instantiate(datBoiPrefab) as GameObject;

        dolan1.transform.position = new Vector2(transform.position.x - 2, transform.position.y);
        dolan2.transform.position = new Vector2(transform.position.x + 2, transform.position.y);
        datBoi.transform.position = new Vector2(transform.position.x, transform.position.y + 2);

        yield return new WaitForSeconds(10);
        canCallForHelp = true;
    }

    IEnumerator redEyes()
    {
        canMove = false;
        canAttackRedEyes = false;
        anim.SetBool("redEyes", true);
        yield return new WaitForSeconds(1.3f);
        GameObject featherShuuriken = Instantiate(featherShuurikenPrefab) as GameObject;
        featherShuuriken.transform.position = transform.position;
        canMove = true;
        anim.SetBool("redEyes", false);
        yield return new WaitForSeconds(10);
        canAttackRedEyes = true;
    }

    IEnumerator shootFeather()
    {
        canShootNext = false;
        GameObject feather = Instantiate(featherPrefab) as GameObject;
        Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
        playerVector.Normalize();

        // Feather is moved up by 1 so that it looks like DatDolan spits it out
        feather.transform.position = new Vector2(transform.position.x, transform.position.y + 1f);

        feather.GetComponent<Rigidbody2D>().velocity = playerVector * featherSpeed;
        yield return new WaitForSeconds(1);
        canShootNext = true;
    }


    IEnumerator drop()
    {
        canDrop = false;
        GameObject pileOfCotton = Instantiate(PileOfCottonPrefab) as GameObject;
        if (deltaX < 0)
        {
            pileOfCotton.transform.position = new Vector2(transform.position.x - 1f, transform.position.y - 2);
        }
        else if (deltaX > 0)
        {
            pileOfCotton.transform.position = new Vector2(transform.position.x + 1f, transform.position.y - 2);
        }
        else if (deltaX == 0)
        {
            pileOfCotton.transform.position = new Vector2(transform.position.x, transform.position.y - 2);
        }
        yield return new WaitForSeconds(5);
        canDrop = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            health = health - FindObjectOfType<Player>().damage;
            if (health <= 0)
            {
                StartCoroutine(die());
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator die()
    {
        for (float f = 1; f >= 0; f -= 0.1f)
        {
            Color colour = sr.material.color;
            colour.a = f;
            sr.material.color = colour;
            yield return null;
        }

        alive = false;

        GameObject pileOfCotton = Instantiate(PileOfCottonPrefab) as GameObject;
        pileOfCotton.transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y - 2);

        GameObject key = Instantiate(keyPrefab) as GameObject;
        key.transform.position = transform.position;
        statue.GetComponent<SpriteRenderer>().sprite = statueActivated;
        Destroy(this.gameObject);
    }
}
