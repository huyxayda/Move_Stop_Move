using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 spin;
    [SerializeField] private Transform tf;
    [SerializeField] private Transform skin;


    private Transform target;
    private Vector3 dir;
    private Transform throwPoint;
    public Character cha;

    public float speed = 100f;
    public WeaponType BulletType;

    
    void Start()
    {
        //if (BulletType == WeaponType.Knife)
        //{
        //    KnifeBullet();
        //}

        //if (BulletType == WeaponType.Hammer)
        //{
        //    HammerBullet();
        //}

        //if (BulletType == WeaponType.Boomerang)
        //{
        //    BoomerangBullet();
        //}
    }

    void Update()
    {
        skin.Rotate(spin * Time.deltaTime);
    }
    public override void OnInit()
    {
        rb.velocity = Vector3.zero;
        Invoke(nameof(OnDespawn), 3f);
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    //gan vi tri enemy
    public void Seek(Transform target)
    {
        this.target = target;
        dir = target.position - transform.position;
        StartBullet();
    }

    public void StartBullet()
    {
        rb.AddForce(dir * speed);
        //rb.velocity = dir * speed * Time.deltaTime;
    }

    //public void KnifeBullet()
    //{
    //    StartBullet();
    //}

    //public void HammerBullet()
    //{
    //    StartBullet();
    //}

    //public void BoomerangBullet()
    //{
    //    //if(Vector3.Distance(tf.position, target.position) < 0.1f)
    //    //{
    //    //    target = throwPoint;
    //    //}
    //    StartBullet();
    //}
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
    }
}
