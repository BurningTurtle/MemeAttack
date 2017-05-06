using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour {

    public Image heartsIngame;
    public Sprite[] hearts;
    private GameObject player;
    public Text waveDisplay;
    public GameObject arenaController;

    [SerializeField] private Image activeItem;
    [SerializeField] private Image passiveItem1;
    [SerializeField] private Image passiveItem2;
    [SerializeField] private Image passiveItem3;

    [SerializeField] private Sprite seitenbacherItem;

    [SerializeField] private Sprite passiveItem;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
        int heartPoints = player.GetComponent<Player>().health / 5;
        Debug.Log(heartPoints);
        if(player.GetComponent<Player>().health >= 0)
        {
            heartsIngame.sprite = hearts[heartPoints];
        }
        // -1 because ArenaController increases wave right after spawn.
        int wave = arenaController.GetComponent<ArenaController>().wave - 1;
        waveDisplay.text = "WAVE\n" + wave;
    }

    IEnumerator newWaveAnimation()
    {
        for(int i = 1; i < 3; i++)
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
}
