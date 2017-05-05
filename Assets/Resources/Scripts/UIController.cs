using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour {

    public Text hp;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");

    }
	
	// Update is called once per frame
	void Update () {
        hp.text = player.GetComponent<Player>().health.ToString();
    }
}
