using UnityEngine;

public class Billboard : GameBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(transform.position + gameManager.Camera.MainCamera.transform.rotation * Vector3.forward,
                             gameManager.Camera.MainCamera.transform.rotation * Vector3.up);
    }

}
