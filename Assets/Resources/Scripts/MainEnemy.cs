using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : MonoBehaviour {

    [SerializeField]
    private GameObject projectilePrefab;
    private GameObject projectile;

    private bool alive;
    public float speed = 2f;
    GameObject player;
    public bool canShootNext;

    // Use this for initialization
    void Start ()
    {
        alive = true;
        player = GameObject.Find("Player");
        canShootNext = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(alive)
        {
            // Get Vector to the player.
            Vector2 targetVelocity = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            GetComponent<Rigidbody2D>().velocity = targetVelocity * speed * Time.deltaTime;

            while(GetComponent<Rigidbody2D>().velocity.magnitude < speed)
            {
                GetComponent<Rigidbody2D>().velocity *= 1.1f;
            }
            //Debug.Log(targetVelocity.x + targetVelocity.y);

            // Get "distance" between enemy and player.
            float combinedDistance = Mathf.Abs(targetVelocity.x + targetVelocity.y);
            if(combinedDistance < 5 && projectile == null && canShootNext)
            {               
                    StartCoroutine(shoot(targetVelocity));      
            }
        }
	}

    IEnumerator shoot(Vector2 targetVelocity)
    {
        canShootNext = false;

        // Instantiate projectile, move it into the enemy and add a force.
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = transform.position;
        projectile.GetComponent<Rigidbody2D>().AddForce(targetVelocity / 75);

        // Destory the projectile if it didn't hit anything after 2 seconds.
        yield return new WaitForSeconds(2f);
        Destroy(projectile.gameObject);
        canShootNext = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destory enemy and projectile if it gets hit.
        if (collision.tag == "PlayerProjectile")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
