using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Enemy>
{
    float timer;
    public void OnEnter(Enemy enemy)
    {
        enemy.SetDestination(enemy.transform.position);
        enemy.isRunning = false;
        enemy.ResetShoot();
        timer = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        Transform target = enemy.FindTarget();
        if (target != null && !enemy.isAttack && !enemy.isRunning)
        {
            enemy.Shoot(target);
        }
        if (timer > 1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
       
    }

    public void OnExit(Enemy enemy)
    {

    }
}
