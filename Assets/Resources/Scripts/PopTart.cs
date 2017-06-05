using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTart : MonoBehaviour
{
    private int damage = 5;

    private void Start()
    {
        StartCoroutine(die());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Substract 2 health from Player if hit by PopTart
            other.GetComponent<Player>().health -= damage;
            Destroy(gameObject);
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

}
