using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*
// Player.cs
//*
// Class behaviour
//*
// @category   3rd Person Survival Shooter Pro
// @tutorial   GameDevHQ
// @author     Dave González
// @copyright  2021 Dave González
// @version    CVS: 0.1
// @link       website
//*

public class Player : MonoBehaviour
{
    [Header("Controller Settings")]
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _gravity = 20.0f;
    [SerializeField] private float _jumpHeight = 8.0f;

    [Header("Camera Settings")]
    [SerializeField] private float _cameraSensitivity = 1;

    private CharacterController _characterController;
    private Camera _playerCamera;

    private Vector3 _direction;
    private Vector3 _velocity;


    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked; // Locks cursor to game screen

        // NULL check character controller
        if (_characterController == null)
            Debug.LogError("Character Controller is NULL");

        // NULL check camera controller
        if (_playerCamera == null)
            Debug.LogError("Main Camera is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CameraController();

        // Unlock cursor with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }

    // Moves the player
        private void Movement()
    {
        // Calculate movement while grounded
        if (_characterController.isGrounded) // Check if we're grounded
        {
            float horizontalMovement = Input.GetAxis("Horizontal"); // Horizontal inputs (a, d, leftarrow, rightarrow)
            float verticalMovement = Input.GetAxis("Vertical"); // Veritical inputs (w, s, uparrow, downarrow)

            _direction = new Vector3(horizontalMovement, 0, verticalMovement); // Direction to move
            _velocity = _direction * _speed; // Velocity is the direction and speed we travel

            // Assign velocity to jump
            if (Input.GetKeyDown(KeyCode.Space))
                _velocity.y = _jumpHeight;
            
        }

        // Apply gravity
        _velocity.y -= _gravity * Time.deltaTime; // Subtract gravity from our yVelocity 
        _velocity = transform.TransformDirection(_velocity); // Assign the cached value of our _velocity
        _characterController.Move(_velocity * Time.deltaTime); // Move the controller x meters per second
    }

    // Controls the third person camera
        private void CameraController()
    {
        float mouseX = Input.GetAxis("Mouse X"); // Get mouse movement on the x
        float mouseY = Input.GetAxis("Mouse Y"); // Get mouse movement on the y

        // Look left and right
        Vector3 currentRotation = transform.localEulerAngles; // Store current rotation
        currentRotation.y += mouseX * _cameraSensitivity; // Add our mouseX movement to the Y axis
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up); // Rotate along the y axis by movement amount

        // Look up and down
        Vector3 currentCameraRotation = _playerCamera.gameObject.transform.localEulerAngles; // Store current rotation
        currentCameraRotation.x -= mouseY * _cameraSensitivity; // Add our mouseY movement to the X axis
        _playerCamera.transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(currentCameraRotation.x, 0, 25), Vector3.right); // Rotate along the x axis by movement amount

    }
}
