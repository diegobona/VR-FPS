using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab1; // 敌人预制体
    public GameObject enemyPrefab2;
    public Transform[] spawnPoints; // 敌人生成位置
    public int enemyCount; // 生成敌人的数量

    private void OnTriggerEnter(Collider other)
    {
        // 当玩家进入区域时，生成敌人
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < enemyCount; i++)
            {
                // 在预定的生成点内生成敌人
                //Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Transform spawnPoint = spawnPoints[i];
                GameObject enemyPrefab = GetRandomEnemyPrefab();
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }




    private GameObject GetRandomEnemyPrefab()
    {
        // 随机选择敌人 Prefab
        if (Random.value < 0.5f)
        {
            return enemyPrefab1;
        }
        else
        {
            return enemyPrefab2;
        }
    }




}
