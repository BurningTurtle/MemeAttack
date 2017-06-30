using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OShitWaddup : MonoBehaviour {

    public int damage;
    public GameObject datBoi;
    public bool stopped;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(die());
        datBoi = GameObject.Find("DatBoi(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {
            StartCoroutine(stop());
        }
    }

    IEnumerator stop()
    {
        if (datBoi != null && datBoi.GetComponent<DatBoi>().stop == true)
        {
            stopped = true;
            Vector2 forceBeforeStop = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(3f);
            GetComponent<Rigidbody2D>().velocity = forceBeforeStop;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (other.GetComponent<Player>().readyForDamage)
            {
                // Substract 2 health from Player if hit by oshitwaddup
                other.GetComponent<Player>().health -= damage;
                other.GetComponent<Player>().GetReadyForDamage();
            }
        }
        Destroy(gameObject);
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
    }
}
