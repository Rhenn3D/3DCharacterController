using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private Transform _sensor;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _sensorRadius = 2;

    [Header("Movimiento")]
    [SerializeField] private float _playerSpeed = 6;
    [SerializeField] private float _jumpForce = 6;
    [SerializeField] private float _smoothTime = 0.2f;
    private float _turnSmoothVelocity;

    [Header("Inputs")]
    private InputAction _moveInput;
    [SerializeField] private Vector2 _moveAction;
    private InputAction _jumpInput;
    [SerializeField] private Vector2 _jumpAction;
    [SerializeField] private Animator _animator;

    [Header("Gravedad")]
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Vector3 _playerGravity;

    [Header("Otros")]
    private CharacterController characterController;
    private Transform _mainCamera;

    [Header("Muerte / Respawn")]
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private float _deathDelay = 1.0f;
    private bool _isDead = false;

    [Header("Fade Out")]
    [SerializeField] private GameObject xerathAroObject;
    [SerializeField] private float fadeOutDuration = 1.5f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        _moveInput = InputSystem.actions["Move"];
        _jumpInput = InputSystem.actions["Jump"];
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main.transform;

        if (xerathAroObject == null)
            xerathAroObject = GameObject.Find("XerathAro");
    }

    void Update()
    {
        if (_isDead) return;

        _moveAction = _moveInput.ReadValue<Vector2>();

        if (_jumpInput.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        Movement();
        Gravity();
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            _playerGravity.y += _gravity * Time.deltaTime;
        }
        else if (IsGrounded() && _playerGravity.y < 0)
        {
            _playerGravity.y = _gravity;
            _animator.SetBool("IsJumping", false);
        }

        characterController.Move(_playerGravity * Time.deltaTime);
    }

    void Jump()
    {
        _animator.SetBool("IsJumping", true);
        _playerGravity.y = Mathf.Sqrt(_jumpForce * -2 * _gravity);
        characterController.Move(_playerGravity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(_sensor.position, _sensorRadius, _groundLayer);
    }

    void Movement()
    {
        Vector3 direction = new Vector3(_moveAction.x, 0, _moveAction.y);
        _animator.SetFloat("Vertical", _moveAction.y);
        _animator.SetFloat("Horizontal", _moveAction.x);

        if (direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _smoothTime);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            characterController.Move(moveDirection.normalized * _playerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death trigger"))
        {
            Die();
        }
    }

    private void Die()
{
    if (_isDead) return;

    _isDead = true;
    _playerGravity = Vector3.zero;

    
    _animator.SetFloat("Vertical", 0f);
    _animator.SetFloat("Horizontal", 0f);
    _animator.SetBool("IsJumping", false);

    
    _animator.SetTrigger("Die");

    
    if (xerathAroObject != null)
        StartCoroutine(FadeOutObject(xerathAroObject, fadeOutDuration));

    
    StartCoroutine(RespawnAfterDelay());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (_sensor != null)
            Gizmos.DrawWireSphere(_sensor.position, _sensorRadius);
    }


    //A parir de aquí, usé IA para hacer una forma de respawn. También hice un fade out al objeto xerath aro al morir, pero no hice el fade in al respawnear.
    //A parir de aquí, usé IA para hacer una forma de respawn. También hice un fade out al objeto xerath aro al morir, pero no hice el fade in al respawnear.
    //A parir de aquí, usé IA para hacer una forma de respawn. También hice un fade out al objeto xerath aro al morir, pero no hice el fade in al respawnear.
    //A parir de aquí, usé IA para hacer una forma de respawn. También hice un fade out al objeto xerath aro al morir, pero no hice el fade in al respawnear.
    //A parir de aquí, usé IA para hacer una forma de respawn. También hice un fade out al objeto xerath aro al morir, pero no hice el fade in al respawnear.
    //A parir de aquí, usé IA para hacer una forma de respawn. También hice un fade out al objeto xerath aro al morir, pero no hice el fade in al respawnear.
    //A parir de aquí, usé IA para hacer una forma de respawn. También hice un fade out al objeto xerath aro al morir, pero no hice el fade in al respawnear.
    

    //Era simplemente para completar un poco la entrega y aprender cosas para el proyecto final. Es un extra y no me importa si no se me sube nota por eso, es normal.
    //Era simplemente para completar un poco la entrega y aprender cosas para el proyecto final. Es un extra y no me importa si no se me sube nota por eso, es normal.
    //Era simplemente para completar un poco la entrega y aprender cosas para el proyecto final. Es un extra y no me importa si no se me sube nota por eso, es normal.
    //Era simplemente para completar un poco la entrega y aprender cosas para el proyecto final. Es un extra y no me importa si no se me sube nota por eso, es normal.
    //Era simplemente para completar un poco la entrega y aprender cosas para el proyecto final. Es un extra y no me importa si no se me sube nota por eso, es normal.
    //Era simplemente para completar un poco la entrega y aprender cosas para el proyecto final. Es un extra y no me importa si no se me sube nota por eso, es normal.
    //Era simplemente para completar un poco la entrega y aprender cosas para el proyecto final. Es un extra y no me importa si no se me sube nota por eso, es normal.

    private IEnumerator RespawnAfterDelay()
    {
    yield return new WaitForSeconds(_deathDelay);

    if (_respawnPoint != null)
    {
        characterController.enabled = false;
        transform.position = _respawnPoint.position;
        characterController.enabled = true;
    }

    
    _animator.ResetTrigger("Die");
    _animator.Rebind();
    _animator.Update(0f);
    _animator.SetFloat("Vertical", 0f);
    _animator.SetFloat("Horizontal", 0f);
    _animator.SetBool("IsJumping", false);


    _isDead = false;
    }


    /*private IEnumerator FadeOutObject(GameObject target, float duration)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer == null) yield break;

        Material mat = renderer.material;
        Color originalColor = mat.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        target.SetActive(false);
    }*/

    
}