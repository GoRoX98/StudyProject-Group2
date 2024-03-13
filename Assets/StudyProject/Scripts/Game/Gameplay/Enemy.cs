using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const int _maxHealth = 100;

    private int _health;
    [SerializeField] private float _speed = 3f;

    public void Init(Vector3 basePos)
    {
        _health = _maxHealth;
        transform.LookAt(basePos);
    }

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
            _health -= 50;

        if (_health <= 0)
            Destroy(gameObject);
    }

}
