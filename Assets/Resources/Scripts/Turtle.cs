using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour {

    private GameObject player;
    private bool alive = true;
    [SerializeField] int maxHealth;
    public int health;
    public int speed;
    public float chargeSpeed;
    public float shootPower;
    private int phase;
    private bool phase2init, phase3init, phase4init;
    private bool canShootNext = true;
    private bool canCharge;
    private bool rotating;

    [SerializeField]
    private GameObject projectilePrefab;
    private GameObject projectile;
    [SerializeField]
    private GameObject projectilePrefab2;

    // Keep track of which direction Doge is moving to. Important for animation.
    private float deltaX;
    private float lastPosition;

    [SerializeField]
    private Sprite shellSprite, shellSprite2;

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
        if (phase == 1 && alive && !stop)
        {
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            playerVector.Normalize();
            GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;
            
            if (canShootNext)
            {
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
        if (health < maxHealth / 4 * 3 && !phase3init)
        {
            // Init phase 2.
            if (!phase2init)
            {
                anim.enabled = false;
                phase = 2;
                phase2init = true;
                canCharge = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            // Movement and attacks for phase 2.
            sr.sprite = shellSprite;
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            playerVector.Normalize();
            if (canCharge)
            {
                StartCoroutine(charge());
            }
            if(rotating)
            {
                transform.Rotate(0, 0, 720 * Time.deltaTime);
            }
        }


        // If Turtle's health drops below 1/2 max life.
        if (health < maxHealth / 2 && !phase4init)
        {
            // Init phase 3.
            if (!phase3init)
            {
                // Things that have to be done once Turtle hits phase 3.
                phase = 3;
                phase3init = true;
                anim.enabled = true;
                anim.SetInteger("phase", 3);
            }
            // Movement and attacks for phase 3.
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            playerVector.Normalize();
            GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;

            if (canShootNext)
            {
                Debug.Log("shoot!");
                StartCoroutine(shootPhase3());
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


        // If Turtle's health drops below 1/4 max life.
        if (health < maxHealth / 4)
        {
            // Init phase 4.
            if (!phase4init)
            {
                // Things that have to be done once Turtle hits phase 4.
                phase = 4;
                phase4init = true;
                anim.enabled = false;
                canCharge = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                canShootNext = true;
            }
            // Movement and attacks for phase 4.
            sr.sprite = shellSprite2;
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            playerVector.Normalize();
            if (canCharge)
            {
                StartCoroutine(charge());
            }
            if (canShootNext)
            {
                StartCoroutine(shootPhase3());
            }
            if (rotating)
            {
                transform.Rotate(0, 0, 720 * Time.deltaTime);
            }
        }
    }

    IEnumerator shootPhase1(Vector2 targetVelocity)
    {
        canShootNext = false;

        // Instantiate projectile, move it into the enemy and add a force.
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = transform.position;
        projectile.GetComponent<Rigidbody2D>().AddForce(targetVelocity * shootPower);
        yield return new WaitForSeconds(2f);
        canShootNext = true;
    }

    IEnumerator charge()
    {
        canCharge = false;
        yield return new WaitForSeconds(2f);
        Vector2 chargeTo = getPlayerVector().normalized;
        yield return new WaitForSeconds(0.025f);
        rotating = true;
        GetComponent<Rigidbody2D>().AddForce(chargeTo / chargeSpeed);
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        rotating = false;
        transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(2f);
        canCharge = true;
    }

    IEnumerator shootPhase3()
    {
        canShootNext = false;
        Instantiate(projectilePrefab2, new Vector2(transform.position.x, transform.position.y+1), Quaternion.identity);
        Instantiate(projectilePrefab2, new Vector2(transform.position.x-1, transform.position.y), Quaternion.identity);
        Instantiate(projectilePrefab2, new Vector2(transform.position.x+1, transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        canShootNext = true;
    }

        private Vector2 getPlayerVector()
    {
        return new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
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
