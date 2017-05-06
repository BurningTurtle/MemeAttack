using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Show this variable in inspector
    [SerializeField] private float speed = 3f;
    public int damage;
    public int health;

    private bool readyForDamage = true;


    private Animator anim;
    [SerializeField] private GameObject projectilePrefab;

    // For Sounds
    private AudioSource audioSource;

    // For Seitenbacher
    [SerializeField] private AudioClip seitenbacherSound;


    private void Start()
    {
        anim = GetComponent<Animator>();
        damage = 1;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
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
            StartCoroutine(shoot());
        }

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            }
            StartCoroutine(getReadyForDamage());
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


    // Is triggered by Seitenbacher class.
    public void seitenbacher()
    {
        // Play "SEITENBACHER BERGSTEIGER MÜSLI! BERGSTEIGER MÜSLI VON SEITENBACHER!". 
        audioSource.PlayOneShot(seitenbacherSound, 1);

        // Give Player a temporary DMG-UP for 10 seconds.
        StartCoroutine(temporaryDmgUp());
    }

    IEnumerator temporaryDmgUp()
    {
        damage += 1;

        yield return new WaitForSeconds(10f);

        damage -= 1;
    }
}
