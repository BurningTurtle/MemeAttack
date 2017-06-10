using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MasterSwordItem : MonoBehaviour
{

    private GameObject player;
    private SoundManager soundMan;
    private SpriteRenderer sr;
    private bool pickedUp = false;
    private GameObject uic;

    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private Text dialogueText;

    void Start()
    {
        player = GameObject.Find("Player");
        soundMan = FindObjectOfType<SoundManager>();
        sr = GetComponent<SpriteRenderer>();
        uic = GameObject.Find("UIController");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Color colour = sr.material.color;
            colour.a = 0;
            sr.material.color = colour;
            if (!pickedUp)
            {
                pickedUp = true;
                StartCoroutine(pickedUpCoroutine());
            }
        }
    }

    IEnumerator pickedUpCoroutine()
    {   
        dialogueBox.SetActive(true);
        dialogueText.text = "This is the Master Sword.";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "It might be familiar to you.";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Once this explanation is over, a song will start playing, and you will turn into Link.";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "As Link, you are immortal. Try to get as much money as you can.";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Later on, you will transform to Dark Link and enemies will stop dropping money.";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "In order to get a big reward, you have to swing your sword to the music's bass.";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "It's important.";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Once the song is over, the simulation will calculate your \"Hits To The Bass Rate\".";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Don't hit enemies when there is no bass!";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "The better you perform, the more money you will get.";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Have fun!";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "One last hint: Don't swing your sword too fast.";
        yield return new WaitForSeconds(0.25f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);

        dialogueBox.SetActive(false);
        player.GetComponent<Player>().transformToLink();
        soundMan.playZelda();
    }


}
