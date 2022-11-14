using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    //public Transform cube1, cube2;
    //public Enemy enemy;
    //public Player player;
    //public int enemyCount;
    //public int enemyAll;
    //public int enemyRemain;


    //public List<Enemy> enemies = new List<Enemy>() ;

    //void Start()
    //{
    //    OnInIt();
    //}
    //private void Update()
    //{
    //    if(enemies.Count < 10 && enemyCount <= enemyAll)
    //    {
    //        RandomPointSpawn();
    //    }
    //}
    //public void OnInIt()
    //{
    //    enemyAll = 20;
    //    enemyRemain = enemyAll;
    //    while (enemies.Count < 10)
    //    {
    //        RandomPointSpawn();
    //    }

    //}

    //public void RandomPointSpawn()
    //{
    //    float xPos = Random.Range(cube1.transform.position.x, cube2.transform.position.x);
    //    float zPos = Random.Range(cube1.transform.position.z, cube2.transform.position.z);
    //    if (Vector3.Distance(new Vector3(xPos, 2.5f, zPos), player.tf.position) < player.attackRange)
    //    {
    //        RandomPointSpawn();
    //    }
    //    else
    //    {
    //        Enemy newEnemy;
    //        newEnemy = Instantiate(enemy, new Vector3(xPos, 2.5f, zPos), Quaternion.identity);
    //        enemies.Add(newEnemy);
    //        enemyCount++;
    //    }
    //}

    
}
