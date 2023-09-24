using UnityEngine;

public class Billboard : GameBehaviour
{
    public void FaceCamera()
    {
        transform.LookAt(transform.position + gameManager.Camera.MainCamera.transform.rotation * Vector3.forward,
                     gameManager.Camera.MainCamera.transform.rotation * Vector3.up);
    }
}
