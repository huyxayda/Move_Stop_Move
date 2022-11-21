using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Enemy : Character, IHIt
{
    [SerializeField] private Rigidbody rb;

    public Transform ground;
    public Collider collider;
    public NavMeshAgent agent;
    public LevelManager levelManager;
    public Indicator indicatorPrefab;
    //public Camera cam;

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
        ChangeState(new IdleState());
        CreateWeapon(weapon);
    }

    public void ResetEnemy()
    {
        //reset lai enemy khi duoc lay ra khoi pool
        collider.enabled = true;
        ChangeState(new IdleState());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        //SimplePool.Despawn(this);
        MinusEnemy();
    }

    public void SetDestination(Vector3 position)
    {
        ChangeAnim(Constant.Anim_run);
        agent.SetDestination(position);

    }

    public void StopMoving()
    {
        ChangeAnim(Constant.Anim_idle);
        agent.SetDestination(TF.position);
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
        agent.SetDestination(TF.position);
        Invoke(nameof(OnDespawn), 2f);
        collider.enabled = false;
        
    }

    public void MinusEnemy()
    {
        LevelManager.Instance.enemies.Remove(this);
        LevelManager.Instance.enemyRemain--;
    }
}
