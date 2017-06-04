using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("tp");
        if (other.transform.name == "Player")
        {
            switch (this.transform.name)
            {
                case "toShop":
                    other.transform.position = new Vector2(48, -24);
                    break;
                case "toGallery":
                    other.transform.position = new Vector2(-23, -29);
                    break;
            }
        }
    }
}
