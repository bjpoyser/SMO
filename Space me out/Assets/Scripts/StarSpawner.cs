using System;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public SkyesEngine skyesEngineScript;
    [SerializeField]
    private float _actualTime, _spawnTime;

    private int _starCount;

    public Transform[] destroyersArray, spawnersArray;

    public GameObject[] starsArray;

    private Star _starScript;

    [SerializeField]
    private bool _isStarTime;

    void Start()
    {
        _actualTime = 0;
    }

    void Update()
    {
        if (skyesEngineScript.isGameOn && _isStarTime)
        {
            if (_actualTime > _spawnTime)
            {
                SpawnStars();
                _actualTime = 0;
            }
            else
                _actualTime += Time.deltaTime;
        }
        SetStarFrecuency();
    }

    private void SpawnStars()
    {
        int spawnIndex = UnityEngine.Random.Range(0, spawnersArray.Length);
        int starIndex = UnityEngine.Random.Range(0, 10);

        if (starIndex >= 0 && starIndex < 7)
            starIndex = 0;
        if (starIndex >= 7 && starIndex < 9)
            starIndex = 1;
        if (starIndex == 9)
            starIndex = 2;

        _starCount++;

        GameObject star = Instantiate(starsArray[starIndex]);
        star.name = "Star_" + _starCount;
        star.transform.SetParent(spawnersArray[spawnIndex]);
        star.transform.position = spawnersArray[spawnIndex].position;

        _starScript = star.GetComponent<Star>();
        _starScript.target = destroyersArray[spawnIndex];
    }

    private void SetStarFrecuency()
    {
        switch (skyesEngineScript.obstacleIndex)
        {
            case 1:
                _isStarTime = false;
                break;
            case 6:
                _isStarTime = true;
                _spawnTime = 15f;
                break;
            case 9:
                _spawnTime = 10f;
                break;
            case 13:
                _spawnTime = 20f;
                break;
            case 14:
                _spawnTime = 1f;
                break;
            case 15:
                _spawnTime = 15f;
                break;
            case 22:
                _spawnTime = 1f;
                break;
            case 23:
                _spawnTime = 20f;
                break;
        }
    }
}
