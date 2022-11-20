using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Tooltip("Player Speed")]
    private float speed;

    [SerializeField] [Tooltip("Gravity Strength")]
    private float gravity;
    
    private Vector3 _velocity;

    private CharacterController _characterController;
    
    private Animator _playerAnimator;
    
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    [SerializeField] private CameraController cameraController;

    [SerializeField] private CameraController.CameraModes cameraMode;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<Animator>();
        cameraController.SwitchCamera(cameraMode);
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerTransform = transform;
        Transform cameraTransform = cameraController.GetCameraTransform();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement;
        
        if (cameraMode is CameraController.CameraModes.TopDown)
        {
            movement = ((Vector3.forward * z) + (Vector3.right * x)).normalized;
            if (movement.magnitude > 0)
            {
                playerTransform.rotation = Quaternion.AngleAxis(
                    Vector3.SignedAngle(Vector3.forward, movement, Vector3.up), Vector3.up);
            }
            else
            {
                float angle = Vector3.SignedAngle(Vector3.forward, playerTransform.forward, Vector3.up);
                angle = (int)(angle % 360 / 90) * 90;
                playerTransform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            }
        }
        else
        {
            movement = (playerTransform.right * x + playerTransform.forward * z).normalized;
            playerTransform.rotation = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up);
        }

        _playerAnimator.SetBool(IsWalking, movement.magnitude > 0);
        _characterController.Move(movement * (speed * Time.deltaTime));
        
        
        // Gravity
        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        switch (cameraMode)
        {
            case CameraController.CameraModes.First:
                cameraMode = CameraController.CameraModes.Third;
                break;
            case CameraController.CameraModes.Third:
                cameraMode = CameraController.CameraModes.TopDown;
                break;
            case CameraController.CameraModes.TopDown:
                cameraMode = CameraController.CameraModes.First;
                break;
        }
        
        cameraController.SwitchCamera(cameraMode);
    }
}
