using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : MonoBehaviour {

    [SerializeField]
    private GameObject projectilePrefab;
    private GameObject projectile;

    private bool alive;
    public float speed;
    public float shootPower;
    public float range;
    GameObject player;
    private bool canShootNext;

    // For It's time to stop
    public bool stop;

    private Animator anim;

    // Use this for initialization
    void Start ()
    {
        alive = true;
        player = GameObject.Find("Player");
        canShootNext = true;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(alive && !stop)
        {
            // Get Vector to the player.
            Vector2 targetVelocity = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            targetVelocity.Normalize();
            GetComponent<Rigidbody2D>().velocity = targetVelocity * speed * Time.deltaTime;

            // Get "distance" between enemy and player.
            Vector2 trueDistance = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            float combinedDistance = Mathf.Abs(trueDistance.x) + Mathf.Abs(trueDistance.y);
            //Debug.Log("combined distance" + combinedDistance);
            if(combinedDistance < range && canShootNext && !stop)
            {
                Debug.Log("shoot!");
                StartCoroutine(shoot(targetVelocity));      
            }

        }
        if (stop)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            anim.SetBool("moving", false);
        }
	}

    IEnumerator shoot(Vector2 targetVelocity)
    {
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
        // Destroy enemy and projectile if it gets hit.
        if (collision.tag == "PlayerProjectile")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "MasterSword")
        {
            Destroy(gameObject);
        }
    }

}
