using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Hammer, Knife, Boomerang }

[CreateAssetMenu( menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] List<Weapon> weaponPrefabs;
    [SerializeField] List<Bullet> bulletPrefabs;

    public Weapon GetWeapon(WeaponType weaponType)
    {
        return weaponPrefabs[(int)weaponType];
    }

    public Bullet GetBullet(WeaponType weaponType)
    {
        return bulletPrefabs[(int)weaponType];
    }
}
