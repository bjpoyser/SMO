using UnityEngine;

public class RocketController : MonoBehaviour
{
    #region Global Variables
    private const string controlPref = "control";
    private const string rocketPref = "rocket";

    public GameObject rocketGO;
    public GameObject[] rocketArray;

    private RocketProps _rocketProps;

    public SkyesEngine skyesEngineScript;

    private bool _isPressed;

    public int controlTypeIndex;
    private int _directionButton;

    public ControlSettings controlScript;

    public Camera mainCamera;
    #endregion

    #region Metodos Unity

    void Awake()
    {
        int index = PlayerPrefs.GetInt(rocketPref);

        rocketGO = Instantiate(rocketArray[index]);
        rocketGO.transform.SetParent(transform);

        controlTypeIndex = PlayerPrefs.GetInt(controlPref);
        _rocketProps = rocketGO.GetComponent<RocketProps>();

        rocketGO.name = "Rocket";
    }

    void Start()
    {
        if (controlTypeIndex == 1 || controlTypeIndex == 3)
            _rocketProps.turnAngle = -8f;
        else
            _rocketProps.turnAngle = -20f;

        controlScript.DisableControllers();

        if (controlTypeIndex == 1)
            controlScript.EnablePanelController();
        else if (controlTypeIndex == 3)
            controlScript.EnableButtonController();

        if (controlTypeIndex == 2)
        {
            _rocketProps.target = new Vector3(0, 0, 0);
            MoveRocket(0, 0);
        }
    }

    void Update()
    {
        if (skyesEngineScript.isGameOn)
            GetControlType();
    }

    #endregion

    #region Movimiento

    //Obtener el control
    public void GetControlType()
    {
        switch (controlTypeIndex)
        {
            case 0: GyroscopeControl(); break;

            case 1:
            case 3:
                if (_isPressed) 
                    MoveRocket(_directionButton, 5f);
                else
                    MoveRocket(0, 0); 
                break;

            case 2: FingerFollowingControl(); break;
        }
    }

    //Movimiento del cohete
    private void MoveRocket(float direction, float speed)
    {
        //Movimiento por Paneles
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        float turnZ = direction * _rocketProps.turnAngle;

        //Rotacion de la nave
        rocketGO.transform.rotation = Quaternion.Euler(0, 0, turnZ);
    }
    #endregion

    #region Tipos de Controles

    //Movimiento por giroscopio del celular
    public void GyroscopeControl()
    {
        MoveRocket(Input.acceleration.x, 15f);
        //MoveRocket(Input.GetAxis("Horizontal"), 7f);
    }

    //Movimiento por Paneles
    #region Paneles

    public void OnPointerDown(int direction)
    {
        _directionButton = direction;
        _isPressed = true;
    }

    public void OnPointerUp()
    {
        _isPressed = false;
    }

    #endregion

    //Movimiento por Seguimiento del dedo
    public void FingerFollowingControl()
    {
        _rocketProps.target = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        MoveRocket(_rocketProps.target.x*0.5f, 10f);
    }


    public void SetAngle(float angle)
    {
        _rocketProps.turnAngle = angle;
    }
    #endregion
}
