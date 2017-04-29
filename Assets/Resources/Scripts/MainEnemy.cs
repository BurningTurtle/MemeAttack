using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : MonoBehaviour {

    [SerializeField]
    private GameObject projectilePrefab;
    private GameObject projectile;

    private bool alive;
    public float speed = 2f;
    public float projectileSpeed;
    GameObject player;

    // Use this for initialization
    void Start ()
    {
        alive = true;
        player = GameObject.Find("Player");
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
            if(combinedDistance < 5)
            {
                if(projectile == null)
                {
                    StartCoroutine(shoot(targetVelocity));
                }
            }
        }
	}

    IEnumerator shoot(Vector2 targetVelocity)
    {
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = transform.position;
        projectile.GetComponent<Rigidbody2D>().velocity = targetVelocity  * projectileSpeed;

        // If the projectile's magnitude is too low (therefore close to the player), increase it until its value is 5. (So it gets harder if the player is too close.
        while (projectile.GetComponent<Rigidbody2D>().velocity.magnitude < 10)
        {
            targetVelocity = targetVelocity * 1.1f;
            projectile.GetComponent<Rigidbody2D>().velocity = targetVelocity * 1.1f;
        }
        //Debug.Log(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);
        yield return new WaitForSeconds(2f);
        Destroy(projectile.gameObject);
    }
}
