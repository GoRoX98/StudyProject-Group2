using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Base _base;

    [Header("Enemies options")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnDelay = 5f;
    private float _spawnTimer = 0f;
    private List<GameObject> _enemies = new();
    private EnemyFabric _fabric;

    private Player _player;

    public Vector3 BasePosition => _base.transform.position;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        List<Vector3> spawnPoints = new();
        foreach (Transform tr in _spawnPoints)
        {
            spawnPoints.Add(tr.position);
        }

        _fabric = new(spawnPoints, BasePosition);
    }

    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnDelay < _spawnTimer)
        {
            Vector3 pos = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
            GameObject obj = Instantiate(_enemyPrefab, pos, new Quaternion());
            _enemies.Add(obj);
            obj.GetComponent<Enemy>().Init(BasePosition);
            _spawnTimer = 0f;
        }
    }
}
