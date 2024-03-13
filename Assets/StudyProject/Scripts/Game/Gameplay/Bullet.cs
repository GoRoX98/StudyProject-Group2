using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _force = 10f;
    [SerializeField] private float _lifeTime = 10f;
    private float _timer = 0f;
    private bool _isDestroy = false;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * _force, ForceMode.Impulse);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _lifeTime && !_isDestroy)
        {
            Destroy(gameObject);
            _isDestroy = true;
        }
    }
}
