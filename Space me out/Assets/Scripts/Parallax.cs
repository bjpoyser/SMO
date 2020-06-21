using UnityEngine;

public class Parallax : MonoBehaviour
{
    void Update()
    {
        float x = Input.acceleration.x;
        float y = Input.acceleration.y;

        GetComponent<RectTransform>().position = new Vector2(
            (x * 10/ Screen.width) * 10000,
            (y * 10/ Screen.height) * 10000
        );
    }
}
