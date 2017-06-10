using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemySpecial : MonoBehaviour {

    [SerializeField]
    private GameObject projectilePrefab;
    private GameObject projectile;
    [SerializeField]
    private GameObject onePrefab, zeroPrefab;

    private bool alive;
    public float speed;
    public float shootPower;
    public float range;
    GameObject player;
    private bool canShootNext;

    // For It's time to stop
    public bool stop;

    private Animator anim;

    [SerializeField]

    // Use this for initialization
    void Start()
    {
        alive = true;
        player = GameObject.Find("Player");
        canShootNext = true;
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive && !stop)
        {
            // Get Vector to the player.
            Vector2 targetVelocity = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            targetVelocity.Normalize();
            GetComponent<Rigidbody2D>().velocity = targetVelocity * speed * Time.deltaTime;

            // Get "distance" between enemy and player.
            Vector2 trueDistance = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            float combinedDistance = Mathf.Abs(trueDistance.x) + Mathf.Abs(trueDistance.y);
        }
        if (stop)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            anim.SetBool("moving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy enemy and projectile if it gets hit.
        if (collision.tag == "PlayerProjectile" || collision.tag == "Bubble")
        {
            int ran = Random.Range(0, 100);
            if (ran <= 50)
            {
                GameObject zero = Instantiate(zeroPrefab) as GameObject;
                zero.transform.position = transform.position;
            }
            else
            {
                GameObject one = Instantiate(onePrefab) as GameObject;
                one.transform.position = transform.position;
            }

            if (collision.gameObject.tag == "PlayerProjectile")
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
