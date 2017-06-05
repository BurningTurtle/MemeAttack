using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Show this variable in inspector
    public float speed = 3f;
    public int damage;
    public int health;

    private bool readyForDamage = true;

    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private Sprite link;
    [SerializeField] private GameObject MasterSword;

    public bool isLink, isDarkLink;
    private bool hasSword;
    private bool attack;
    public float bass;

    public bool hasKey = false;
    public bool hasCoinMagnet = false;

    // Kleines Yen
    public static int kleinesYen;
    private SoundManager soundMan;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        soundMan = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        // Uncomment this for testing
        health = 100;

        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        bass = spectrum[2] + spectrum[0] + spectrum[1];

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


        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            // Switches from idle to walk animation.
            anim.SetBool("isWalking", true);
            transform.Translate(movement);
        }
        else
        {
            anim.SetBool("isWalking", false);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (!hasSword && isDarkLink)
        {
            MasterSword = Instantiate(MasterSword) as GameObject;
            MasterSword.transform.Rotate(0, 0, -90);
            MasterSword.transform.localScale += new Vector3(0.2f, 0.2f, 0);

            hasSword = true;
        }

        if (isDarkLink)
        {
            MasterSword.transform.position = new Vector2(transform.position.x + .3f, transform.position.y - .2f);
        }

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
                    break;
                case "Dolan":
                    health -= 5;
                    break;
                case "DatBoi":
                    health -= 5;
                    break;
                case "Doge":
                    health -= 5;
                    break;
                case "Turtle":
                    health -= 20;
                    break;
                case "DatDolan":
                    health -= 20;
                    break;
                case "NyanDogeDoge":
                    health -= 10;
                    break;
                case "NyanDogeCat":
                    health -= 10;
                    break;
            }
            StartCoroutine(getReadyForDamage());
        }
        if (collision.gameObject.tag == "NyanCat")
        {
            health -= 20;
            Destroy(collision.gameObject);
        }
    }



    // Damage the player every second if the enemy sticks with him. (Avoiding way too much damage)
    IEnumerator getReadyForDamage()
    {
        readyForDamage = false;
        yield return new WaitForSeconds(1.0f);
        readyForDamage = true;
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
        transform.localScale += new Vector3(0.2f, 0.2f, 0);
        Debug.Log("transformed");
    }

    IEnumerator waitForDarkLink()
    {
        yield return new WaitForSeconds(34.2f);
        anim.SetBool("isLink", false);
        anim.SetBool("isDarkLink", true);
        yield return new WaitForSeconds(0.3f);
        isDarkLink = true;
        isLink = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "1Yen":
                kleinesYen += 1;
                soundMan.playKleinesYen();
                Destroy(collision.gameObject);
                break;
            case "5Yen":
                kleinesYen += 5;
                soundMan.playKleinesYen();
                Destroy(collision.gameObject);
                break;
            case "10Yen":
                kleinesYen += 10;
                soundMan.playKleinesYen();
                Destroy(collision.gameObject);
                break;
            case "50Yen":
                kleinesYen += 50;
                soundMan.playKleinesYen();
                Destroy(collision.gameObject);
                break;
            case "100Yen":
                kleinesYen += 100;
                soundMan.playKleinesYen();
                Destroy(collision.gameObject);
                break;
            case "500Yen":
                kleinesYen += 500;
                soundMan.playKleinesYen();
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
    }
}
