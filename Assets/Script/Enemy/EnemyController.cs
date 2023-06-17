using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController
{
    private EnemyView _enemyView;
    private ScriptableObject _enemySO;

    public EnemyController(EnemyView enemyView,ScriptableObject enemySO)
    {
        _enemyView = enemyView;
        _enemySO = enemySO;

    }
}
