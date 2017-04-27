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
            transform.position = new Vector2(player.transform.position.x + .1f, player.transform.position.y + -.2f);
            if(!small)
            {
                transform.localScale += new Vector3(-0.15f, -0.15f, 0);
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
        Debug.Log("dark triggered");
        yield return new WaitForSeconds(8.475f);
        GameObject.Find("Link").GetComponent<Animator>().enabled = false;
        GameObject.Find("Link").GetComponent<SpriteRenderer>().sprite = darkLink;
        Destroy(gameObject);
        Debug.Log("dark triggered 2");
    }
}
