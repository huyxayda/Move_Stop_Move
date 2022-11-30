using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 spin;
    [SerializeField] private Transform skin;


    private Transform target;
    private Vector3 dir;
    private Transform throwPoint;
    public Character cha;

    public float speed = 100f;
    public WeaponType BulletType;

    void Update()
    {
        skin.Rotate(spin * Time.deltaTime);
        if(Vector3.Distance(TF.position,cha.TF.position) > cha.attackRange + 4f)
        {
            OnDespawn();
        }
    }
    public override void OnInit()
    {
        //rb.velocity = Vector3.zero;
        //Invoke(nameof(OnDespawn), 3f);
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    //gan vi tri enemy
    public void Seek(Transform target)
    {
        this.target = target;
        dir = (this.target.position - transform.position).normalized;
        StartBullet(dir);
    }

    public void StartBullet(Vector3 direct)
    {
        rb.velocity = direct * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        //IHIt hit = other.GetComponent<IHIt>();
        IHIt hit = Cache.GetHit(other);
        if(hit != null)
        {
            hit.OnHit();
            OnDespawn();
            cha.LevelUp();
        }

        if (other.CompareTag(Constant.Tag_obstacles))
        {
            OnDespawn();
        }
    }
    
}
