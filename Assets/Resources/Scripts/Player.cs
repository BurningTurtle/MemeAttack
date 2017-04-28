using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Show this variable in inspector
    [SerializeField] private float speed = 3f;

    //private bool isLink = false;

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

        }
    }
}
