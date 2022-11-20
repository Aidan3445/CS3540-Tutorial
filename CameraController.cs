using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera firstPersonCm;
    
    [SerializeField] private CinemachineVirtualCamera topDownCm;
    
    [SerializeField] private CinemachineFreeLook thirdPersonCm;
    
    [SerializeField] private new Camera camera;
    
    public enum CameraModes
    {
        First, Third, TopDown
    }

    public Transform GetCameraTransform()
    {
        return camera.transform;
    }

    public void SwitchCamera(CameraModes mode)
    {
        switch (mode)
        {
            case CameraModes.First:
                firstPersonCm.Priority = 1;
                thirdPersonCm.Priority = 0;
                topDownCm.Priority = 0;
                camera.orthographic = false;
                break;
            case CameraModes.Third:
                firstPersonCm.Priority = 0;
                thirdPersonCm.Priority = 1;
                topDownCm.Priority = 0;
                camera.orthographic = false;
                break;
            case CameraModes.TopDown:
                firstPersonCm.Priority = 0;
                thirdPersonCm.Priority = 0;
                topDownCm.Priority = 1;
                camera.orthographic = true;
                break;
        }
    }
}
