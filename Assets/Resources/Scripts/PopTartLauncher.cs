using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTartLauncher : MonoBehaviour {

    [SerializeField] private GameObject popTartPrefab;
    private GameObject player;
    private int shotSpeed = 3;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void launchPopTart()
    {
        GameObject poptart = Instantiate(popTartPrefab) as GameObject;

        // Poptart comes out of Nyan Cat's mouth, where the Spawner is located
        poptart.transform.position = this.transform.position;

        // Launch towards Player
        Vector2 playerVector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
        poptart.GetComponent<Rigidbody2D>().velocity = playerVector * shotSpeed;
    }
}
