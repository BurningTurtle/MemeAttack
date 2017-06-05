using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoofSpawner : MonoBehaviour {

    private GameObject player;
    [SerializeField] private GameObject woofPrefab;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
    public void spawnWoof()
    {
        GameObject woof = Instantiate(woofPrefab) as GameObject;
        woof.transform.position = this.transform.position;
    }
}
