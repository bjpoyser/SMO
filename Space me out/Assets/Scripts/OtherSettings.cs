using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OtherSettings : MonoBehaviour
{
    private const string LanguagePref = "language";
    private const string vibrationPref = "vibration";

    private const string playTxtPref = "playTxt";
    private const string garageTxtPref = "garageTxt";
    private const string exitTxtPref = "exitTxt";
    private const string vibrationTxtPref = "vibrationTxt";
    private const string languageTxtPref = "languageTxt";

    private const string backTxtPref = "backTxt";
    private const string selectTxtPref = "selectTxt";
    private const string settingsTxtPref = "settingsTxt";
    private const string controlTxtPref = "controlsTxt";
    private const string resumeTxtPref = "resumeTxt";
    private const string quitTxtPref = "quitTxt";
    private const string pauseTxtPref = "pauseTxt";
    private const string altitudeTxtPref = "altitudeTxt";
    private const string playAgainTxtPref = "playAgainTxt";

    public int languageIndex;

    public Text[] play;
    public Text[] garage;
    public Text[] exit;

    public Text[] back;
    public Text[] select;
    public Text[] settings;
    public Text[] vibration;
    public Text[] language;

    public Text[] control;
    public Text[] resume;
    public Text[] quit;
    public Text[] pause;
    public Text[] altitude;
    public Text[] playAgain;

    public Toggle isVibrationOn;

    public Dropdown languageDD;

    public Sprite[] flagsArray;

    private void Start()
    {
        InitLanguage();
        FillDD();
        SetToggle();
        ChangeStrings();
    }

    private void InitLanguage()
    {
        if (PlayerPrefs.GetInt(LanguagePref) == 0)
        {
            SystemLanguage lang = Application.systemLanguage;
            if (lang == SystemLanguage.English)
                PlayerPrefs.SetInt(LanguagePref, 1);
            else if (lang == SystemLanguage.Spanish)
                PlayerPrefs.SetInt(LanguagePref, 2);
            else
                PlayerPrefs.SetInt(LanguagePref, 1);
        }
    }

    private void FillDD()
    {
        languageDD.ClearOptions();
        List<Dropdown.OptionData> langItems = new List<Dropdown.OptionData>();

        foreach(var flag in flagsArray)
        {
            var flagOption = new Dropdown.OptionData(flag.name, flag);
            langItems.Add(flagOption);
        }

        languageDD.AddOptions(langItems);

        languageDD.value = PlayerPrefs.GetInt(LanguagePref)-1;
    }

    private void SetToggle()
    {
        bool state;

        if (string.IsNullOrEmpty(PlayerPrefs.GetString(vibrationPref)))
            state = true;
        else
            state = bool.Parse(PlayerPrefs.GetString(vibrationPref));

        isVibrationOn.isOn = state;
    }

    public void SetLanguage()
    {
        int newLanguage = languageDD.value + 1;
        PlayerPrefs.SetInt(LanguagePref, newLanguage);
        ChangeStrings();
    }

    public void ChangeStrings()
    {
        if (PlayerPrefs.GetInt(LanguagePref) == 1)
        {
            PlayerPrefs.SetString(playTxtPref, "PLAY");
            PlayerPrefs.SetString(garageTxtPref, "GARAGE");
            PlayerPrefs.SetString(exitTxtPref, "EXIT");
            PlayerPrefs.SetString(resumeTxtPref, "RESUME");
            PlayerPrefs.SetString(quitTxtPref, "QUIT");
            PlayerPrefs.SetString(pauseTxtPref, "PAUSE");
            PlayerPrefs.SetString(altitudeTxtPref, "ALTITUDE");
            PlayerPrefs.SetString(playAgainTxtPref, "PLAY AGAIN!");
            PlayerPrefs.SetString(backTxtPref, "BACK");
            PlayerPrefs.SetString(selectTxtPref, "SELECT");
            PlayerPrefs.SetString(settingsTxtPref, "SETTINGS");
            PlayerPrefs.SetString(controlTxtPref, "CONTROLS");
            PlayerPrefs.SetString(vibrationTxtPref, "VIBRATION");
            PlayerPrefs.SetString(languageTxtPref, "LANGUAGE");
        }
        if (PlayerPrefs.GetInt(LanguagePref) == 2)
        {
            PlayerPrefs.SetString(playTxtPref, "JUGAR");
            PlayerPrefs.SetString(garageTxtPref, "GARAJE");
            PlayerPrefs.SetString(exitTxtPref, "SALIR");
            PlayerPrefs.SetString(resumeTxtPref, "CONTINUAR");
            PlayerPrefs.SetString(quitTxtPref, "SALIR");
            PlayerPrefs.SetString(pauseTxtPref, "PAUSA");
            PlayerPrefs.SetString(altitudeTxtPref, "ALTURA");
            PlayerPrefs.SetString(playAgainTxtPref, "VOLVER A JUGAR!");
            PlayerPrefs.SetString(backTxtPref, "VOLVER");
            PlayerPrefs.SetString(selectTxtPref, "SELECCIONAR");
            PlayerPrefs.SetString(settingsTxtPref, "OPCIONES");
            PlayerPrefs.SetString(controlTxtPref, "CONTROLES");
            PlayerPrefs.SetString(vibrationTxtPref, "VIBRACIÓN");
            PlayerPrefs.SetString(languageTxtPref, "IDIOMA");
        }
        languageIndex = PlayerPrefs.GetInt(LanguagePref);
        ChangeTexts();
    }

    private void ChangeTexts()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            foreach (Text item in play)
            {
                item.text = PlayerPrefs.GetString(playTxtPref);
            }
            foreach (Text item in garage)
            {
                item.text = PlayerPrefs.GetString(garageTxtPref);
            }
            foreach (Text item in exit)
            {
                item.text = PlayerPrefs.GetString(exitTxtPref);
            }
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            foreach (Text item in resume)
            {
                item.text = PlayerPrefs.GetString(resumeTxtPref);
            }
            foreach (Text item in quit)
            {
                item.text = PlayerPrefs.GetString(quitTxtPref);
            }
            foreach (Text item in pause)
            {
                item.text = PlayerPrefs.GetString(pauseTxtPref);
            }
            foreach (Text item in altitude)
            {
                item.text = PlayerPrefs.GetString(altitudeTxtPref);
            }
            foreach (Text item in playAgain)
            {
                item.text = PlayerPrefs.GetString(playAgainTxtPref);
            }
        }
        foreach (Text item in back){
            item.text = PlayerPrefs.GetString(backTxtPref);
        }
        foreach (Text item in select)
        {
            item.text = PlayerPrefs.GetString(selectTxtPref);
        }
        foreach (Text item in settings)
        {
            item.text = PlayerPrefs.GetString(settingsTxtPref);
        }
        foreach (Text item in control)
        {
            item.text = PlayerPrefs.GetString(controlTxtPref);
        }
        foreach (Text item in vibration)
        {
            item.text = PlayerPrefs.GetString(vibrationTxtPref);
        }
        foreach (Text item in language)
        {
            item.text = PlayerPrefs.GetString(languageTxtPref);
        }
    }

    public void SetVibration()
    {
        string state = isVibrationOn.isOn.ToString();
        PlayerPrefs.SetString(vibrationPref, state);
    }
}
