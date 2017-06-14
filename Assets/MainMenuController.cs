using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private Text buttonText;
    [SerializeField]
    private GameObject[] bars;
    [SerializeField]
    private GameObject canvas;

    // Use this for initialization
    void Start () {
		foreach (GameObject bar in bars)
        {
            bar.SetActive(false);
        }
        StartCoroutine(introHasStarted());
        canvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        for (int i = 0; i < 16; i++)
        {
            Vector3 previousScale = bars[i].transform.localScale;
            previousScale.y = Mathf.Lerp(previousScale.y, spectrum[i] * 16, Time.deltaTime * 30);
            bars[i].transform.localScale = previousScale;
        } 
    }

    public void loadGame()
    {
        buttonText.fontSize = 4;
        buttonText.text = "LOADING...";
        SceneManager.LoadScene("Arena");
    }

    IEnumerator introHasStarted()
    {
        yield return new WaitForSeconds(73);
        foreach (GameObject bar in bars)
        {
            bar.SetActive(true);
        }
        canvas.SetActive(true);
    }
}
