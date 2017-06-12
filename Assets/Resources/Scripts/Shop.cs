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
    private bool bubbleBought = false;
    private int dmgUps = 0;
    private int speedUps = 0;

    private SoundManager soundMan;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        soundMan = FindObjectOfType<SoundManager>();
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
        if (!bubbleBought)
        {
            soundMan.playAudioClip("ShopClerkSympathetic");
            dialogueText.text = "I see, the Bubble. Developer Byn's choice. That will be 5.000 Kleines Yen.";
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
            dialogueText.text = "Press \"y\" if you wanna buy it, or \"n\" if you don't.";
            yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
            if (Input.GetKey("y"))
            {
                if (player.GetComponent<Player>().returnKleinesYen() >= 5000)
                {
                    player.GetComponent<Player>().payYen(5000);
                    GameObject bubble = Instantiate(bubblePrefab) as GameObject;
                    bubbleBought = true;
                    dialogueText.text = "Thanks for your purchase!";
                }
                else
                {
                    dialogueText.text = "Oh, you don't have enough Kleines Yen!";
                    soundMan.playAudioClip("ShopClerkSurprised");
                }
            }
            else if (Input.GetKey("n"))
            {
                dialogueText.text = "Okay, maybe next time!";
                soundMan.playAudioClip("ShopClerkWondering");
            }
            yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        }
        else
        {
            dialogueText.text = "It appears you already own a bubble. Maybe you're interested in buying something else?";
            soundMan.playAudioClip("ShopClerkSurprised");
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        }

        dialogueBox.SetActive(false);
        processing = false;
    }

    IEnumerator buySword()
    {
        switch (dmgUps)
        {
            case 0:
                soundMan.playAudioClip("ShopClerkSympathetic");
                float ran = Random.value;
                if (ran > 0.66)
                {
                    dialogueText.text = "I see, DMG upgrade. Good choice. That will be 300 Kleines yen.";
                }
                else if (ran > 0.33)
                {
                    dialogueText.text = "Ah, the good old DMG upgrade. 300 Kleines yen please.";
                }
                else
                {
                    dialogueText.text = "DMG upgrade? 300 Kleines Yen and we're good to go.";
                }
                yield return new WaitForSeconds(0.5f);
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueText.text = "Press \"y\" if you wanna buy it, or \"n\" if you don't.";
                yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
                if (Input.GetKey("y"))
                {
                    if (player.GetComponent<Player>().returnKleinesYen() >= 300)
                    {
                        player.GetComponent<Player>().payYen(300);
                        player.GetComponent<Player>().damage++;
                        dmgUps++;
                        dialogueText.text = "Thanks for your purchase!";
                    }
                    else
                    {
                        dialogueText.text = "Oh, you don't have enough Kleines Yen!";
                        soundMan.playAudioClip("ShopClerkSurprised");
                    }
                }
                else if (Input.GetKey("n"))
                {
                    dialogueText.text = "Okay, maybe next time!";
                    soundMan.playAudioClip("ShopClerkWondering");
                }
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueBox.SetActive(false);
                processing = false;
                break;
            case 1:
                soundMan.playAudioClip("ShopClerkSympathetic");
                float ran1 = Random.value;
                if (ran1 > 0.66)
                {
                    dialogueText.text = "I see, DMG upgrade. Good choice. That will be 600 Kleines yen.";
                }
                else if (ran1 > 0.33)
                {
                    dialogueText.text = "Ah, the good old DMG upgrade. 600 Kleines yen please.";
                }
                else
                {
                    dialogueText.text = "DMG upgrade? 600 Kleines Yen and we're good to go.";
                }
                yield return new WaitForSeconds(0.5f);
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueText.text = "Press \"y\" if you wanna buy it, or \"n\" if you don't.";
                yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
                if (Input.GetKey("y"))
                {
                    if (player.GetComponent<Player>().returnKleinesYen() >= 600)
                    {
                        player.GetComponent<Player>().payYen(600);
                        player.GetComponent<Player>().damage++;
                        dmgUps++;
                        dialogueText.text = "Thanks for your purchase!";
                    }
                    else
                    {
                        dialogueText.text = "Oh, you don't have enough Kleines Yen!";
                        soundMan.playAudioClip("ShopClerkSurprised");
                    }
                }
                else if (Input.GetKey("n"))
                {
                    dialogueText.text = "Okay, maybe next time!";
                    soundMan.playAudioClip("ShopClerkWondering");
                }
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueBox.SetActive(false);
                processing = false;
                break;
            case 2:
                soundMan.playAudioClip("ShopClerkSympathetic");
                float ran2 = Random.value;
                if (ran2 > 0.66)
                {
                    dialogueText.text = "I see, DMG upgrade. Good choice. That will be 900 Kleines yen.";
                }
                else if (ran2 > 0.33)
                {
                    dialogueText.text = "Ah, the good old DMG upgrade. 900 Kleines yen please.";
                }
                else
                {
                    dialogueText.text = "DMG upgrade? 900 Kleines Yen and we're good to go.";
                }
                yield return new WaitForSeconds(0.5f);
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueText.text = "Press \"y\" if you wanna buy it, or \"n\" if you don't.";
                yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
                if (Input.GetKey("y"))
                {
                    if (player.GetComponent<Player>().returnKleinesYen() >= 900)
                    {
                        player.GetComponent<Player>().payYen(900);
                        player.GetComponent<Player>().damage++;
                        dmgUps++;
                        dialogueText.text = "Thanks for your purchase!";
                    }
                    else
                    {
                        dialogueText.text = "Oh, you don't have enough Kleines Yen!";
                        soundMan.playAudioClip("ShopClerkSurprised");
                    }
                }
                else if (Input.GetKey("n"))
                {
                    dialogueText.text = "Okay, maybe next time!";
                    soundMan.playAudioClip("ShopClerkWondering");
                }
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueBox.SetActive(false);
                processing = false;
                break;
            case 3:
                soundMan.playAudioClip("ShopClerkSympathetic");
                float ran3 = Random.value;
                if (ran3 > 0.66)
                {
                    dialogueText.text = "I see, DMG upgrade. Good choice. That will be 1.200 Kleines yen.";
                }
                else if (ran3 > 0.33)
                {
                    dialogueText.text = "Ah, the good old DMG upgrade. 1.200 Kleines yen please.";
                }
                else
                {
                    dialogueText.text = "DMG upgrade? 1.200 Kleines Yen and we're good to go.";
                }
                yield return new WaitForSeconds(0.5f);
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueText.text = "Press \"y\" if you wanna buy it, or \"n\" if you don't.";
                yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
                if (Input.GetKey("y"))
                {
                    if (player.GetComponent<Player>().returnKleinesYen() >= 1200)
                    {
                        player.GetComponent<Player>().payYen(1200);
                        player.GetComponent<Player>().damage++;
                        dmgUps++;
                        dialogueText.text = "Thanks for your purchase!";
                    }
                    else
                    {
                        dialogueText.text = "Oh, you don't have enough Kleines Yen!";
                        soundMan.playAudioClip("ShopClerkSurprised");
                    }
                }
                else if (Input.GetKey("n"))
                {
                    dialogueText.text = "Okay, maybe next time!";
                    soundMan.playAudioClip("ShopClerkWondering");
                }
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueBox.SetActive(false);
                processing = false;
                break;
            case 4:
                soundMan.playAudioClip("ShopClerkSurprised");
                dialogueText.text = "I'm sorry, but you're powerful enough already. I cannot sell you any more of those.";
                yield return new WaitForSeconds(0.5f);
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueBox.SetActive(false);
                processing = false;
                break;
        }
    }

    IEnumerator buyNikeVans()
    {
        switch (speedUps)
        {
            case 0:
                soundMan.playAudioClip("ShopClerkSympathetic");
                float ran = Random.value;
                if (ran > 0.66)
                {
                    dialogueText.text = "I see, Speed-Upgrade. Great choice. That will be 300 Kleines yen.";
                }
                else if (ran > 0.33)
                {
                    dialogueText.text = "Ah, the good old Speed-Upgrade. 300 Kleines yen please.";
                }
                else
                {
                    dialogueText.text = "Speed-Upgrade? 300 Kleines Yen and we're good to go.";
                }
                yield return new WaitForSeconds(0.5f);
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueText.text = "Press \"y\" if you wanna buy it, or \"n\" if you don't.";
                yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
                if (Input.GetKey("y"))
                {
                    if (player.GetComponent<Player>().returnKleinesYen() >= 300)
                    {
                        player.GetComponent<Player>().payYen(300);
                        player.GetComponent<Player>().speed++;
                        speedUps++;
                        dialogueText.text = "Thanks for your purchase!";
                    }
                    else
                    {
                        dialogueText.text = "Oh, you don't have enough Kleines Yen!";
                        soundMan.playAudioClip("ShopClerkSurprised");
                    }
                }
                else if (Input.GetKey("n"))
                {
                    dialogueText.text = "Okay, maybe next time!";
                    soundMan.playAudioClip("ShopClerkWondering");
                }
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueBox.SetActive(false);
                processing = false;
                break;
            case 1:
                soundMan.playAudioClip("ShopClerkSympathetic");
                float ran1 = Random.value;
                if (ran1 > 0.66)
                {
                    dialogueText.text = "I see, Speed-Upgrade. Good choice. That will be 600 Kleines yen.";
                }
                else if (ran1 > 0.33)
                {
                    dialogueText.text = "Ah, the good old Speed-Upgrade. 600 Kleines yen please.";
                }
                else
                {
                    dialogueText.text = "Speed-Upgrade? 600 Kleines Yen and we're good to go.";
                }
                yield return new WaitForSeconds(0.5f);
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueText.text = "Press \"y\" if you wanna buy it, or \"n\" if you don't.";
                yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
                if (Input.GetKey("y"))
                {
                    if (player.GetComponent<Player>().returnKleinesYen() >= 600)
                    {
                        player.GetComponent<Player>().payYen(600);
                        player.GetComponent<Player>().speed++;
                        speedUps++;
                        dialogueText.text = "Thanks for your purchase!";
                    }
                    else
                    {
                        dialogueText.text = "Oh, you don't have enough Kleines Yen!";
                        soundMan.playAudioClip("ShopClerkSurprised");
                    }
                }
                else if (Input.GetKey("n"))
                {
                    dialogueText.text = "Okay, maybe next time!";
                    soundMan.playAudioClip("ShopClerkWondering");
                }
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueBox.SetActive(false);
                processing = false;
                break;
            case 2:
                soundMan.playAudioClip("ShopClerkSympathetic");
                float ran2 = Random.value;
                if (ran2 > 0.66)
                {
                    dialogueText.text = "I see, Speed-Upgrade. Good choice. That will be 900 Kleines yen.";
                }
                else if (ran2 > 0.33)
                {
                    dialogueText.text = "Ah, the good old Speed-Upgrade. 900 Kleines yen please.";
                }
                else
                {
                    dialogueText.text = "Speed-Upgrade? 900 Kleines Yen and we're good to go.";
                }
                yield return new WaitForSeconds(0.5f);
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueText.text = "Press \"y\" if you wanna buy it, or \"n\" if you don't.";
                yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
                if (Input.GetKey("y"))
                {
                    if (player.GetComponent<Player>().returnKleinesYen() >= 900)
                    {
                        player.GetComponent<Player>().payYen(900);
                        player.GetComponent<Player>().speed++;
                        speedUps++;
                        dialogueText.text = "Thanks for your purchase!";
                    }
                    else
                    {
                        dialogueText.text = "Oh, you don't have enough Kleines Yen!";
                        soundMan.playAudioClip("ShopClerkSurprised");
                    }
                }
                else if (Input.GetKey("n"))
                {
                    dialogueText.text = "Okay, maybe next time!";
                    soundMan.playAudioClip("ShopClerkWondering");
                }
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueBox.SetActive(false);
                processing = false;
                break;
            case 3:
                soundMan.playAudioClip("ShopClerkSympathetic");
                float ran3 = Random.value;
                if (ran3 > 0.66)
                {
                    dialogueText.text = "I see, Speed-Upgrade. Good choice. That will be 1.200 Kleines yen.";
                }
                else if (ran3 > 0.33)
                {
                    dialogueText.text = "Ah, the good old Speed-Upgrade. 1.200 Kleines yen please.";
                }
                else
                {
                    dialogueText.text = "Speed-Upgrade? 1.200 Kleines Yen and we're good to go.";
                }
                yield return new WaitForSeconds(0.5f);
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueText.text = "Press \"y\" if you wanna buy it, or \"n\" if you don't.";
                yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
                if (Input.GetKey("y"))
                {
                    if (player.GetComponent<Player>().returnKleinesYen() >= 1200)
                    {
                        player.GetComponent<Player>().payYen(1200);
                        player.GetComponent<Player>().speed++;
                        speedUps++;
                        dialogueText.text = "Thanks for your purchase!";
                    }
                    else
                    {
                        dialogueText.text = "Oh, you don't have enough Kleines Yen!";
                        soundMan.playAudioClip("ShopClerkSurprised");
                    }
                }
                else if (Input.GetKey("n"))
                {
                    dialogueText.text = "Okay, maybe next time!";
                    soundMan.playAudioClip("ShopClerkWondering");
                }
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueBox.SetActive(false);
                processing = false;
                break;
            case 4:
                soundMan.playAudioClip("ShopClerkSurprised");
                dialogueText.text = "I'm sorry, but you've almost hit Sanic-Speed already. I cannot sell you any more of those.";
                yield return new WaitForSeconds(0.5f);
                yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
                dialogueBox.SetActive(false);
                processing = false;
                break;
        }
    }

    IEnumerator buyCoinMagnet()
    {
        if (!player.GetComponent<Player>().hasCoinMagnet)
        {
            soundMan.playAudioClip("ShopClerkSympathetic");
            float ran = Random.value;
            if (ran > 0.66)
            {
                dialogueText.text = "I see, the Coin Magnet. Good choice. That will be 1.000 Kleines Yen.";
            }
            else if (ran > 0.33)
            {
                dialogueText.text = "Ah, the good old Coin Magnet. 1.000 Kleines Yen please.";
            }
            else
            {
                dialogueText.text = "Coin magnet? 1.000 Kleines Yen and we're good to go.";
            }
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
            dialogueText.text = "Press \"y\" if you wanna buy it, or \"n\" if you don't.";
            yield return new WaitUntil(() => Input.GetKeyDown("y") == true || Input.GetKeyDown("n"));
            if (Input.GetKey("y"))
            {
                if (player.GetComponent<Player>().returnKleinesYen() >= 1000 && player.GetComponent<Player>().hasCoinMagnet == false)
                {
                    player.GetComponent<Player>().payYen(1000);
                    player.GetComponent<Player>().hasCoinMagnet = true;
                    dialogueText.text = "Thanks for your purchase!";
                }
                else
                {
                    dialogueText.text = "Oh, you don't have enough Kleines Yen!";
                    soundMan.playAudioClip("ShopClerkSurprised");
                }
            }
            else if (Input.GetKey("n"))
            {
                dialogueText.text = "Okay, maybe next time!";
                soundMan.playAudioClip("ShopClerkWondering");
            }
            yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        }
        else
        {
            soundMan.playAudioClip("ShopClerkSurprised");
            dialogueText.text = "Looks like you already own this awesome coin magnet!";
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKeyDown("e") == true);
        }
        dialogueBox.SetActive(false);
        processing = false;
    }
}
