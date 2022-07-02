using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject spawnPoint;
    public EnemyList spawnList;
    private  int enemyCount;
    void Awake()
    {
        spawnList = GameManager.instance.enemyList;
        //GameManager.instance.player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        //spawnPoint = GameObject.FindGameObjectWithTag("EnemySpawner");
    }
    private void Start()
    {

        foreach (EnemyController enemyController in spawnList.enemyList)
        {
            enemyCount++;
        }
        SpawnEnemy(SelectEnemy(enemyCount));
    }

    public EnemyController SelectEnemy(int enemyCount)
    {
        int rollForEnemy= Random.Range(0,enemyCount);
        EnemyController selectedEnemy = spawnList.enemyList[rollForEnemy];
        Debug.LogWarning($"EnemyCount: {enemyCount} \nSelected Enemy:{selectedEnemy.name}\n" +
            $"RollForEnemyNumber: {rollForEnemy}");
        return selectedEnemy;
    }
    public void SpawnEnemy(EnemyController enemyToSpawn)
    {
        enemyToSpawn.homePosition=spawnPoint.transform.position;
        Instantiate(enemyToSpawn, spawnPoint.transform);
    }

}
