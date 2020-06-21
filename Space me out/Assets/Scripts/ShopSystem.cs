using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    private const string starsPref = "stars";

    [SerializeField]
    private int _totalStars;

    public int earnedStars;

    public Text starsInGameTxt;
    public Text starsMainMenuTxt, starsInShop;

    void Start()
    {
        _totalStars = PlayerPrefs.GetInt(starsPref);
        earnedStars = 0;
        DisplayCoins();
    }

    public void DisplayCoins()
    {
        if (SceneManager.GetActiveScene().name == "Game")
            starsInGameTxt.text = earnedStars + "";
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            starsMainMenuTxt.text = PlayerPrefs.GetInt(starsPref) + "";
            starsInShop.text = PlayerPrefs.GetInt(starsPref) + "";
        }
    }

    public void AddCoins(int value)
    {
        earnedStars += value;
        _totalStars += value;
        PlayerPrefs.SetInt(starsPref, _totalStars);
    }

    public int GetTotalCoins()
    {
        return PlayerPrefs.GetInt(starsPref);
    }

    public bool SubstractCoinsFromTotal(int value)
    {
        if (_totalStars >= value)
        {
            _totalStars -= value;
            PlayerPrefs.SetInt(starsPref, _totalStars);
            DisplayCoins();
            return true;
        }
        else
            return false;
    }
}
