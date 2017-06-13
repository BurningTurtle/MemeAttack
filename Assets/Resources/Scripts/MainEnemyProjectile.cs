using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemyProjectile : MonoBehaviour {

    public int damage;
    public GameObject mainEnemy;
    public bool stopped;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(die());
        mainEnemy = GameObject.Find("MainEnemy(Clone)");
    }

    private void Update()
    {
        if(!stopped)
        {
            StartCoroutine(stop());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Subtract one healthpoint from the player if he gets hit by the projectile.
        if (other.tag == "Player")
        {
            if (GameObject.Find("Player").GetComponent<Player>().readyForDamage)
            {
                other.GetComponent<Player>().health -= damage;
                other.GetComponent<Player>().GetReadyForDamage();
            }
            Destroy(gameObject);
        }
    }

    IEnumerator stop()
    {
        if (mainEnemy != null && mainEnemy.GetComponent<MainEnemy>().stop == true)
        {
            stopped = true;
            Vector2 forceBeforeStop = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(3f);
            GetComponent<Rigidbody2D>().velocity = forceBeforeStop;
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
    }
}
