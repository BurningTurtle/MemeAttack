using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : MonoBehaviour {

    [SerializeField]
    private GameObject projectilePrefab;
    private GameObject projectile;
    [SerializeField]
    private GameObject yen1, yen500;

    private bool alive;
    public float speed;
    public float shootPower;
    public float range;
    GameObject player;
    private bool canShootNext;

    // For It's time to stop
    public bool stop;

    private Animator anim;

    private GameObject statue;
    [SerializeField]
    private Sprite statueActivated;
    [SerializeField]
    private GameObject special1Controller;

    // Use this for initialization
    void Start()
    {
        special1Controller = GameObject.Find("Special1Controller");
        alive = true;
        player = GameObject.Find("Player");
        canShootNext = true;
        anim = GetComponent<Animator>();
        statue = GameObject.Find("mainEnemyStatue1");
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
                //Debug.Log("shoot!");
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
        if (collision.tag == "PlayerProjectile" || collision.tag == "Bubble")
        {
            int ran = Random.Range(0, 100);
            if(ran <= 50)
            {
                int ran1 = Random.Range(0, 1000);
                if(ran1 == 1)
                {
                    GameObject f1 = Instantiate(yen500) as GameObject;
                    GameObject f2 = Instantiate(yen500) as GameObject;
                    GameObject f3 = Instantiate(yen500) as GameObject;
                    GameObject f4 = Instantiate(yen500) as GameObject;
                    GameObject f5 = Instantiate(yen500) as GameObject;
                    f1.transform.position = new Vector2(transform.position.x, transform.position.y);
                    f2.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
                    f3.transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                    f4.transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                    f5.transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                }
                else
                {
                    GameObject oneYen = Instantiate(yen1) as GameObject;
                    oneYen.transform.position = transform.position;
                }
            }

            if(collision.gameObject.tag == "PlayerProjectile")
            {
                Destroy(collision.gameObject);
            }

            statue.GetComponent<SpriteRenderer>().sprite = statueActivated;
            Destroy(gameObject);
        }

        if (collision.tag == "MasterSword")
        {
            if(player.GetComponent<Player>().bass > 0.9f)
            {
                special1Controller.GetComponent<Special1Controller>().crits++;
                Debug.Log("crit");
                GameObject.Find("UIController").GetComponent<UIController>().showCrit();
            }
            else
            {
                special1Controller.GetComponent<Special1Controller>().nonCrits++;
            }
            Destroy(gameObject);
        }
    }

}
