using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb2D;
    private PlayerControls _playerControls;
    private Vector2 _currentMovementValue;
    InputAction.CallbackContext _currentContext;
    private float _speed = 3f;
    private bool _onStairs = false;
    private const int _jumpForce = 5;

    private void Awake()
    {
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
        _playerControls = new PlayerControls();
        _playerControls.Player.Move.performed += _ => Move(_);
        _playerControls.Player.Jump.performed += _ => Jump(_);
    }

    private void OnEnable()
    {
        _playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Player.Disable();
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _rb2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            
        }
        
    }

    private void Move(InputAction.CallbackContext context)
    {
        _currentMovementValue = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_onStairs == false)
        {
            _currentMovementValue.y = 0;
            _rb2D.gravityScale = 1;
        }
        else
        {
            _rb2D.gravityScale = 0;
        }

        if (_playerControls.Player.Move.phase != InputActionPhase.Waiting)
        {
            
            _rb2D.MovePosition(_rb2D.position + _currentMovementValue * _speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            _onStairs = true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Flag"))
        {
            _onStairs = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            _onStairs = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Flag"))
        {
            _onStairs = false;
        }
    }

}