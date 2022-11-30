using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IHIt
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Transform shootingRange;
    public LevelManager levelManager;
    public Weapon currentWeapon;
    public GameObject currentHair;
    private Transform target;

    public void Start()
    {
        //OnInit();
        currentWeapon = CreateWeapon(weapon);

    }

    private void Update()
    {
        //dieu khien nhan vat
        if (!IsDeath)
        {
            if (GameManager.Instance.IsState(GameState.GamePlay) && Input.GetMouseButton(0))
            {
                //tranh xung dot giua truc y
                rb.velocity = JoystickControl.direct * speed + rb.velocity.y * Vector3.up;
                isRunning = true;
                TF.forward = JoystickControl.direct;
                ChangeAnim(Constant.Anim_run);
            }

            if (Input.GetMouseButtonUp(0))
            {
                ChangeAnim(Constant.Anim_idle);
                rb.velocity = Vector3.zero;
                isRunning = false;
                ResetShoot();
                target = FindTarget();
                Debug.Log(FindTarget());
            }

            //shoot
            if (target != null && !isAttack && !isRunning)
            {
                Shoot(target);
            }
        }       

    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim(Constant.Anim_idle);
        level = 0;
        attackRange = 6f;
        shootingRange.localScale = new Vector3(0.37f, 0.37f, 0.37f) * attackRange;
        target = null;
    }

    public void OnHit()
    {
        //khi player bi ban se gameover
        ChangeAnim(Constant.Anim_dead);
        IsDeath = true;
        //rb.velocity = Vector3.zero;
        LevelManager.Instance.OnFinishGame();
        UIManager.Instance.OpenUI<Loose>();
        UIManager.Instance.CloseUI<GamePlay>();
        GameManager.Instance.ChangeState(GameState.FinishGame);
        //Debug.Log("trung dan");
    }

    public override void LevelUp()
    {
        base.LevelUp();
        //tang attack range sau khi giet ddc enemy
        shootingRange.localScale = shootingRange.localScale / (attackRange - 1) * attackRange;
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = CreateWeapon(weaponType);
    }

    public void ChangePant(int index)
    {
        CreatPant(index);
    }

    public void ChangeHair(int index)
    {
        if(currentHair != null)
        {
            Destroy(currentHair.gameObject);
        }
        currentHair = CreatHair(index);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}
