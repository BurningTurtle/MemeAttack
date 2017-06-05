using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

    private int damage = 5;
    [SerializeField] private Sprite[] splashs;
    private SpriteRenderer sr;

    private void Start()
    {
        StartCoroutine(die());
        sr = GetComponent<SpriteRenderer>();

        int x = Random.Range(0, splashs.Length);
        sr.sprite = splashs[x];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Substract 2 health from Player if hit by Splash
            other.GetComponent<Player>().health -= damage;
            Destroy(gameObject);
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
