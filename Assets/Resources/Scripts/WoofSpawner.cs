using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoofSpawner : MonoBehaviour {

    private GameObject player;
    [SerializeField] private GameObject woofPrefab;
    private int shotSpeed = 2;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
    public void spawnWoof()
    {
        GameObject woof = Instantiate(woofPrefab) as GameObject;
        woof.transform.position = this.transform.position;

        // Get Vector to the player
        Vector2 playerVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        playerVector = Vector2.ClampMagnitude(playerVector, shotSpeed);

        woof.GetComponent<Rigidbody2D>().velocity = playerVector * shotSpeed;
    }
}
