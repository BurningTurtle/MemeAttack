using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour {

    private GameObject player;
    private bool alive = true;
    [SerializeField] int maxHealth;
    public int health;
    public int speed;
    public float shootPower;
    private int phase;
    private bool phase2init, phase3init, phase4init;
    private bool canShootNext = true;

    [SerializeField]
    private GameObject projectilePrefab;
    private GameObject projectile;

    // Keep track of which direction Doge is moving to. Important for animation.
    private float deltaX;
    private float lastPosition;

    private SpriteRenderer sr;
    private Animator anim;

    // For It's time to stop
    public bool stop = false;

    [SerializeField]
    private GameObject money;

    // Use this for initialization
    void Start ()
    {
        phase = 1;
        player = GameObject.Find("Player");
        lastPosition = transform.position.x;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("Turtle HP:" + health);
        if (phase == 1 && alive && !stop)
        {
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            playerVector.Normalize();
            GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;

            // Get "distance" between enemy and player.
            Vector2 trueDistance = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            float combinedDistance = Mathf.Abs(trueDistance.x) + Mathf.Abs(trueDistance.y);
            //Debug.Log("combined distance" + combinedDistance);
            if (canShootNext && !stop)
            {
                Debug.Log("shoot!");
                StartCoroutine(shootPhase1(playerVector));
            }

            // Get deltaX for current position.
            deltaX = lastPosition - transform.position.x;

            // Last position for the next frame is the current position from now.
            lastPosition = transform.position.x;

            // If Doge is moving right
            if (deltaX < 0)
            {
                // Flip Doge
                sr.flipX = false;
            }
            else if (deltaX > 0)
            {
                // Flip Doge back to normal
                sr.flipX = true;
            }
        }

        // If Turtle's health drops below 3/4 max life.
        if (health < maxHealth / 4 * 3)
        {
            // Init phase 2.
            if(!phase2init)
            {
                // Things that have to be done once Turtle hits phase 2.
                phase = 2;
                phase2init = true;
            }
            // Movement and attacks for phase 2.
        }


        // If Turtle's health drops below 1/2 max life.
        if (health < maxHealth / 2)
        {
            // Init phase 3.
            if (!phase3init)
            {
                // Things that have to be done once Turtle hits phase 3.
                phase = 3;
                phase3init = true;
            }
            // Movement and attacks for phase 3.
        }


        // If Turtle's health drops below 1/4 max life.
        if (health < maxHealth / 4)
        {
            // Init phase 4.
            if (!phase4init)
            {
                // Things that have to be done once Turtle hits phase 4.
                phase = 4;
                phase4init = true;
            }
            // Movement and attacks for phase 4.
        }
    }

    IEnumerator shootPhase1(Vector2 targetVelocity)
    {
        Debug.Log("Turtle should shoot");
        canShootNext = false;

        // Instantiate projectile, move it into the enemy and add a force.
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = transform.position;
        projectile.GetComponent<Rigidbody2D>().AddForce(targetVelocity * shootPower);
        yield return new WaitForSeconds(2f);
        canShootNext = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Subtract one healthpoint if Dolan gets hit by the PlayerProjectile.
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
        // Reduce f by 0.1 until f = 0
        for (float f = 1; f >= 0; f -= 0.1f)
        {
            // Colour of Dolan's sprite
            Color colour = sr.material.color;

            // Alpha of colour is reduced by 0.1 for every f >= 0
            colour.a = f;

            // Apply colour with less alpha to Dolan's SpriteRenderer
            sr.material.color = colour;

            // Wait until next frame
            yield return null;
        }


        // Destroy that duckling
        alive = false;
        Instantiate(money, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(this.gameObject);
    }
}
