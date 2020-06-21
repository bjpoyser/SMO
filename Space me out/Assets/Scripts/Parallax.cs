using UnityEngine;

public class Parallax : MonoBehaviour
{
    void Update()
    {
        float x = Input.acceleration.x;
        float y = Input.acceleration.y;

        GetComponent<RectTransform>().position = new Vector2(
            (x / Screen.width) * 7000,
            (y / Screen.height) * 7000
        );
    }
}
