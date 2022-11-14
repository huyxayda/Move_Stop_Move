using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : Character, IHIt
{
    [SerializeField] private Rigidbody rb;

    public Transform ground;
    public NavMeshAgent agent;
    public LevelManager levelManager;

    IState<Enemy> currentState;


    void Update()
    {
        
        if(GameManager.Instance.IsState(GameState.GamePlay) && currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        //random weapon cho enemy tai thoi diem bat dau
        int randonWeapon = Random.Range(0, 3);
        weapon = (WeaponType)randonWeapon;
        poolType = (PoolType)randonWeapon;
        //
        ChangeState(new IdleState());
        CreateWeapon(weapon);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        LevelManager.Instance.enemies.Remove(this);
        LevelManager.Instance.enemyRemain -- ;
        Destroy(gameObject);
    }

    public void SetDestination(Vector3 position)
    {
        //agent.enabled = true;
        ChangeAnim(Constant.Anim_run);
        agent.SetDestination(position);
        tf.LookAt(position);
    }

    public void StopMoving()
    {
        ChangeAnim(Constant.Anim_idle);
        //agent.enabled = false;
        agent.SetDestination(tf.position);
        isRunning = false;

    }

    public void ChangeState(IState<Enemy> state)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void OnHit()
    {
        ChangeState(null);
        ChangeAnim(Constant.Anim_dead);
        agent.SetDestination(tf.position);
        Invoke(nameof(OnDespawn), 2f);
    }

}
