using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject gameControllerGO;
    public SkyesEngine skyesEngineScript;

    public float spawnTime, actualTime;

    public GameObject[] obstaclesArray;
    public Transform[] spawnersArray;
    public Transform[] destroyersArray;

    private int planetIndex;

    private int _maxSpawnSelection;
    public int _minObstacleSelection, _maxObstacleSelection;

    private int _obstacleCount;

    private Obstacles obstaclesScript;

    private float _speed;
    private int _minSpawnSelection;

    private const float _ultraLowFQ = 10f, _lowFQ = 5.5f, _mediumFQ = 3f, _highFQ = 1.5f, _madFQ = 0.8f;
    private const float _reallySlow = 5f, _slow = 6f, _normal = 7.5f, _fast = 9f, _reallyFast = 10f, _imposible = 11f;

    void Start()
    {
        gameControllerGO = GameObject.Find("GameController");
        skyesEngineScript = gameControllerGO.GetComponent<SkyesEngine>();
        _obstacleCount = 0;
        actualTime = 0;
        planetIndex = 6;
    }

    void Update()
    {
        if (skyesEngineScript.isGameOn)
        {
            if (actualTime > spawnTime)
            {
                SpawnObstacles();
                actualTime = 0;
            }
            else
                actualTime += Time.deltaTime;
        }
    }

    private void SpawnObstacles()
    {
        try
        {
            int randomSpawnerPosition = Random.Range(_minSpawnSelection, _maxSpawnSelection);
            int randomObstacleSelector = Random.Range(_minObstacleSelection, _maxObstacleSelection);

            _obstacleCount++;
            SetObstacleSelector();

            GameObject obstacle = Instantiate(obstaclesArray[randomObstacleSelector]);
            obstacle.name = "Obstacle_" + _obstacleCount;
            obstacle.transform.SetParent(spawnersArray[randomSpawnerPosition]);
            obstacle.transform.position = spawnersArray[randomSpawnerPosition].position;

            obstaclesScript = obstacle.GetComponent<Obstacles>();
            obstaclesScript.SetSpeed(_speed);
            obstaclesScript.target = destroyersArray[randomSpawnerPosition];

            ChangePositionObstacle(randomSpawnerPosition, obstacle);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }


    /*
     0 = aircraft
     1 = helicopter
     2 = heat ballon
     3 = plane
     4 = meteorite
     5 = asteroid
    */

    private void SetObstacleSelector()
    {
        switch (skyesEngineScript.obstacleIndex)
        {
            case 1:
                _speed = _reallySlow;
                spawnTime = _ultraLowFQ;
                _minSpawnSelection = 0;
                _maxSpawnSelection = 2;
                //Only aircraft
                _minObstacleSelection = 0;
                _maxObstacleSelection = 1;
                break;
            case 2:
                //aircraft and helicopter
                spawnTime = _lowFQ;
                _maxObstacleSelection = 2;
                break;
            case 3:
                _speed = _slow;
                spawnTime = _mediumFQ;
                //helicopter and heat ballon
                _minObstacleSelection = 1;
                _maxObstacleSelection = 3;
                break;
            case 4:
                _speed = _normal;
                spawnTime = _lowFQ;
                //Only Plane
                _minObstacleSelection = 3;
                _maxObstacleSelection = 4;
                break;
            case 5:
            case 6:
                _speed = _fast;
                spawnTime = _mediumFQ;
                _maxSpawnSelection = 4;
                //Only meteorite
                _minObstacleSelection = 4;
                _maxObstacleSelection = 5;
                break;
            case 7:
                _speed = _normal;
                spawnTime = _highFQ;
                //meteorite and asteroid
                _maxObstacleSelection = 6;
                break;
            case 8:
                _speed = _fast;
                break;
            case 9:
                _speed = _reallyFast;
                spawnTime = _madFQ;
                break;
            case 10:
                _speed = _normal;
                break;
            case 11:
                spawnTime = _mediumFQ;
                _speed = _slow;
                _minSpawnSelection = 2;
                _maxSpawnSelection = 4;
                //Planets
                _minObstacleSelection = planetIndex;
                _maxObstacleSelection = planetIndex++;
                break;
            case 12:
                _minObstacleSelection = 4;
                _maxObstacleSelection = 6;
                break;
            case 13:
                spawnTime = _madFQ;
                _speed = _fast;
                _minSpawnSelection = 0;
                _maxSpawnSelection = 4;
                //meteorite and asteroid
                _minObstacleSelection = 4;
                _maxObstacleSelection = 6;
                break;
            case 14:
                spawnTime = _ultraLowFQ;
                break;
            case 15:
                _speed = _reallyFast;
                spawnTime = _madFQ;
                break;
            case 18:
                _speed = _fast;
                spawnTime = _highFQ;
                break;
            case 19:
                _speed = _reallyFast;
                spawnTime = _madFQ;
                break;
            case 20:
                _speed = _normal;
                spawnTime = _highFQ;
                break;
            case 21:
                _speed = _imposible;
                spawnTime = _madFQ;
                break;
            case 22:
                spawnTime = _ultraLowFQ;
                break;
            case 23:
                _speed = _imposible + 2;
                spawnTime = _madFQ;
                break;
        }
    }

    private void ChangePositionObstacle(int spawn, GameObject obstacle)
    {
        Transform tr = obstacle.transform;

        switch (spawn)
        {
            case 1:
                tr.localScale = new Vector3(-tr.localScale.x, tr.localScale.y, tr.localScale.z); break;
            case 2:
            case 3:
                tr.rotation = Quaternion.Euler(new Vector3(0, 0, -60)); break;
        }

        if (obstacle.tag == "Planet")
        {
            if (spawn == 2)
            {
                tr.localPosition = new Vector3(-2, tr.localRotation.y + 12, tr.position.z);
                obstaclesScript.target = destroyersArray[1];
            }
            if (spawn == 3)
            {
                tr.localPosition = new Vector3(6, tr.localRotation.y + 12, tr.position.z);
                obstaclesScript.target = destroyersArray[0];
            }
            tr.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

}
