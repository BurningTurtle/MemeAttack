using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowShoot : MonoBehaviour {

    [SerializeField] private GameObject splashPrefab;
    private GameObject player;
    private int shotSpeed = 2;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void splash()
    {
        GameObject splash = Instantiate(splashPrefab) as GameObject;

        // Splash comes out of Rainbow, where the Spawner is located
        splash.transform.position = this.transform.position;

        // Launch towards Player
        Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
        splash.GetComponent<Rigidbody2D>().velocity = playerVector * shotSpeed;
    }
}
