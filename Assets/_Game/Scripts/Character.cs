using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : GameUnit
{
    [SerializeField] private Animator anim;
    [SerializeField] protected Transform weaponHolder;
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected Transform hairHolder;
    [SerializeField] protected HairData hairData;
    [SerializeField] protected ColorData colorData;
    [SerializeField] protected PantData pantData;

    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private SkinnedMeshRenderer pantMesh;

    public Transform skin;
    private List<Transform>enemyPos = new List<Transform>();   
    //public Transform TF;
    public bool isAttack = false;
    public bool isRunning = false;
    private float time;

    public WeaponType weapon;
    public PoolType WeaponpoolType;
    public float attackRange;

    private string currentAnimName;
    protected bool IsDeath = false;
    public int level = 0;
    public Transform bulletSpawnPoint;


    void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        IsDeath = false;
        
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    // cau truc thay doi animation
    protected void ChangeAnim(string animName)
    {

        if (currentAnimName != animName)
        {
            //Debug.Log("Anim" + animName + this);
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public Transform FindTarget()
    {
        //tim vi tri enemy
        Transform nearestEnemy;
        Collider[] colliders = Physics.OverlapSphere(TF.position, attackRange);
        float minDistance = Mathf.Infinity;

        nearestEnemy = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(TF.position, colliders[i].transform.position);
            if (colliders[i].gameObject.tag == Constant.Tag_character &&
                distanceToEnemy > 0.5f)  //tranh bullet target vao chinh chu
            {
                if(distanceToEnemy < minDistance)
                {
                    minDistance = distanceToEnemy;
                    nearestEnemy = colliders[i].transform;
                }
            }
            else
            {
                continue;
            }
        }
        return nearestEnemy;
    }

    public void Shoot(Transform targetPos)
    {
        if(targetPos != null)
        {
            //xoay nhan vat
            Vector3 dir = targetPos.position - TF.position;
            Quaternion lookRatation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRatation.eulerAngles;
            gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            ChangeAnim(Constant.Anim_attack);
            time += Time.deltaTime;
            if (time > 0.3f)
            {
                isAttack = true;
                Bullet bullet = CreateBullet(WeaponpoolType);
                if (bullet != null)
                {
                    bullet.OnInit();
                    bullet.Seek(targetPos);
                }
                //xac dinh bullet duoc ban tu character nao
                bullet.cha = this;
                time = 0f;
            }
        }

        //sau khi attack thi target se dc reset lai
        targetPos = null;
    }

    public void ResetShoot()
    {
        isAttack = false;
        //time = 0;
    }

    //ham khoi tao weapon cho nhan vat
    public Weapon CreateWeapon(WeaponType weapon)
    {
        return Instantiate(weaponData.GetWeapon(weapon), weaponHolder);
    }

    //ham khoi tao bullet
    public Bullet CreateBullet(PoolType poolType)
    {
        return SimplePool.Spawn<Bullet>(poolType, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        //return Instantiate(weaponData.GetBullet(weapon), bulletSpawnPoint);
    }

    public GameObject CreatHair(int randomHair)
    {
        return Instantiate(hairData.hair[randomHair], hairHolder);
    }

    public void CreatPant(int randomPant)
    {
        pantMesh.material = pantData.pants[randomPant];
    }
    public void ChangeColor()
    {
        int randomPant = Random.Range(0, hairData.hair.Count);
        Instantiate(hairData.hair[randomPant], hairHolder);
    }

    public virtual void LevelUp()
    {
        level++;
        attackRange++;

        //Debug.Log("Level " + level + " " + this);
    }

    
}
