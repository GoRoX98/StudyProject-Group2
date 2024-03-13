using UnityEngine;
using UnityEngine.InputSystem;

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

    private Gameplay _gameplay;
    private float _timer = 0f;

    private void Awake()
    {
        _gameplay = new Gameplay(GetComponent<Rigidbody>());
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions["Move"];
        _jumpAction = _input.actions["Jump"];
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
        if (Input.GetMouseButtonDown(0))
            Instantiate(_bulletPrefab, _gun.position, _bulletPrefab.transform.rotation);
        _timer += Time.deltaTime;
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
        Vector2 move = context.ReadValue<Vector2>();

        if (move == Vector2.up)
            transform.position += transform.forward * _speed;
        if (move == Vector2.down)
            transform.position += -transform.forward * _speed;
        if (move == Vector2.left)
            transform.position += -transform.right * _speed;
        if (move == Vector2.right)
            transform.position += transform.right * _speed;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (_timer < 3)
            return;

        _timer = 0;
        _gameplay.DoSomething();
    }
}
