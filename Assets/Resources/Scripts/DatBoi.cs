using UnityEngine;

public class DatBoi : MonoBehaviour {

    private GameObject player;
    private bool alive = true;
    public float speed = 1f;
    private int health = 10;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (alive)
        {
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;

            // Accelerate DatBoi if he is too slow
            while(GetComponent<Rigidbody2D>().velocity.magnitude < speed)
            {
                GetComponent<Rigidbody2D>().velocity *= 1.1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Substract 1 health if hit by PlayerProjectile GameObject. Destroy DatBoi if health equals 0 (or lower)
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health -= 1;

            if (health <= 0)
            {
                alive = false;
                Destroy(this.gameObject);
            }
        }

        Destroy(collision.gameObject);
    }
}
