using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleProjectile2 : MonoBehaviour {
    
    private GameObject player;
    private SpriteRenderer sr;
    [SerializeField]
    private Sprite fire;

    [SerializeField]
    private int damage;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(eruption());
        player = GameObject.FindWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {   
    }

    IEnumerator eruption()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0.04f));
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        playerVector.Normalize();
        if (playerVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        GetComponent<Rigidbody2D>().AddForce(playerVector / 15);
        yield return new WaitForSeconds(1.25f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        transform.rotation = Quaternion.identity;
        sr.sprite = fire;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<Player>().readyForDamage)
            {
                collision.GetComponent<Player>().health -= damage;
                collision.GetComponent<Player>().GetReadyForDamage();
            }
        }
        Destroy(this.gameObject);
    }
}
