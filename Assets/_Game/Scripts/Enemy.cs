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
        CheckEnemyOnCamera();

    }

    public void CheckEnemyOnCamera()
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(TF.position);
        if (viewPos.x < 1f && viewPos.x > 0f && viewPos.y < 1f && viewPos.y > 0f)
        {
            Debug.Log("Nhin thay dich");           

        }
        else
        {
            
            Debug.Log("khong nhin thay dich");
            Vector2 targetOnScreen = new Vector2();
            targetOnScreen.x = Mathf.Clamp(viewPos.x, 0.1f, 0.9f);
            targetOnScreen.y = Mathf.Clamp(viewPos.y, 0.1f, 0.9f);
            Vector3 indicatorTargetPos = Camera.main.ViewportToScreenPoint(new Vector3(targetOnScreen.x, targetOnScreen.y, 0));
            Debug.Log(indicatorTargetPos);
            float angle = Mathf.Atan2( Screen.width / 2 - targetOnScreen.x, Screen.height / 2 - targetOnScreen.y);
            Debug.Log(angle);
            indicatorPrefab.transform.position = indicatorTargetPos;
            Vector3 IndicatorRotate = new Vector3(0, 0, angle * Mathf.Rad2Deg);
            Debug.Log("goc Xoay" + IndicatorRotate);
            //indicatorPrefab.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
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
        LevelManager.Instance.enemies.Remove(this);
        LevelManager.Instance.enemyRemain -- ;
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

}
