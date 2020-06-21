using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlSettings : MonoBehaviour
{
    private const string controlPref = "control";

    public RocketController rocketScript;

    public GameObject leftPanelGO, rightPanelGO, leftBtnGO, rightBtnGO;
    public Animator previewAni;
    
    public int option;

    public Button btnPrev, btnNext, btnSelect;

    public Image glowImg;


    public void StartControlSettings()
    {
        option = PlayerPrefs.GetInt(controlPref);
        ChangeControl();
        ChargePreview();
    }

    //Cambiar los controles
    public void ChangeControl()
    {
        DisableGO(btnSelect.gameObject);

        if (SceneManager.GetActiveScene().name == "Game")
        {
            rocketScript.controlTypeIndex = option;

            if (option == 1 || option == 3)
                rocketScript.SetAngle(-7f);
            else
                rocketScript.SetAngle(-20f);

            DisableControllers();

            switch (option)
            {
                case 1:
                    EnablePanelController();
                    break;
                case 3:
                    EnableButtonController();
                    break;
            }
        }
        glowImg.enabled = true;
        PlayerPrefs.SetInt(controlPref, option); 
    }

    //Encender Control de Paneles
    public void EnablePanelController()
    {
        leftPanelGO.SetActive(true);
        rightPanelGO.SetActive(true);
    }

    //Apagar Control de Paneles
    public void DisableControllers()
    {
        leftPanelGO.SetActive(false);
        rightPanelGO.SetActive(false);
        leftBtnGO.SetActive(false);
        rightBtnGO.SetActive(false);
    }

    //Encender Control de Botones
    public void EnableButtonController()
    {
        leftBtnGO.SetActive(true);
        rightBtnGO.SetActive(true);
    }

    public void EnableGO(GameObject go)
    {
        go.SetActive(true);
    }

    public void DisableGO(GameObject go)
    {
        go.SetActive(false);
    }

    public void SetOption(int newOp)
    {
        option += newOp;
        ChargePreview();
    }

    private void ChargePreview()
    {

        EnableGO(btnSelect.gameObject);

        if (PlayerPrefs.GetInt(controlPref) == option)
        {
            DisableGO(btnSelect.gameObject);
            glowImg.enabled = true;
        }
        else
        {
            glowImg.enabled = false;
        }
            

        switch (option)
        {
            case 0:
                previewAni.SetTrigger("gyro");
                DisableGO(btnPrev.gameObject);
                EnableGO(btnNext.gameObject);
                break;
            case 1:
                previewAni.SetTrigger("panel");
                EnableGO(btnPrev.gameObject);
                EnableGO(btnNext.gameObject);
                break;
            case 2:
                previewAni.SetTrigger("drag");
                EnableGO(btnNext.gameObject);
                EnableGO(btnPrev.gameObject);
                break;
            case 3:
                previewAni.SetTrigger("buttons");
                DisableGO(btnNext.gameObject);
                EnableGO(btnPrev.gameObject);
                break;
        }
    }
}
