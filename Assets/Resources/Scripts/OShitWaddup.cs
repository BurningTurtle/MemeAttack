using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OShitWaddup : MonoBehaviour {

    public int damage;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(die());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // Substract 2 health from Player if hit by oshitwaddup
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
