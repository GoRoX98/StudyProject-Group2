using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _health = 5;
    public event Action GameOver;

    void Update()
    {
        if (_health <= 0)
        {
            print("GameOver");
            GameOver?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        print("OnTrigger");
        if (obj.GetComponent<Enemy>())
        {
            _health -= 1;
            Destroy(obj.gameObject);
        }
    }
}
