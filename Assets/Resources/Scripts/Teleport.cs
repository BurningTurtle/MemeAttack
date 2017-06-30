using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour {

    private SoundManager soundMan;
    public bool canEscape = false;

	// Use this for initialization
	void Start () {
        soundMan = GameObject.FindObjectOfType<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
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
                    other.transform.position = new Vector2(2, 50);
                    soundMan.playAudioClip("Teleport");
                    break;
                case "Big Portal":
                    StartCoroutine(teleportFinal());
                    break;

            }
            if(this.tag == "toSpecial2")
            {
                other.transform.position = new Vector2(-40, -50);
                soundMan.playAudioClip("Teleport");
            }
        }
    }

    IEnumerator teleportFinal()
    {
        if (!canEscape && Application.loadedLevelName == "Arena")
        {
            soundMan.playAudioClip("Teleport");
            yield return new WaitForSeconds(0.2f);
            soundMan.playAudioClip("Teleport");
            yield return null;
            soundMan.playAudioClip("Teleport");
            yield return null;
            soundMan.playAudioClip("Teleport");
            soundMan.playAudioClip("Teleport");
            soundMan.playAudioClip("Teleport");
            yield return null;
            soundMan.playAudioClip("Teleport");
            yield return null;
            soundMan.playAudioClip("Teleport");
            yield return null;
            soundMan.playAudioClip("Teleport");
            soundMan.playAudioClip("Teleport");
            soundMan.playAudioClip("Teleport");
            yield return null;
            soundMan.playAudioClip("Teleport");
            yield return null;
            soundMan.playAudioClip("Teleport");
            yield return null;
            soundMan.playAudioClip("Teleport");
            yield return null;
            soundMan.playAudioClip("Teleport");
            yield return null;
            soundMan.playAudioClip("Teleport");
            yield return null;
            SceneManager.LoadScene("Arena 1");
        }
        else if(!canEscape && Application.loadedLevelName == "Arena 1")
        {
            List<string> sounds = new List<string>();
            sounds.Add("Teleport");
            sounds.Add("VillainLaugh");
            sounds.Add("DuckExcited");

            int ran = Random.Range(0, 3);

            soundMan.playAudioClip(sounds[ran]);
        }
    }
}
