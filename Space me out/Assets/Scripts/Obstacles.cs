using System;
using System.Collections;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private const string vibrationPref = "vibration";

    public GameObject gameControllerGO;
    public GameOver gameOverScript;

    public SkyesEngine skyesEngineScript;

    public GameObject particleSystemGO;
    public ParticleSystem particles;

    public GameObject rocketGO;
    public SpriteRenderer rocketSprite;

    public GameObject cameraGO;
    public CameraShake cameraShakeScript;

    public bool isVibrationOn;

    public float speed;

    public Transform target;

    private void Start()
    {
        gameControllerGO = GameObject.Find("GameController");
        gameOverScript = gameControllerGO.GetComponent<GameOver>();

        skyesEngineScript = gameControllerGO.GetComponent<SkyesEngine>();

        particleSystemGO = GameObject.Find("RocketParticles");
        particles = particleSystemGO.GetComponent<ParticleSystem>();

        rocketGO = GameObject.Find("Rocket");
        rocketSprite = rocketGO.GetComponent<SpriteRenderer>();

        cameraShakeScript = gameControllerGO.GetComponent<CameraShake>();

        try
        {
            isVibrationOn = bool.Parse(PlayerPrefs.GetString(vibrationPref));
        }
        catch (FormatException)
        {
            PlayerPrefs.SetString(vibrationPref, "true");
            isVibrationOn = true;
        }
    }

    public void Update()
    {
        if(skyesEngineScript.isGameOn)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rocket")
        {
            StartCoroutine(BreakRocket());
        }
        if (collision.gameObject.tag == "Destroyer")
        {
            StartCoroutine(DestroyObstacle());
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private IEnumerator BreakRocket()
    {
        particles.Play();
        cameraShakeScript.ShakeCamera();

        Destroy(rocketGO.transform.GetComponent<BoxCollider2D>());

        rocketSprite.enabled = false;
        skyesEngineScript.isGameOn = false;

        if (isVibrationOn)
            Handheld.Vibrate();

        yield return new WaitForSeconds(2f);
        gameOverScript.ShowGameOver();
    }

    private IEnumerator DestroyObstacle()
    {
        yield return new WaitForSeconds(2.1f);
        Destroy(this.gameObject);
    }
}
