using UnityEngine;
using Unity.Cinemachine;
using Fusion;

public class FusionPlayerSetup : NetworkBehaviour
{
    public void SetupCamera()
    {
        if (Object.HasInputAuthority)
        {
            //CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            FusionCameraFollow cameraFollow = FindFirstObjectByType<FusionCameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.AssignCamera(transform);
            }
        }
    }
}