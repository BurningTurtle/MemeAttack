using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dolan : MonoBehaviour {

    private GameObject player;
    private bool alive = true;
    private float minimalSpeed = 3f;
    private int health = 10;
    GameObject knife1, knife2, knife3, knife4;

    private Animator anim;

    [SerializeField] private GameObject knifePrefab;
    private bool canShootNext;

    private void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        canShootNext = true;
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            GetComponent<Rigidbody2D>().velocity = playerVector * minimalSpeed * Time.deltaTime;

            // Accelerate Dolan if he's too slow.
            while (GetComponent<Rigidbody2D>().velocity.magnitude < minimalSpeed)
            {
                GetComponent<Rigidbody2D>().velocity *= 1.1f;
            }

            // Distance between Dolan and Player
            float distance = Mathf.Abs(playerVector.x + playerVector.y);

            // If Dolan's distance is smaller than 7 etc., shoot
            if (distance < 7 && canShootNext)
            {
                StartCoroutine(shoot(playerVector));
            }
        }
    }

    IEnumerator shoot(Vector2 playerVector)
    {
        canShootNext = false;

        // Instantiate new knife at Dolan's position
        knife1 = Instantiate(knifePrefab) as GameObject;
        knife1.transform.position = this.transform.position;

        knife2 = Instantiate(knifePrefab) as GameObject;
        knife2.transform.position = this.transform.position;

        knife3 = Instantiate(knifePrefab) as GameObject;
        knife3.transform.position = this.transform.position;

        knife4 = Instantiate(knifePrefab) as GameObject;
        knife4.transform.position = this.transform.position;

        // Launch and rotate the knife towards the player
        knife1.GetComponent<Rigidbody2D>().AddForce(playerVector / 75);
        float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
        knife1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector2 playerVectorRotated = playerVector;

        playerVectorRotated = RotateVector(playerVector, 90f);
        knife2.GetComponent<Rigidbody2D>().AddForce(playerVectorRotated / 75);
        knife2.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        playerVectorRotated = RotateVector(playerVectorRotated, 90f);
        knife3.GetComponent<Rigidbody2D>().AddForce(playerVectorRotated / 75);
        knife3.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);

        playerVectorRotated = RotateVector(playerVectorRotated, 90f);
        knife4.GetComponent<Rigidbody2D>().AddForce(playerVectorRotated / 75);
        knife4.transform.rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);

        // Wait and Destroy the knife
        yield return new WaitForSeconds(1f);
        Destroy(knife1.gameObject);

        // Update our vector
        Vector2 playerVector2 = new Vector2(player.transform.position.x - knife2.transform.position.x, player.transform.position.y - knife2.transform.position.y);
        Vector2 playerVector3 = new Vector2(player.transform.position.x - knife3.transform.position.x, player.transform.position.y - knife3.transform.position.y);
        Vector2 playerVector4 = new Vector2(player.transform.position.x - knife4.transform.position.x, player.transform.position.y - knife4.transform.position.y);

        // ATTACK
        knife2.GetComponent<Rigidbody2D>().AddForce(playerVector2 / 100);
        knife3.GetComponent<Rigidbody2D>().AddForce(playerVector3 / 100);
        knife4.GetComponent<Rigidbody2D>().AddForce(playerVector4 / 100);

        knife2.GetComponent<Knife>().back = true;
        knife3.GetComponent<Knife>().back = true;
        knife4.GetComponent<Knife>().back = true;

        yield return new WaitForSeconds(2f);

        Destroy(knife2.gameObject);
        Destroy(knife3.gameObject);
        Destroy(knife4.gameObject);

        canShootNext = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Subtract one healthpoint if Dolan gets hit by the PlayerProjectile.
        if (collision.tag == "PlayerProjectile")
        {
            health -= 1;
            if(health <= 0)
            {
                // Coroutine because Wait Time is necessary
                StartCoroutine(die());
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator die()
    {
        // Activate Death Animation (Animator)
        anim.SetBool("isDead", true);

        // Wait for animation to run ONCE (!) If waiting for longer than 0.3s, animation will restart
        yield return new WaitForSeconds(0.3f);

        // Destroy that duckling
        alive = false;
        Destroy(this.gameObject);
    }

    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
}
