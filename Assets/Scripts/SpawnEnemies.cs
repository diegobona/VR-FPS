using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab1; // ����Ԥ����
    public GameObject enemyPrefab2;
    public Transform[] spawnPoints; // ��������λ��
    public int enemyCount; // ���ɵ��˵�����

    private void OnTriggerEnter(Collider other)
    {
        // ����ҽ�������ʱ�����ɵ���
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < enemyCount; i++)
            {
                // ��Ԥ�������ɵ������ɵ���
                //Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Transform spawnPoint = spawnPoints[i];
                GameObject enemyPrefab = GetRandomEnemyPrefab();
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }




    private GameObject GetRandomEnemyPrefab()
    {
        // ���ѡ����� Prefab
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
