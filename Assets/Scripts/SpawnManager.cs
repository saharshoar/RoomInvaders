using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyRangedPrefab;
    [SerializeField] private Text enemyText;
    [SerializeField] private Text roundText;

    public int enemyCount;
    public int roundNumber = 1;


    // Start is called before the first frame update
    void Start()
    {
        enemyText.text = "Enemies left: " + enemyCount.ToString();
        roundText.text = "Round: " + roundNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        enemyText.text = "Enemies left: " + enemyCount.ToString();
        

        if (enemyCount == 0)
        {
            roundNumber++;
            SpawnEnemyWave(roundNumber);
            roundText.text = "Round: " + roundNumber.ToString();

            if (roundNumber >= 5)
            {
                SpawnRangedWave(roundNumber / 2);
            }
        }
    }

    private Vector3 GenerateSpawnPoint()
    {
        float spawnPosX = Random.Range(-8f, 8f);
        float spawnPosZ = Random.Range(-8f, 8f);

        return new Vector3(spawnPosX, 0.25f, spawnPosZ);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPoint(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnRangedWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyRangedPrefab, GenerateSpawnPoint(), enemyRangedPrefab.transform.rotation);
        }
    }
}
