using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour {

    private int damage = 10;
    private GameObject player;

	// Use this for initialization
	void Start () {
        StartCoroutine(destroyFeather());
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
        if (playerVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    IEnumerator destroyFeather()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
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
        Destroy(this.gameObject);
    }
}
