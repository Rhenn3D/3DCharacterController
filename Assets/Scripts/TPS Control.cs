using UnityEngine;
using UnityEngine.InputSystem;

public class TPSControl : MonoBehaviour
{
//Componentes
    private CharacterController _controller;
    private Animator _animator;

//Inputs
    private InputAction _moveAction;
    private Vector2 _moveInput;
    private InputAction _jumpAction;
    private InputAction _aimAction;
    private InputAction _lookAction;
    [SerializeField] private Vector2 _lookInput;
//Camara
    private Transform _mainCamera;
    [SerializeField] private float _cameraSensitivity = 10;
    [SerializeField] Transform _lookAt;
    float _xRotation;

//Movimiento

    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _jumpHeight = 2;
//Gravedad
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Vector3 _playerGravity;

//Ground Sensor
    [SerializeField] private Transform _sensor;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _sensorRadius = 2;


    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _lookAction = InputSystem.actions["Look"];
        _aimAction = InputSystem.actions["Aim"];
        _mainCamera = Camera.main.transform;
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        _lookInput = _lookAction.ReadValue<Vector2>();

        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        Gravity();
        Movement();
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            _playerGravity.y += _gravity * Time.deltaTime;
        }
        else if (IsGrounded() && _playerGravity.y < -0)
        {
            _playerGravity.y = _gravity;
            _animator.SetBool("IsJumping", false);
        }
        

        _controller.Move(_playerGravity * Time.deltaTime);
    }

    void Jump()
    {
        _animator.SetBool("IsJumping", true);
        _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        _controller.Move(_playerGravity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(_sensor.position, _sensorRadius, _groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_sensor.position, _sensorRadius);
    }

    void Movement()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0, _moveInput.y);
        float moveX = _lookInput.x * _cameraSensitivity * Time.deltaTime;
        float moveY = _lookInput.y * _cameraSensitivity * Time.deltaTime;

        _xRotation -= moveY;
        _xRotation = Mathf.Clamp(_xRotation, -89, 89);

        _animator.SetFloat("Horizontal", _moveInput.x);
        _animator.SetFloat("Vertical", _moveInput.y);

        transform.Rotate(Vector3.up, moveX);
        _lookAt.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        //_lookAt.Rotate(Vector3.right, moveY);

        if(direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _controller.Move(moveDirection * _movementSpeed * Time.deltaTime);
        }
    }

}
