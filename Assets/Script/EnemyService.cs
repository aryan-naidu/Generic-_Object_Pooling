using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] private ScriptableObject _enemySO;
    [SerializeField] private EnemyView _enemyView;
    private EnemyController _enemyController;

    public EnemyService()
    {
        _enemyController = new EnemyController(_enemyView, _enemySO);
    }

}
