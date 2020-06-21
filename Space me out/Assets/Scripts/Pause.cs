using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public SkyesEngine skyesEngineScript;
    public Image pauseBtn;
    public GameObject transitionMainMenuGO;

    public void QuitGame()
    {
        StartCoroutine(NextScene("MainMenu"));
    }

    public void PlayAgain()
    {
        StartCoroutine(NextScene("Game"));
    }

    public IEnumerator NextScene(string scene)
    {
        skyesEngineScript.isGameOn = false;

        transitionMainMenuGO.SetActive(true);
        transitionMainMenuGO.GetComponent<Animator>().SetTrigger(scene);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(scene);
    }

    public void SetGameState(bool state)
    {
        skyesEngineScript.isGameOn = state;
        pauseBtn.enabled = state;
    }
}
