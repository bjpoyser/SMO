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

    private void CreateSky()
    {
        _skyCount++;
        Skyaltitude();
        GameObject sky = Instantiate(skyesContainerArray[minAltitude]);
        sky.SetActive(true);
        sky.name = "Sky_" + _skyCount;
        sky.transform.parent = transform;
        PlaceSky();
    }

    private void Skyaltitude()
    {
        if (altitude < 1)
        {
            minAltitude = 1;
            obstacleIndex = 1;
        }
        else if (altitude < 1000)
        {
            minAltitude = 2;
            obstacleIndex = 2;
        }
        else if (altitude < 1200)
        {
            minAltitude = 3;
            obstacleIndex = 3;
        }
        else if (altitude < 2000)
        {
            minAltitude = 4;
            obstacleIndex = 4;
        }
        else if (altitude < 2200)
        {
            minAltitude = 5;
            obstacleIndex = 5;
        }
        else if (altitude < 3000)
        {
            minAltitude = 6;
            obstacleIndex = 6;
        }
        else if (altitude < 3200)
        {
            minAltitude = 7;
            obstacleIndex = 7;
        }
        else if (altitude < 4000)
        {
            minAltitude = Random.Range(8, 10);
            obstacleIndex = Random.Range(8, 10);
        }
        else if (altitude < 5000)
        {
            obstacleIndex = 10;
        }
        else if (altitude < 7800)
        {
            obstacleIndex = 11;
        }
        else if (altitude < 10000)
        {
            obstacleIndex = 12;
        }
        else if (altitude < 12000)
        {
            obstacleIndex = 13;
        }
        else if (altitude < 13000)
        {
            obstacleIndex = 14;
        }
        else if (altitude < 15000)
        {
            obstacleIndex = 15;
        }
        else if (altitude < 16000)
        {
            obstacleIndex = 16;
        }
        else if (altitude < 28000)
        {
            obstacleIndex = 17;
        }

        if (altitude > 30000 && altitude < 30200)
        {
            PlayerPrefs.SetInt(catNamePref, 1);
            gameAni.SetTrigger("unlocked");
        }
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

    private void DestroySky()
    {
        Destroy(prevSky.gameObject);
        CreateSky();
    }

}
