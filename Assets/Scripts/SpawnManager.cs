using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyRangedPrefab;
    [SerializeField] private Text enemyText;
    [SerializeField] private Text roundText;

    [SerializeField] private GameObject RoundStartBox;
    [SerializeField] private Text RoundStartText;

    public int enemyCount;
    public int roundNumber = 1;
    public int enemyNumber = 0;
    private float roundCounter = 5f;

    public Vector3 spawnPoint2;
    public List<Transform> spawnPointLocs;

    private bool countDown = false;
    private float startTimer = 5f;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        enemyText.text = "Enemies left: " + enemyCount.ToString();
        roundText.text = "Round: " + roundNumber.ToString();
        RoundStartBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        enemyText.text = "Enemies left: " + enemyCount.ToString();

        StartRound();

    }

    private void StartRound()
    {
        if (enemyCount == 0)
        {
            countDown = true;

            if (countDown)
            {
                RoundStartBox.SetActive(true);
                RoundStartText.text = "Round " + (roundNumber + 1).ToString() + " is about to begin!";

                startTimer -= Time.deltaTime;

                if (startTimer <= 0)
                {
                    RoundStartBox.SetActive(false);
                    countDown = false;
                    startTimer = 5f;
                }
            }

            roundCounter -= Time.deltaTime;

            if (roundCounter <= 0)
            {
                roundNumber++;
                enemyNumber++;
                if (enemyNumber >= 50)
                {
                    enemyNumber = 50;
                }
                SpawnEnemyWave(enemyNumber);
                roundText.text = "Round: " + roundNumber.ToString();

                if (roundNumber >= 5)
                {
                    SpawnRangedWave(enemyNumber / 2);
                }

                roundCounter = 5;
            }
        }
    }

    private Vector3 GenerateSpawnPoint(int currentZone)
    {
        float spawnPosX = Random.Range(-4f, 4f);
        float spawnPosZ = Random.Range(-4f, 4f);

        if (currentZone == 1)
        {
            return new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
        }
        else if (currentZone == 2)
        {
            int spawnRandomizer = Random.Range(1, 3);

            switch (spawnRandomizer)
            {
                case 1:
                    spawnPoint2 = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                    break;
                case 2:
                    spawnPoint2 = new Vector3(spawnPointLocs[1].position.x + spawnPosX, 0.25f, spawnPointLocs[1].position.z + spawnPosZ);
                    break;
            }
            
            return spawnPoint2;
        }
        else
        {
            return new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
        }

    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPoint(PlayerController.instance.playerZone), enemyPrefab.transform.rotation);
        }
    }

    void SpawnRangedWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyRangedPrefab, GenerateSpawnPoint(PlayerController.instance.playerZone), enemyRangedPrefab.transform.rotation);
        }
    }
}
