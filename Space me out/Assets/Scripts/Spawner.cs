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
        planetIndex = 4;
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

    private void SetObstacleSelector()
    {
        switch (skyesEngineScript.obstacleIndex)
        {
            case 1:
            case 2:
                _speed = 7;
                spawnTime = 6;
                _minSpawnSelection = 0;
                _maxSpawnSelection = 2;
                _minObstacleSelection = 0;
                _maxObstacleSelection = 1;
                break;
            case 3:
            case 4:
                _speed = 8;
                spawnTime = 4;
                _minObstacleSelection = 1;
                _maxObstacleSelection = 2;
                break;
            case 5:
            case 6:
                _speed = 9;
                spawnTime = 4;
                _maxSpawnSelection = 4;
                _minObstacleSelection = 2;
                _maxObstacleSelection = 3;
                break;
            case 7:
                spawnTime = 2;
                _maxObstacleSelection = 4;
                break;
            case 8:
                _speed = 10;
                _maxObstacleSelection = 4;
                break;
            case 9:
                _speed = 11;
                spawnTime = 1f;
                _maxObstacleSelection = 4;
                break;
            case 10:
                _speed = 8;
                spawnTime = 0.8f;
                break;
            case 11:
                spawnTime = 4;
                _minSpawnSelection = 2;
                _maxSpawnSelection = 4;
                _minObstacleSelection = planetIndex;
                _maxObstacleSelection = planetIndex++;
                break;
            case 12:
                _speed = 12;
                spawnTime = 1f;
                _minSpawnSelection = 0;
                _maxSpawnSelection = 4;
                _minObstacleSelection = 2;
                _maxObstacleSelection = 4;
                break;
            case 13:
                spawnTime = 1.5f;
                _minSpawnSelection = 2;
                _maxSpawnSelection = 4;
                break;
            case 14:
                spawnTime = 0.8f;
                _minSpawnSelection = 0;
                _maxSpawnSelection = 4;
                break;
            case 15:
                actualTime = 0;
                break;
            case 16:
                _speed = 12;
                spawnTime = 1f;
                _minSpawnSelection = 0;
                _maxSpawnSelection = 4;
                _minObstacleSelection = 2;
                _maxObstacleSelection = 4;
                break;
            case 17:
                _speed = 13f;
                spawnTime = 0.8f;
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
