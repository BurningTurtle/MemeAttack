using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Show this variable in inspector
    [SerializeField] private float speed = 3f;

    //private bool isLink = false;
    private bool alive = true;

    private Animator animation;

    private void Start()
    {
        animation = GetComponent<Animator>();
    }

    private void Update()
    {
        if (alive)
        {
            float xValue = Input.GetAxis("Horizontal") * speed;
            float yValue = Input.GetAxis("Vertical") * speed;
            Vector2 movement = new Vector2(xValue, yValue);
            movement = Vector2.ClampMagnitude(movement, speed); //limit diagonal movement to the same speed as movement along an axis

            movement *= Time.deltaTime;

            animation.SetBool("isWalking", false); //so that player doesn't continue running once he was moving
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)  
            {
                animation.SetBool("isWalking", true); //switches from idle to walk animation
                transform.Translate(movement);
            }
            
        }
    }
}
