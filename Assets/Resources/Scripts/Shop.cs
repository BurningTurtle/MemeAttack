using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private Text dialogueText;
    private GameObject player;

    [SerializeField] GameObject bubblePrefab;

    private bool processing;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 3)
            {
                if(name == "sword" && !processing)
                {
                    processing = true;
                    dialogueBox.SetActive(true);
                    StartCoroutine(buySword());
                }
                else if(name == "nikeVans" && !processing)
                {
                    processing = true;
                    dialogueBox.SetActive(true);
                    StartCoroutine(buyNikeVans());
                }
                else if(name == "coinMagnet" && !processing)
                {
                    processing = true;
                    dialogueBox.SetActive(true);
                    StartCoroutine(buyCoinMagnet());
                }
                else if(name == "shopBubble" && !processing)
                {
                    processing = true;
                    dialogueBox.SetActive(true);
                    StartCoroutine(buyBubble());
                }
            }
        }
    }

    IEnumerator buyBubble()
    {
        dialogueText.text = "I see, the Bubble. Developer Byn's choice. That will be xxx kleines yen.";

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Press \"y\" for yes, \"n\" for no";
        yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
        if (Input.GetKey("y"))
        {
            if (player.GetComponent<Player>().returnKleinesYen() >= 10000)
            {
                player.GetComponent<Player>().payYen(10000);
                GameObject bubble = Instantiate(bubblePrefab) as GameObject;
                dialogueText.text = "Thanks for your purchase!";
            }
            else
            {
                dialogueText.text = "Oh, you don't have enough kleines yen!";
            }
        }
        else if (Input.GetKey("n"))
        {
            dialogueText.text = "Okay, maybe next time!";
        }
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);
        processing = false;
    }

    IEnumerator buySword()
    {
        float ran = Random.value;
        if(ran > 0.66)
        {
            dialogueText.text = "I see, DMG upgrade. Good choice. That will be xxx kleines yen.";
        }
        else if(ran > 0.33)
        {
            dialogueText.text = "Ah, the good old DMG upgrade. xxx kleines yen please.";
        }
        else
        {
            dialogueText.text = "DMG upgrade? xxx kleines yen and we're good to go.";
        }
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Press \"y\" for yes, \"n\" for no";
        yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
        if (Input.GetKey("y"))
        {
            if(player.GetComponent<Player>().returnKleinesYen() >= 10)
            {
                player.GetComponent<Player>().payYen(10);
                player.GetComponent<Player>().damage++;
                dialogueText.text = "Thanks for your purchase!";
            }
            else
            {
                dialogueText.text = "Oh, you don't have enough kleines yen!";
            }
        }
        else if(Input.GetKey("n"))
        {
            dialogueText.text = "Okay, maybe next time!";
        }
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);
        processing = false;
    }

    IEnumerator buyNikeVans()
    {
        float ran = Random.value;
        if (ran > 0.66)
        {
            dialogueText.text = "I see, speed upgrade. Good choice. That will be xxx kleines yen.";
        }
        else if (ran > 0.33)
        {
            dialogueText.text = "Ah, the good old speed upgrade. xxx kleines yen please.";
        }
        else
        {
            dialogueText.text = "Speed upgrade? xxx kleines yen and we're good to go.";
        }
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Press \"y\" for yes, \"n\" for no";
        yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
        if (Input.GetKey("y"))
        {
            if (player.GetComponent<Player>().returnKleinesYen() >= 10)
            {
                player.GetComponent<Player>().payYen(10);
                player.GetComponent<Player>().speed++;
                dialogueText.text = "Thanks for your purchase!";
            }
            else
            {
                dialogueText.text = "Oh, you don't have enough kleines yen!";
            }
        }
        else if (Input.GetKey("n"))
        {
            dialogueText.text = "Okay, maybe next time!";
        }
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);
        processing = false;
    }

    IEnumerator buyCoinMagnet()
    {
        float ran = Random.value;
        if (ran > 0.66)
        {
            dialogueText.text = "I see, the coin magnet. Good choice. That will be xxx kleines yen.";
        }
        else if (ran > 0.33)
        {
            dialogueText.text = "Ah, the good old coin magnet. xxx kleines yen please.";
        }
        else
        {
            dialogueText.text = "Coin magnet? xxx kleines yen and we're good to go.";
        }
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueText.text = "Press \"y\" for yes, \"n\" for no";
        yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
        if (Input.GetKey("y"))
        {
            if (player.GetComponent<Player>().returnKleinesYen() >= 10 && player.GetComponent<Player>().hasCoinMagnet == false)
            {
                player.GetComponent<Player>().payYen(10);
                player.GetComponent<Player>().hasCoinMagnet = true;
                dialogueText.text = "Thanks for your purchase!";
            }
            else if(player.GetComponent<Player>().hasCoinMagnet == true)
            {
                dialogueText.text = "Looks like you already own a coin magnet!";
            }
            else
            {
                dialogueText.text = "Oh, you don't have enough kleines yen!";
            }
        }
        else if (Input.GetKey("n"))
        {
            dialogueText.text = "Okay, maybe next time!";
        }
        yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        dialogueBox.SetActive(false);
        processing = false;
    }
}
