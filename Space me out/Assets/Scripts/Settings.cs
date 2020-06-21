using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    private const string firstTimePref = "FirstTime";
    private const string starsPref = "stars";

    public GameObject pausePanel, settingsPanel, controlPanel, garagePanel;

    public ControlSettings controlScript;

    public Pause pauseScript;

    public RocketSelection rocketSelectionScript;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(firstTimePref) == 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt(starsPref, 0);
            PlayerPrefs.SetInt(firstTimePref, 1);
        }
    }

    public void PauseGame()
    {
        settingsPanel.SetActive(false);
        controlPanel.SetActive(false);

        if (SceneManager.GetActiveScene().name == "MainMenu")
            garagePanel.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Game")
        {
            pausePanel.SetActive(true);
            pauseScript.SetGameState(false);
        }
    }

    public void ResumeGame()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            pausePanel.SetActive(false);
            pauseScript.SetGameState(true);
        }
    }

    public void SettingsMenu()
    {
        if (SceneManager.GetActiveScene().name == "Game")
            pausePanel.SetActive(false);

        if (SceneManager.GetActiveScene().name == "MainMenu")
            garagePanel.SetActive(false);

        controlPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ControlsMenu()
    {
        if (SceneManager.GetActiveScene().name == "Game")
            pausePanel.SetActive(false);

        if (SceneManager.GetActiveScene().name == "MainMenu")
            garagePanel.SetActive(false);

        settingsPanel.SetActive(false);
        controlPanel.SetActive(true);
        
        controlScript.StartControlSettings();
    }

    public void GarageMenu()
    {
        settingsPanel.SetActive(false);
        controlPanel.SetActive(false);
        garagePanel.SetActive(true);
        rocketSelectionScript.StartRocketSelection();
    }
}
