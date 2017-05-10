using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemyProjectile : MonoBehaviour {

    public int damage;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(die());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Subtract one healthpoint from the player if he gets hit by the projectile.
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().health -= damage;
            Destroy(gameObject);
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
