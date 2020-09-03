using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OtherSettings : MonoBehaviour
{
    private const string LanguagePref = "language";
    private const string vibrationPref = "vibration";
    private const string musicTxtPref = "musicTxt";
    private const string sfxTxtPref = "sfxTxt";

    private const string playTxtPref = "playTxt";
    private const string garageTxtPref = "garageTxt";
    private const string exitTxtPref = "exitTxt";
    private const string vibrationTxtPref = "vibrationTxt";
    private const string languageTxtPref = "languageTxt";

    private const string selectTxtPref = "selectTxt";
    private const string settingsTxtPref = "settingsTxt";
    private const string controlTxtPref = "controlsTxt";
    private const string resumeTxtPref = "resumeTxt";
    private const string quitTxtPref = "quitTxt";
    private const string pauseTxtPref = "pauseTxt";
    private const string altitudeTxtPref = "altitudeTxt";
    private const string playAgainTxtPref = "playAgainTxt";

    const string masterVol = "MasterVol";
    const string musicVol = "MusicVol";
    const string sfxVol = "SFXVol";
    const string settingsVol = "SettingsVol";

    public int languageIndex;

    public Text[] play;
    public Text[] garage;
    public Text[] exit;

    public Text[] select;
    public Text[] settings;
    public Text[] vibration;
    public Text[] language;
    public Text[] music;
    public Text[] sfx;

    public Text[] control;
    public Text[] resume;
    public Text[] quit;
    public Text[] pause;
    public Text[] altitude;
    public Text[] playAgain;

    public Toggle isVibrationOn;

    public Dropdown languageDD;

    public Sprite[] flagsArray;

    public AudioSource nextAudio, backAudio, selectAudio, optionAudio;

    public Slider masterSlider, musicSlider, sfxSlider, settingsSlider;

    private void Start()
    {
        InitAudio();
        InitLanguage();
        FillDD();
        SetToggle();
        ChangeStrings();
    }

    private void InitAudio()
    {
        masterSlider.value = PlayerPrefs.GetFloat(masterVol) == 0f ? 1 : PlayerPrefs.GetFloat(masterVol);
        musicSlider.value = PlayerPrefs.GetFloat(musicVol) == 0f ? 1 : PlayerPrefs.GetFloat(musicVol);
        sfxSlider.value = PlayerPrefs.GetFloat(sfxVol) == 0f ? 1 : PlayerPrefs.GetFloat(sfxVol);
        settingsSlider.value = PlayerPrefs.GetFloat(settingsVol) == 0f ? 1 : PlayerPrefs.GetFloat(settingsVol);
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
            PlayerPrefs.SetString(selectTxtPref, "SELECT");
            PlayerPrefs.SetString(settingsTxtPref, "SETTINGS");
            PlayerPrefs.SetString(controlTxtPref, "CONTROLS");
            PlayerPrefs.SetString(vibrationTxtPref, "VIBRATION");
            PlayerPrefs.SetString(languageTxtPref, "LANGUAGE");
            PlayerPrefs.SetString(musicTxtPref, "MUSIC");
            PlayerPrefs.SetString(sfxTxtPref, "SFX");
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
            PlayerPrefs.SetString(selectTxtPref, "SELECCIONAR");
            PlayerPrefs.SetString(settingsTxtPref, "OPCIONES");
            PlayerPrefs.SetString(controlTxtPref, "CONTROLES");
            PlayerPrefs.SetString(vibrationTxtPref, "VIBRACIÓN");
            PlayerPrefs.SetString(languageTxtPref, "IDIOMA");
            PlayerPrefs.SetString(musicTxtPref, "MÚSICA");
            PlayerPrefs.SetString(sfxTxtPref, "EFECTOS");
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
        foreach (Text item in music)
        {
            item.text = PlayerPrefs.GetString(musicTxtPref);
        }
        foreach (Text item in sfx)
        {
            item.text = PlayerPrefs.GetString(sfxTxtPref);
        }
    }

    public void SetVibration()
    {
        string state = isVibrationOn.isOn.ToString();
        PlayerPrefs.SetString(vibrationPref, state);
    }

    public void PlayNextAudio()
    {
        nextAudio.Play();
    }

    public void PlaySelectAudio()
    {
        selectAudio.Play();
    }

    public void PlayBackAudio()
    {
        backAudio.Play();
    }

    public void PlayOptionAudio()
    {
        optionAudio.Play();
    }
}
