using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private const string altitudeTxtPref = "altitudeTxt";

    private string altitudeString;
    public GameObject gameOverPanel;
    public SkyesEngine skyesEngineScript;
    public Text altitudeTxt, numStarsTxt;
    public ShopSystem shopSysScript;
    public AudioSource mainMusic;

    private void Awake()
    {
        altitudeString = PlayerPrefs.GetString(altitudeTxtPref);
    }

    public void ShowGameOver()
    {
        mainMusic.Stop();
        gameOverPanel.SetActive(true);
        altitudeTxt.text =  $"{altitudeString}\n{skyesEngineScript.altitudeText.text}" ;
        numStarsTxt.text = $"{shopSysScript.earnedStars}";
        this.GetComponent<SkyesEngine>().isGameOn = false;
    }
}
