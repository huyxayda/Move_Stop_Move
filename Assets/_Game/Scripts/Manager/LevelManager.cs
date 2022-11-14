using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levelPrefabs;
    private Level currentLevel;
    public Enemy enemy;
    public Player player;
    public int enemyCount;  //so luong enemy da xuat hien
    public int enemyAll;    //tong so luong enemy cua 1 level
    public int enemyRemain; //so luong enemy con lai


    public List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        LoadLevel(0);
        OnInIt();
        UIManager.Instance.OpenUI<MainMenu>();
    }
    private void Update()
    {
        if (enemies.Count < 10 && enemyCount < enemyAll)
        {
            RandomPointSpawn();
        }

        if(enemyRemain == 0 && GameManager.Instance.IsState(GameState.GamePlay))
        {
            LevelManager.Instance.OnFinishGame();
            UIManager.Instance.OpenUI<Win>();
            UIManager.Instance.CloseUI<GamePlay>();
            GameManager.Instance.ChangeState(GameState.FinishGame);
        }

        //test navmesh.sampleposition
        //Vector3 point;
        //if (RandomPoint(transform.position, range, out point))
        //{
        //    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        //}
    }
    public void OnInIt()
    {
        enemyCount = 0;
        player.tf.position = currentLevel.startPoint.position;
        player.OnInit();
        //enemyAll = 20;
        enemyRemain = enemyAll;
        while (enemies.Count < 10 && enemyCount < enemyAll )
        {
            RandomPointSpawn();
        }

    }

    public void LoadLevel(int level)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        if(level < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[level]);
            currentLevel.OnInit();
        }
        else
        {
            //TODO: level vuot qua limit
        }
    }
    

    public void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
        //khi loading level xong thi chua choi game ngay, phai an play
    }

    public void OnFinishGame()
    {
        //ket thuc game(ko can biet thang thua), show cac UI
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ChangeState(null);
            enemies[i].StopMoving();
        }
    }

    public void OnReset()
    {
        //destroy map cu, enemy => load level moi
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i].gameObject);
        }
        enemies.Clear();

    }

    public void OnRetry()
    {
        // xoa map cu, load lai level,setup lai map, mo UI main menu
        OnReset();
        LoadLevel(0);
        OnInIt();
        //UIManager.Instance.OpenUI<MainMenu>();
    }

    public void OnNextLevel()
    {
        OnReset();
        LoadLevel(0);
        OnInIt();
        UIManager.Instance.OpenUI<MainMenu>();
    }

    //ham tim diem ngau nhien cho enemy di chuyen
    public Vector3 ERandomDestination()
    {

        float xPos = Random.Range(currentLevel.cube1.position.x, currentLevel.cube2.position.x);
        float zPos = Random.Range(currentLevel.cube1.position.z, currentLevel.cube2.position.z);

        return new Vector3(xPos, 1.6f, zPos);

    }
    //ham tim diem ngau nhien de spawn enemy
    public void RandomPointSpawn()
    {
        float xPos = Random.Range(currentLevel.cube1.position.x, currentLevel.cube2.position.x);
        float zPos = Random.Range(currentLevel.cube1.position.z, currentLevel.cube2.position.z);
        if (Vector3.Distance(new Vector3(xPos, 2.5f, zPos), player.tf.position) < player.attackRange)
        {
            RandomPointSpawn();
        }
        else
        {
            Enemy newEnemy;
            newEnemy = Instantiate(enemy, new Vector3(xPos, 2.5f, zPos), Quaternion.identity);
            enemies.Add(newEnemy);
            enemyCount++;
        }
    }

    //public float range = 100f;
    //bool RandomPoint(Vector3 center, float range, out Vector3 result)
    //{
    //    for (int i = 0; i < 30; i++)
    //    {
    //        Vector3 randomPoint = center + Random.insideUnitSphere * range;
    //        NavMeshHit hit;
    //        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
    //        {
    //            result = hit.position;
    //            return true;
    //        }
    //    }
    //    result = Vector3.zero;
    //    return false;
    //}
}
