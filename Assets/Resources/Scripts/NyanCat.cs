using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyanCat : MonoBehaviour {

    private bool alive;
    public float speed;
    GameObject player;
    public int health = 3;
    private SpriteRenderer sr;

    [SerializeField] GameObject yen1, yen5, yen10, yen50, yen100;

    // For It's time to stop
    public bool stop;

    private Animator anim;

    private GameObject statue;
    [SerializeField]
    private Sprite statueActivated;

    // Use this for initialization
    void Start()
    {
        alive = true;
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        statue = GameObject.Find("nyancatStatue1");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive && !stop)
        {
            // Get Vector to the player.
            Vector2 targetVelocity = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            targetVelocity.Normalize();
            GetComponent<Rigidbody2D>().velocity = targetVelocity * speed * Time.deltaTime;  
        }
        if (stop)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            anim.SetBool("moving", false);
        }

        Vector2 moveDirection = GetComponent<Rigidbody2D>().velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Substract 1 health if hit by PlayerProjectile GameObject.
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health = health - FindObjectOfType<Player>().damage; ;

            if (health <= 0)
            {
                // Coroutine because Wait Time is necessary.
                StartCoroutine(die());
            }

            Destroy(collision.gameObject);
        }

        if (collision.tag == "MasterSword")
        {
            if (player.GetComponent<Player>().isDarkLink && player.GetComponent<Player>().bass > 0.3f)
            {
                Debug.Log("crit");
                GameObject.Find("UIController").GetComponent<UIController>().showCrit();
                Destroy(gameObject);
            }
            else
            {
                health -= 3;
            }

            if (health <= 0 && alive)
            {
                // Coroutine because Wait Time is necessary.
                StartCoroutine(die());
            }
        }
    }

    IEnumerator die()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            // The colour of DatBoi's sprite.
            Color colour = sr.material.color;

            // Reduce colour's alpha by 0.1f for every f >= 0.
            colour.a = f;

            // Apply colour with new alpha value to DatBoi's SpriteRenderer Component.
            sr.material.color = colour;

            // Wait until next frame.
            yield return null;
        }

        // Kill NyanCat.
        alive = false;

        int ran = Random.Range(0, 100);
        if (ran < 50)
        {
            int ran1 = Random.Range(0, 100);

            if (ran1 < 10)
            {
                GameObject oneYen = Instantiate(yen1) as GameObject;
                oneYen.transform.position = transform.position;
            }
            else if (ran1 < 20)
            {
                GameObject fiveYen = Instantiate(yen5) as GameObject;
                fiveYen.transform.position = transform.position;
            }
            else if (ran1 < 80)
            {
                GameObject tenYen = Instantiate(yen10) as GameObject;
                tenYen.transform.position = transform.position;
            }
            else if (ran1 < 100)
            {
                GameObject fiftyYen = Instantiate(yen50) as GameObject;
                fiftyYen.transform.position = transform.position;
            }
        }

        statue.GetComponent<SpriteRenderer>().sprite = statueActivated;
        Destroy(this.gameObject);
    }
}
