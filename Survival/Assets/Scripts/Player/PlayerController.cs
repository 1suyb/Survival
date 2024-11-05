using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController
{

    [Header("Move")]
    private int _damage = 5;
    private Vector2 _curMovementInput;
    public LayerMask groundLayerMask;
    [SerializeField] private Camera _camera;

    private Rigidbody _rigidbody;
    private Animator _animator;

    [Header("Look")]
    public Transform cameraContainer;
    private float _minXLook = -85.0f;
    private float _maxXLook = 85.0f;
    private float _camCurXRot;
    private float _lookSensitivity = 0.2f;
    private Vector2 _mouseDelta;
    public bool canLook = true;
    private bool isDash = false;

    public Action inventory;
    public Action buildinventory;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            Look();
        }
    }

    public override void Move()
    {
        if (isDash)
        {
            PlayerManager.Instance.Player.condition.UseStamina(0.1f);
        }
        Vector3 dir = transform.forward * _curMovementInput.y + transform.right * _curMovementInput.x;
        dir *= PlayerManager.Instance.Player.data.Speed();
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
    }

    public override void Look()
    {
        _camCurXRot += _mouseDelta.y * _lookSensitivity;
        _camCurXRot = Mathf.Clamp(_camCurXRot, _minXLook, _maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-_camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, _mouseDelta.x * _lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _animator.SetBool("Move", true);
            _curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _animator.SetBool("Move", false);
            _curMovementInput = Vector2.zero;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PlayerManager.Instance.Player.data.ChangeSpeed(PlayerManager.Instance.Player.data.Speed() * 2);
            isDash = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            PlayerManager.Instance.Player.data.ChangeSpeed(PlayerManager.Instance.Player.data.Speed() / 2);
            isDash = false;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded() && PlayerManager.Instance.Player.condition.StaminaCheck() > 10.0f)
        {
            PlayerManager.Instance.Player.condition.UseStamina(10.0f);
            _rigidbody.AddForce(Vector2.up * PlayerManager.Instance.Player.data.JumpPower(), ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    public void OnBuildInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            buildinventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _animator.SetTrigger("Attack");
        }
    }
    public override void Attack()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, PlayerManager.Instance.Player.data.AttackDistance()))
        {
            if (hit.collider.TryGetComponent(out IDamagable monster))
            {
                monster.TakeDamage((int)PlayerManager.Instance.Player.data.Damage());
            }
        }
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
