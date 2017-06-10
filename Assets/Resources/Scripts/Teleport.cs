using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    private SoundManager soundMan;

	// Use this for initialization
	void Start () {
        soundMan = GameObject.FindObjectOfType<SoundManager>();
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
                    soundMan.playAudioClip("Teleport");
                    break;
                case "toGallery":
                    other.transform.position = new Vector2(-42, -26);
                    soundMan.playAudioClip("Teleport");
                    break;
                case "toArena2":
                    other.transform.position = new Vector2(12, 50);
                    soundMan.playAudioClip("Teleport");
                    break;

            }
            if(this.tag == "toSpecial2")
            {
                other.transform.position = new Vector2(-40, -50);
                soundMan.playAudioClip("Teleport");
            }
        }
    }
}
