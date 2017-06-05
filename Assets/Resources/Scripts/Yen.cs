using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yen : MonoBehaviour {

    private GameObject player;
    [SerializeField]
    private float speedToPlayer;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
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
}
