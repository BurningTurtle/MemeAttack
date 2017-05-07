using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public Image heartsIngame;
    public Sprite[] hearts;
    public Image active1, passive1, passive2, passive3;
    public Sprite seitenbacherSprite;
    public Sprite nikeVansSprite;
    public Sprite emptyPassive;
    private GameObject player;
    public Text waveDisplay;
    public GameObject arenaController;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int heartPoints = player.GetComponent<Player>().health / 5;
        Debug.Log(heartPoints);
        if (player.GetComponent<Player>().health >= 0)
        {
            heartsIngame.sprite = hearts[heartPoints];
        }
        // -1 because ArenaController increases wave right after spawn.
        int wave = arenaController.GetComponent<ArenaController>().wave - 1;
        waveDisplay.text = "WAVE\n" + wave;
    }

    IEnumerator newWaveAnimation()
    {
        for (int i = 1; i < 3; i++)
        {
            waveDisplay.CrossFadeAlpha(0f, 0.25f, true);
            yield return new WaitForSeconds(0.25f);
            waveDisplay.CrossFadeAlpha(1f, 0.25f, true);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void startNewWaveAnimation()
    {
        StartCoroutine(newWaveAnimation());
    }

    IEnumerator seitenbacherCoroutine()
    {
        if (passive1.sprite.name == "ItemSlotPassive")
        {
            passive1.sprite = seitenbacherSprite;
            yield return new WaitForSeconds(10f);
            passive1.sprite = emptyPassive;
        }
        else if (passive2.sprite.name == "ItemSlotPassive")
        {
            passive2.sprite = seitenbacherSprite;
            yield return new WaitForSeconds(10f);
            passive2.sprite = emptyPassive;
        }
        else if (passive3.sprite.name == "ItemSlotPassive")
        {
            passive3.sprite = seitenbacherSprite;
            yield return new WaitForSeconds(10f);
            passive3.sprite = emptyPassive;
        }
    }

    IEnumerator nikeVansCoroutine()
    {
        if (passive1.sprite.name == "ItemSlotPassive")
        {
            passive1.sprite = nikeVansSprite;
            yield return new WaitForSeconds(10f);
            passive1.sprite = emptyPassive;
        }
        else if (passive2.sprite.name == "ItemSlotPassive")
        {
            passive2.sprite = nikeVansSprite;
            yield return new WaitForSeconds(10f);
            passive2.sprite = emptyPassive;
        }
        else if (passive3.sprite.name == "ItemSlotPassive")
        {
            passive3.sprite = nikeVansSprite;
            yield return new WaitForSeconds(10f);
            passive3.sprite = emptyPassive;
        }
    }

    public void seitenbacher()
    {
        StartCoroutine(seitenbacherCoroutine());
    }

    public void nikeVans()
    {
        StartCoroutine(nikeVansCoroutine());
    }
}
