using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour {

    public Image heartsIngame;
    public Sprite[] hearts;
    private GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
        int heartPoints = player.GetComponent<Player>().health / 5;
        Debug.Log(heartPoints);
        heartsIngame.sprite = hearts[heartPoints];
    }
}
