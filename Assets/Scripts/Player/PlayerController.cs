using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event Action OnUseItem;

    [Header("Move")]
    private float _defaultMoveSpeed = 5.0f;
    private readonly float _defaultJumpPower = 10f;
    public float _speed;
    private Vector2 _inputMovement;
    public LayerMask Ground;

    [Header("Look")]
    [SerializeField] private Transform _camera;
    private readonly float _minXLook = -85f;
    private readonly float _maxXLook = 85f;
    private float _camXRot;
    [Range(0f, 5f)] public float Sensitive = 0.1f;
    private Vector2 _mouseDeltaValue;
    private bool isDuraitem = false;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        OnUseItem += UseItem;
        StartCoroutine(DuraItemUse());
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * _inputMovement.y + transform.right * _inputMovement.x;
        dir *= (_defaultMoveSpeed + _speed);
        dir.y = _rb.velocity.y;

        _rb.velocity = dir;
    }

    private void Look()
    {
        _camXRot += _mouseDeltaValue.y * Sensitive;
        _camXRot = Mathf.Clamp(_camXRot, _minXLook, _maxXLook);
        _camera.localEulerAngles = new Vector3(-_camXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, _mouseDeltaValue.x * Sensitive, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _inputMovement = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _inputMovement = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _mouseDeltaValue = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded())
        {
            _rb.AddForce(Vector2.up * _defaultJumpPower, ForceMode.Impulse);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && CharacterManager.Instance.Player.Item != null)
        {
            OnUseItem?.Invoke();
        }
    }

    private bool isGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1f, Ground)) return true;
        return false;
    }

    private void UseItem()
    {
        if(CharacterManager.Instance.Player.Item != null)
        {
            ItemData item = CharacterManager.Instance.Player.Item.GetComponent<Item>().ItemData;
            for(int i = 0; i < item.ItemUsuable.Length; i ++)
            {
                if (item.ItemType == ItemType.Temp)
                {
                    switch (item.ItemUsuable[i].Type) //현재 순간 적용되는 아이템이 체력 밖에 없음 && 현재 게이지가 체력 밖에 없음
                    {
                        default : CharacterManager.Instance.Player.Condition.CurValue += item.ItemUsuable[i].Value;
                            break;
                    }
                }
                else if (item.ItemType == ItemType.Dura)
                {
                    isDuraitem = true;
                }
            }
        }
    }

    private IEnumerator DuraItemUse() //현재 지속 적용되는 아이템이 속도 밖에 없음
    {
        while (true)
        {
            if (isDuraitem == true)
            {
                ItemUsuable[] item = CharacterManager.Instance.Player.Item.GetComponent<Item>().ItemData.ItemUsuable;
                for (int i = 0; i < item.Length; i++)
                {
                    switch (item[i].Type)
                    {
                        default:
                            _speed = item[i].Value;
                            yield return new WaitForSeconds(item[i].Time);
                            isDuraitem = false;
                            break;
                    }
                }
            }
            else
            {
                _speed = 0f;
                yield return null;
            }
        }
    }
}
