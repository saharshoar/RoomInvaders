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

    [SerializeField] private GameObject RoundStartBox;
    [SerializeField] private Text RoundStartText;

    public int enemyCount;
    public int roundNumber = 1;
    public int enemyNumber = 0;
    private float roundCounter = 5f;


    // Start is called before the first frame update
    void Start()
    {
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

    IEnumerator WaitForRound()
    {
        RoundStartBox.SetActive(true);
        RoundStartText.text = "Round " + (roundNumber + 1).ToString() + " is about to begin!";
        
        yield return new WaitForSeconds(5);

        RoundStartBox.SetActive(false);
    }

    private void StartRound()
    {
        if (enemyCount == 0)
        {
            StartCoroutine(WaitForRound());
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
