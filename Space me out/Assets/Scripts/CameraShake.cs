using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator cameraAni;
    
    public void ShakeCamera()
    {
        cameraAni.SetTrigger("shake");
    }
}
