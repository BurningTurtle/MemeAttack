using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Show this variable in inspector
    [SerializeField] private float speed = 3f;

    public int health;
    private bool readyForDamage = true;

    private bool alive = true;

    private Animator anim;

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

        }

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MainEnemyProjectile")
        {
            health -= 1;
        }

        if (readyForDamage)
        {
            switch (collision.gameObject.tag)
            {
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
}
