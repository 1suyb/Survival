using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController
{

    [Header("Move")]
    private float _moveSpeed = 5.0f;
    private float _jumpPower = 80.0f;
    private float _attackDistance = 2.0f;
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
        Vector3 dir = transform.forward * _curMovementInput.y + transform.right * _curMovementInput.x;
        dir *= _moveSpeed;
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
            _moveSpeed *= 2;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _moveSpeed /= 2;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log(IsGrounded());
        if (context.phase == InputActionPhase.Started && IsGrounded() && PlayerManager.Instance.Player.condition.StaminaCheck() > 10.0f)
        {
            PlayerManager.Instance.Player.condition.UseStamina();
            _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode.Impulse);
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

        if (Physics.Raycast(ray, out hit, _attackDistance))
        {
            if (hit.collider.TryGetComponent(out MonsterController monster))
            {
                monster.TakeDamage((int)_damage);
            }
        }
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

	public override void TakeDamage(int damage)
	{
        PlayerManager.Instance.Player.condition.TakePhysicalDamage(damage);
	}
}
