using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState <Enemy>
{
    float timer;
    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;                          
        randomTime = Random.Range(2f, 3f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.FindTarget() != null)
        {
            enemy.ChangeState(new AttackState());
        }
        else if (timer > randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
