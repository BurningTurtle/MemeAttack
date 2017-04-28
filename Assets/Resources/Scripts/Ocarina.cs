using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocarina : MonoBehaviour {

    bool given;
    public bool small;
    public Sprite darkLink;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if(given)
        {
            GameObject player = GameObject.Find("Link");

            // Ocarina is set to the character's mouth.
            transform.position = new Vector2(player.transform.position.x + .1f, player.transform.position.y + -.2f);

            // Is it already small?
            if(!small)
            {
                // If not, make it smaller so it fits onto the character.
                transform.localScale += new Vector3(-0.15f, -0.15f, 0);

                // Change sprite to darkLink.
                StartCoroutine(getDark());
                small = true;
            }
            
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        given = true;
    }

    IEnumerator getDark()
    {
        yield return new WaitForSeconds(8.475f);

        // De- and reactivating the Animator so the sprite can change.
        GameObject.Find("Link").GetComponent<Animator>().enabled = false;
        GameObject.Find("Link").GetComponent<SpriteRenderer>().sprite = darkLink;
        GameObject.Find("Link").GetComponent<Animator>().enabled = true;
    }
}
