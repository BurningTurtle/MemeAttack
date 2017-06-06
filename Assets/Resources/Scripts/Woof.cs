using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woof : MonoBehaviour
{

    private GameObject player;
    private GameObject doge;
    [SerializeField]
    private float speed = 270f;
    private int damage = 5;
    public bool stop = false;

    // Use this for initialization
    void Start()
    {
        doge = GameObject.FindWithTag("Doge");
        player = GameObject.Find("Player");
        StartCoroutine(die());
    }

    void FixedUpdate()
    {
        if(stop == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else
        {
            Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
            playerVector.Normalize();
            GetComponent<Rigidbody2D>().velocity = playerVector * speed * Time.deltaTime;

            if (playerVector != Vector2.zero)
            {
                float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
