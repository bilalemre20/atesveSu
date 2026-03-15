using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Zıplama ve Fizik")]
    public float jumpHeight = 1.5f; // Zıplama yüksekliği
    public float gravity = -9.81f;  // Yerçekimi kuvveti

    [Header("Animasyon")]
    public string runBool = "isRunning";
    public string jumpTrigger = "Jump"; 

    [Header("Kamera Referansı")]
    public Transform mainCamera; // Kamerayı buraya alıyoruz

    // Gerekli bileşenler
    private InputSystem_Actions _inputActions;
    private CharacterController _controller;
    private Animator _animator;

    // Değişkenler
    private Vector2 _inputVector;
    private Vector3 _moveDirection;
    private Vector3 _velocity; 
    private bool _isGrounded;

    void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        
        // Eğer Unity arayüzünde kamera atanmamışsa oyun başlayınca otomatik bulur
        if (mainCamera == null && Camera.main != null)
        {
            mainCamera = Camera.main.transform;
        }
    }

    void OnEnable() { _inputActions.Enable(); }
    void OnDisable() { _inputActions.Disable(); }

    void Update()
    {
        _isGrounded = _controller.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; 
        }

        ReadInput();
        MoveCharacter();
        RotateCharacter();
        HandleJumpAndGravity(); 
        AnimateCharacter();
    }

    void ReadInput()
    {
        _inputVector = _inputActions.Player.Move.ReadValue<Vector2>();
    }

    void MoveCharacter()
    {
        if (mainCamera == null) return;

        // 1. Kameranın baktığı ileri ve sağ yönleri al
        Vector3 camForward = mainCamera.forward;
        Vector3 camRight = mainCamera.right;

        // 2. Y eksenlerini sıfırla (Karakterin yeri delmesini veya havaya uçmasını engeller)
        camForward.y = 0f;
        camRight.y = 0f;

        // 3. Vektörleri eşitle (Normalize)
        camForward.Normalize();
        camRight.Normalize();

        // 4. Tuş girdilerini kameranın dünya yönleriyle çarp
        _moveDirection = (camForward * _inputVector.y) + (camRight * _inputVector.x);

        // Çapraz gidişlerdeki ani hızlanmaları ve dönüşlerdeki yalpalamaları engelle
        if (_moveDirection.magnitude > 1f)
        {
            _moveDirection.Normalize();
        }

        _controller.Move(_moveDirection * moveSpeed * Time.deltaTime); 
    }

    void RotateCharacter()
    {
        // Karakterin titremesini ve yön sapmasını engellemek için eşik değeri
        if (_moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void HandleJumpAndGravity()
    {
        if (_inputActions.Player.Jump.triggered && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (_animator != null) _animator.SetTrigger(jumpTrigger);
        }

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void AnimateCharacter()
    {
        bool isMoving = _moveDirection.sqrMagnitude > 0.01f;
        if (_animator != null)
        {
            _animator.SetBool(runBool, isMoving);
        }
    }
}