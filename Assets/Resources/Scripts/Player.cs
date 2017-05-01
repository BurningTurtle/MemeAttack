using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Show this variable in inspector
    [SerializeField] private float speed = 3f;
    public int health;

    private bool alive = true;
    private bool readyForDamage = true;


    private Animator anim;
    [SerializeField] private GameObject projectilePrefab;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (alive)
        {
            float xValue = Input.GetAxis("Horizontal") * speed;
            float yValue = Input.GetAxis("Vertical") * speed;
            Vector2 movement = new Vector2(xValue, yValue);

            // Limit diagonal movement to the same speed as movement along an axis
            movement = Vector2.ClampMagnitude(movement, speed);

            movement *= Time.deltaTime;

            
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                // Switches from idle to walk animation
                anim.SetBool("isWalking", true);
                transform.Translate(movement);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

            Debug.Log(health);

            if (Input.GetMouseButtonDown(0)) //&& !projectileFlying)
            {
                StartCoroutine(shoot());
            }

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
                    health -= 1;
                    break;
                case "Dolan":
                    health -= 3;
                    break;
                case "DatBoi":
                    health -= 3;
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
}
