using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

    public bool back;
    public int damage;
    public GameObject dolan;
    private bool stopped = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(die());
        dolan = GameObject.Find("Dolan(Clone)");
    }

    // Update is called once per frame
    private void Update()
    {
        // Rotate if the knife is on its way back to the player.
        if (back)
        {
            transform.Rotate(0, 0, 720 * Time.deltaTime);
        }
        if(!stopped)
        {
            StartCoroutine(stop());
        }
    }

    IEnumerator stop()
    {
        if (dolan != null && dolan.GetComponent<Dolan>().stop == true)
        {
            stopped = true;
            back = false;
            Vector2 forceBeforeStop = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(3f);
            if (GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                GetComponent<Rigidbody2D>().velocity = forceBeforeStop;
            }
            Debug.Log("back force from Knife script");
            back = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // Give Player 2 Damage on hit, then destroy knife.
            collision.GetComponent<Player>().health -= damage;
            Destroy(this.gameObject);
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
