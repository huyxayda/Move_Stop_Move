using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Enemy>
{
    
    Vector3 moveTarget;
    //LevelManager levelManager;
    public void OnEnter(Enemy enemy)
    {
        moveTarget = LevelManager.Instance.ERandomDestination();
        enemy.SetDestination(moveTarget);
        enemy.isRunning = true;
    }

    public void OnExecute(Enemy enemy)
    {
        //neu enemy xd dc muc tieu ban thi se dung lai de ban
        if(enemy.FindTarget() != null)
        {
            enemy.ChangeState(new AttackState());
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position, moveTarget) < 0.1f)
            {
                enemy.ChangeState(new IdleState());
            }
           
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
