using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField][Range(1, 10)] private float _speed = 2f;

    [Header("Gun")]
    [SerializeField] private Transform _gun;
    [SerializeField] private GameObject _bulletPrefab;

    private PlayerInput _input;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _contact;
    private InputAction _touch;

    private Gameplay _gameplay;
    private float _timer = 0f;
    [Header("Move Settings")]
    [SerializeField][Range(0, 1)] private float _speedIncrase = 1;
    [SerializeField] private float _maxSpeed = 5;
    private Vector2 _movement = new Vector2(0,0);
    private float _currentSpeed = 0;

    private void Awake()
    {
        _gameplay = new Gameplay(GetComponent<Rigidbody>());
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions["Move"];
        _jumpAction = _input.actions["Jump"];
        _contact = _input.actions["PrimaryContact"];
        _touch = _input.actions["MoveTouch"];
    }

    private void OnEnable()
    {
        _moveAction.performed += Move;
        _jumpAction.performed += Jump;
    }


    private void OnDisable()
    {
        _moveAction.performed -= Move;
        _jumpAction.performed -= Jump;
    }

    void Update()
    {
        if (_input.currentControlScheme == "Mobile")
        {
            if (_contact.inProgress)
            {
                TouchState touch = _touch.ReadValue<TouchState>();
                Vector2 startPos = touch.startPosition;
                Vector2 currentPos = touch.position;

                _movement = currentPos - startPos;
                _movement.Normalize();
            }
            else
                _movement = Vector2.zero;
        }
        else
        {
            _movement = _moveAction.ReadValue<Vector2>();
        }

        if (Input.GetMouseButtonDown(0))
            Instantiate(_bulletPrefab, _gun.position, _bulletPrefab.transform.rotation);
        _timer += Time.deltaTime;

        if (_movement.y != 0 || _movement.x != 0)
        {
            if (_currentSpeed < _maxSpeed)
                _currentSpeed += _speedIncrase * Time.deltaTime;
        }
        else
        {
            _currentSpeed = 0;
        }

        if (_currentSpeed > 0)
        {
            transform.position += (transform.forward * _movement.y + transform.right * _movement.x) * _currentSpeed * Time.deltaTime;
        }

        //Old movement
        /*
        if (_timer > 3f && Input.GetKeyDown(KeyCode.Space))
        {
            _timer = 0f;
            _gameplay.DoSomething();
        }

        //var directionV = Input.GetAxis("Vertical");
        var directionH = Input.GetAxis("Horizontal");

        transform.position += transform.right * directionH * _speed * Time.deltaTime;*/
    }

    //New Input System
    private void Move(InputAction.CallbackContext context)
    {
        //_movement = context.ReadValue<Vector2>();
        //print($"Vector {_movement}");
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (_timer < 3)
            return;

        _timer = 0;
        _gameplay.DoSomething();
    }
}
