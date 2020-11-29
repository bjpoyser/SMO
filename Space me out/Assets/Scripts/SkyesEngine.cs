using UnityEngine;
using UnityEngine.UI;

public class SkyesEngine : MonoBehaviour
{

    #region Global Variables

    private const string catNamePref = "CatRocket";

    public GameObject prevSky, nextSky;
    public GameObject[] skyesContainerArray;

    private float _speed;

    public bool isGameOn;

    public float _skySize;
    private int _skyCount;

    public Vector3 limitScreenSize;
    public GameObject camGO;
    public Camera camComp;

    public float altitude;

    public int minAltitude, obstacleIndex;

    public Text altitudeText;

    public Animator gameAni;

    #endregion

    void Start()
    {
        //Screen
        camGO = GameObject.Find("MainCamera");
        camComp = camGO.GetComponent<Camera>();
        ScreenMeasure();

        //Values
        altitude = 0;
        _speed = 5;
        isGameOn = true;

        //Sky
        CreateSky();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }

    void Update()
    {
        //Sky mover
        if (isGameOn)
        {
            transform.Translate(Vector2.down * _speed * Time.deltaTime);
            altitude += 100 * Time.deltaTime;
            altitudeText.text = (int)altitude + " KM";

            //Sky destroyer
            if (prevSky.transform.position.y < limitScreenSize.y)
                DestroySky();
        }
    }

    private void DestroySky()
    {
        Destroy(prevSky.gameObject);
        CreateSky();
    }

    private void CreateSky()
    {
        _skyCount++;
        SkyAltitude();
        GameObject sky = Instantiate(skyesContainerArray[minAltitude]);
        sky.SetActive(true);
        sky.name = "Sky_" + _skyCount;
        sky.transform.parent = transform;
        PlaceSky();
    }

    private void SkyAltitude()
    {
        SkySelector();
        ObstacleSelector();
    }

    private void PlaceSky()
    {
        prevSky = GameObject.Find("Sky_"+(_skyCount-1));
        nextSky = GameObject.Find("Sky_"+_skyCount);
        _skySize = prevSky.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        nextSky.transform.position = new Vector3(prevSky.transform.position.x, prevSky.transform.position.y + _skySize - .1f, 0);
    }

    private void ScreenMeasure()
    {
        limitScreenSize = new Vector3(0, camComp.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 8f, 0);
    }

    private void ObstacleSelector()
    {
        if (altitude < 1)
            obstacleIndex = 1;
        if (altitude > 1000)
            obstacleIndex = 2;
        if (altitude > 3000)
            obstacleIndex = 3;
        if (altitude > 5000)
            obstacleIndex = 4;
        if (altitude > 7000)
            obstacleIndex = 5;
        if (altitude > 8000)
            obstacleIndex = 6;
        if (altitude > 9000)
            obstacleIndex = 7;
        if (altitude > 10000)
            obstacleIndex = 8;
        if (altitude > 12000)
            obstacleIndex = 9;
        if (altitude > 13000)
            obstacleIndex = 10;
        if (altitude > 14000)
            obstacleIndex = 11;
        if (altitude > 16300)
            obstacleIndex = 12;
        if (altitude > 16500)
            obstacleIndex = 13;
        if (altitude > 17000)
            obstacleIndex = 14;
        if (altitude > 18000)
            obstacleIndex = 15;
        if (altitude > 20000)
            obstacleIndex = 16;
        if (altitude > 22000)
            obstacleIndex = 17;
        if (altitude > 23000)
            obstacleIndex = 18;
        if (altitude > 25000)
            obstacleIndex = 19;
        if (altitude > 27000)
            obstacleIndex = 20;
        if (altitude > 29000)
            obstacleIndex = 21;
        if (altitude > 30000)
            obstacleIndex = 22;
        if (altitude > 31000 && altitude < 31200)
        {
            if(PlayerPrefs.GetInt(catNamePref) == 0)
            {
                gameAni.SetTrigger("unlocked");
                //PlayerPrefs.SetInt(catNamePref, 1);
            }
        }
        if (altitude > 32000)
            obstacleIndex = 23;
    }

    private void SkySelector()
    {
        //Sky 1: constant
        if (altitude < 1)
            minAltitude = 0;

        //Sky 2: transition
        if (altitude > 2500)
            minAltitude = 1;

        //Sky 3: constant
        if (altitude > 2800)
            minAltitude = 2;

        //Sky 4: transition
        if (altitude > 4900)
            minAltitude = 3;

        //Sky 5: constant
        if (altitude > 5000)
            minAltitude = 4;

        //Sky 6: transition
        if (altitude > 7000)
            minAltitude = 5;

        //Sky 7: constant
        if (altitude > 7300)
            minAltitude = 6;

        //Sky 8: transition
        if (altitude > 9000)
            minAltitude = 7;

        //Sky 9: constant
        if (altitude > 9300)
            minAltitude = Random.Range(8, 10);

    }
}
