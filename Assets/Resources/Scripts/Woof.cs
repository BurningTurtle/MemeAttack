using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woof : MonoBehaviour
{
    private GameObject player;
    private GameObject doge;
    private int damage = 5;
    public bool stop = false;

    // Use this for initialization
    void Start()
    {
        doge = GameObject.FindWithTag("Doge");
        player = GameObject.Find("Player");
        StartCoroutine(die());
        StartCoroutine(grow());

        // Rotation towards player
        Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
        playerVector.Normalize();
        if (playerVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void FixedUpdate()
    {
        if(stop == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    IEnumerator grow()
    {
        for(float f = 0.3f ; f < 2; f = f + 0.2f)
        {
            if (!stop)
            {
                transform.localScale = new Vector3(f, f, 0);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().health -= damage;
            Destroy(this.gameObject);
        }
    }
}
