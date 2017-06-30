using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yen : MonoBehaviour {

    private GameObject player;
    [SerializeField]
    private float speedToPlayer;
    GameObject arenaController;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        arenaController = GameObject.Find("ArenaController");
        arenaController.GetComponent<ArenaController>().moneyInScene.Add(this.gameObject);
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(die());
    }
	
	// Update is called once per frame
	void Update () {
		if(player.GetComponent<Player>().hasCoinMagnet == true)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 7.5f)
            {
                Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
                playerVector.Normalize();
                GetComponent<Rigidbody2D>().velocity = playerVector * speedToPlayer * Time.deltaTime;
            }
        }
	}

    IEnumerator die()
    {
        yield return new WaitForSeconds(5);

        for(float f = 1; f >= 0; f -= 0.1f)
        {
            Color colour = sr.color;
            colour.a = f;
            sr.color = colour;
            yield return null;
        }
        Destroy(gameObject);
    }
}
