using System.Collections.Generic;
using UnityEngine;

public class EnemyFabric
{
    private readonly List<Vector3> _spawnPoints = new();
    private readonly Vector3 _basePos;
    
    public EnemyFabric(List<Vector3> spawnPoints, Vector3 basePose)
    {
        _spawnPoints = spawnPoints;
        _basePos = basePose;
    }
}
