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
                _speed = 5;
                spawnTime = 6;
                _minSpawnSelection = 0;
                _maxSpawnSelection = 2;
                //Only aircraft
                _minObstacleSelection = 0;
                _maxObstacleSelection = 1;
                break;
            case 2:
                //aircraft and helicopter
                _speed = 6;
                spawnTime = 4;
                _maxObstacleSelection = 2;
                break;
            case 3:
                _speed = 7.5f;
                spawnTime = 2;
                //helicopter and heat ballon
                _minObstacleSelection = 1;
                _maxObstacleSelection = 3;
                break;
            case 4:
                _speed = 10;
                spawnTime = 4;
                //Only Plane
                _minObstacleSelection = 3;
                _maxObstacleSelection = 4;
                break;
            case 5:
            case 6:
                _speed = 9;
                spawnTime = 3;
                _maxSpawnSelection = 4;
                //Only meteorite
                _minObstacleSelection = 4;
                _maxObstacleSelection = 5;
                break;
            case 7:
                spawnTime = 2;
                //meteorite and asteroid
                _maxObstacleSelection = 6;
                break;
            case 8:
                _speed = 10;
                break;
            case 9:
                _speed = 11;
                spawnTime = 1f;
                break;
            case 10:
                _speed = 9.5f;
                spawnTime = 0.8f;
                break;
            case 11:
                spawnTime = 3f;
                _speed = 7f;
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
                spawnTime = 0.8f;
                _speed = 10f;
                _minSpawnSelection = 0;
                _maxSpawnSelection = 4;
                //meteorite and asteroid
                _minObstacleSelection = 4;
                _maxObstacleSelection = 6;
                break;
            case 14:
                spawnTime = 8f;
                break;
            case 15:
                _speed = 12;
                spawnTime = 0.8f;
                break;
            case 18:
                _speed = 10;
                spawnTime = 1.2f;
                break;
            case 19:
                _speed = 11f;
                spawnTime = 1f;
                break;
            case 20:
                _speed = 8f;
                spawnTime = 1f;
                break;
            case 21:
                _speed = 13f;
                spawnTime = 0.8f;
                break;
            case 22:
                spawnTime = 8f;
                break;
            case 23:
                _speed = 14f;
                spawnTime = 0.7f;
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
