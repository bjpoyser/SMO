using UnityEngine;
using UnityEngine.UI;

public class RocketSelection : MonoBehaviour
{
    private const string rocketPrefs = "rocket";
    private const string selectTxtPrefs = "selectTxt";

    private const string defRocket = "ALPHA";

    private int _rocketIndex;

    public int selectionIndex;

    public Image[] rocketsArray;
    private Image _rocket;

    public RectTransform rocketContainer;

    public Text priceTxt, rocketName;

    public Button selectBtn, leftBtn, rightBtn;

    private int maxIndex;

    private Price _priceScript;

    public ShopSystem shopSystemScript;

    private int _price;

    public Image glowImg;

    public void StartRocketSelection()
    {
        _rocketIndex = PlayerPrefs.GetInt(rocketPrefs);
        selectionIndex = _rocketIndex;
        maxIndex = rocketsArray.Length - 1;
        ChangeRocket(0);
    }

    public void ChangeRocket(int newSelection)
    {
        selectionIndex += newSelection;

        if (selectionIndex == 0)
        {
            leftBtn.interactable = false;
            rightBtn.interactable = true;
        }
        if (selectionIndex > 0 && selectionIndex < maxIndex)
        {
            leftBtn.interactable = true;
            rightBtn.interactable = true;
        }
        if (selectionIndex == maxIndex)
        {
            leftBtn.interactable = true;
            rightBtn.interactable = false;
        }

        DisplayRocket();
    }

    public void DisplayRocket()
    {
        if (rocketContainer.transform.childCount > 0)
            Destroy(rocketContainer.GetChild(0).gameObject);

        _rocket = Instantiate(rocketsArray[selectionIndex]);
        _rocket.rectTransform.SetParent(rocketContainer);

        _rocket.rectTransform.sizeDelta = rocketContainer.sizeDelta;
        _rocket.rectTransform.localScale = new Vector3(1, 1, 1);

        _rocket.rectTransform.localPosition = new Vector3(0,0,0);


        _priceScript = _rocket.GetComponent<Price>();
        _price = _priceScript.rocketPrice;

        _rocket.name = rocketsArray[selectionIndex].name;

        glowImg.gameObject.SetActive(false);
        selectBtn.gameObject.SetActive(true);

        rocketName.text = _rocket.name;

        if (PlayerPrefs.GetInt(_rocket.name) == 0 && _rocket.name != defRocket)
        {
            priceTxt.text = $"{_price} stars";

            if (shopSystemScript.GetTotalCoins() >= _price)
                selectBtn.interactable = true;
            else
                selectBtn.interactable = false;

            _rocket.color = Color.gray;
        }
        else
        {
            priceTxt.text = PlayerPrefs.GetString(selectTxtPrefs);
            _rocket.color = Color.white;

            if (_rocketIndex == selectionIndex)
            {
                selectBtn.gameObject.SetActive(false);
                glowImg.gameObject.SetActive(true);
            }   
            else
                selectBtn.interactable = true;
        }
    }

    public void SelectRocket()
    {
        if (PlayerPrefs.GetInt(_rocket.name) == 0 || _rocket.name == defRocket) { 
            if (shopSystemScript.SubstractCoinsFromTotal(_price))
            {
                PlayerPrefs.SetInt(_rocket.name, 1);
                _rocket.color = Color.white;
            } 
        }
        else
            PlayerPrefs.SetInt(rocketPrefs, selectionIndex);

        PlayerPrefs.SetInt(rocketPrefs, selectionIndex);
        selectBtn.gameObject.SetActive(false);
        glowImg.gameObject.SetActive(true);

        _rocketIndex = selectionIndex;
    }
}
