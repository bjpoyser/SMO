using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject gameControllerGO;

    public ShopSystem shopSysScript;
    public SkyesEngine skyesEngineScript;

    public int value;
    public Transform target;
    public float _speed;

    private AudioSource _pickedSound;

    private void Start()
    {
        gameControllerGO = GameObject.Find("GameController");
        skyesEngineScript = gameControllerGO.GetComponent<SkyesEngine>();
        shopSysScript = gameControllerGO.GetComponent<ShopSystem>();
        _speed = 5f;
    }

    public void Update()
    {
        if (skyesEngineScript.isGameOn)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rocket")
        {
            _pickedSound = collision.GetComponentInParent<AudioSource>();
            shopSysScript.AddCoins(value);
            shopSysScript.DisplayCoins();
            _pickedSound.Play();
            Destroy(gameObject);
        }
        if (collision.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
