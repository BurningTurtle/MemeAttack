using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherShuuriken : MonoBehaviour {

    private int damage = 20;
    private GameObject player;
    private int speed = 350;
    private int spinSpeed = 300;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        StartCoroutine(die());
	}

    private void FixedUpdate()
    {
        Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
        playerVector.Normalize();
        GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;

        transform.Rotate(0,0, spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            if (player.GetComponent<Player>().readyForDamage)
            {
                collision.GetComponent<Player>().health -= damage;
                collision.GetComponent<Player>().GetReadyForDamage();
            }
        }
        Destroy(gameObject);
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
