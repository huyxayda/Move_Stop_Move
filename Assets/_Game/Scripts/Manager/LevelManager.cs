using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Networking.UnityWebRequest;
using Color = System.Drawing.Color;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levelPrefabs;
    private Level currentLevel;
    public Enemy enemy;
    public Player player;
    public int enemyCount;  //so luong enemy da xuat hien
    public int enemyAll;    //tong so luong enemy cua 1 level
    public int enemyRemain; //so luong enemy con lai

    public int coinPlayer;
    private int numberLevel;

    public List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        numberLevel = 1;
        LoadLevel(numberLevel);
        currentLevel.OnInit();
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
            UIManager.Instance.CloseUI<IndicatorCanVas>();
            GameManager.Instance.ChangeState(GameState.FinishGame);
        }

        
    }
    public void OnInIt()
    {
        enemyCount = 0;
        player.TF.position = currentLevel.startPoint.position;
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
        SimplePool.CollectAll();
        enemies.Clear();

    }

    public void OnRetry()
    {
        // xoa map cu, load lai level,setup lai map, mo UI main menu
        OnReset();
        LoadLevel(numberLevel);
        OnInIt();
    }

    public void OnNextLevel()
    {
        numberLevel++;
        OnReset();
        LoadLevel(numberLevel);
        OnInIt();
        UIManager.Instance.OpenUI<MainMenu>();
    }

    //ham tim diem ngau nhien cho enemy di chuyen

    public Vector3 ERandomDestination()
    {

        float xPos = Random.Range(currentLevel.cube1.position.x, currentLevel.cube2.position.x);
        float zPos = Random.Range(currentLevel.cube1.position.z, currentLevel.cube2.position.z);
        Vector3 target = new Vector3 (xPos, currentLevel.transform.position.y + 1.75f, zPos);
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(target, out hit, 4.0f, NavMesh.AllAreas))
        {
            ERandomDestination();
        }
        else
        {
            target.x = hit.position.x;
            target.z = hit.position.z;
        }
        return target;

    }
    
    //ham tim diem ngau nhien de spawn enemy
    public void RandomPointSpawn()
    {
        float xPos = Random.Range(currentLevel.cube1.position.x, currentLevel.cube2.position.x);
        float zPos = Random.Range(currentLevel.cube1.position.z, currentLevel.cube2.position.z);
        Vector3 point = new Vector3(xPos, 2.5f, zPos);
        NavMeshHit hit;
        if (Vector3.Distance(point, player.TF.position) < player.attackRange || !NavMesh.SamplePosition(point, out hit, 4.0f, NavMesh.AllAreas))
        {
            RandomPointSpawn();
        }
        else
        {
            Enemy newEnemy;
            newEnemy = SimplePool.Spawn<Enemy>(enemy, point, Quaternion.identity);
            newEnemy.ResetEnemy();
            enemies.Add(newEnemy);
            enemyCount++;
        }
    }

    //ham dung cho shop
     public void EquipWeapon(WeaponType weapon)
    {
        player.ChangeWeapon(weapon);
        player.weapon = weapon;
    }
    public void EquipBullet(PoolType bulletType)
    {
        player.WeaponpoolType = bulletType;
    }

    public void EquipPant(int index)
    {
        player.ChangePant(index);
    }

    public void EquipHair(int index)
    {
        player.ChangeHair(index);
    }

    public void PayCoin(int cost)
    {
        coinPlayer = coinPlayer - cost;
    }

    public void currentCoinPlayer(int coinPlayer)
    {
        this.coinPlayer = coinPlayer;
    }
}
