using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyRangedPrefab;
    [SerializeField] private GameObject enemyThirdPrefab;
    [SerializeField] private Text enemyText;
    [SerializeField] private Text roundText;

    [SerializeField] private GameObject RoundStartBox;
    [SerializeField] private Text RoundStartText;

    public int enemyCount;
    public int roundNumber = 1;
    public int enemyNumber = 0;
    private float roundCounter = 5f;

    public Vector3 spawnPoint;
    public List<Transform> spawnPointLocs;
    public List<Interactable> doors;

    private bool countDown = false;
    private float startTimer = 5f;

    private bool door1Open = false;
    private bool door2Open = false;
    private bool door3Open = false;
    private bool door4Open = false;
    private bool door5Open = false;
    private bool door6Open = false;
    private bool door7Open = false;
    private bool door8Open = false;


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
        enemyCount += FindObjectsOfType<EnemyThirdController>().Length;
        enemyText.text = "Enemies left: " + enemyCount.ToString();

        StartRound();

        DoorStatus();

    }

    private void DoorStatus()
    {
        if (doors[0] && doors[0].moveDoor)
            door1Open = true;
        if (doors[1] && doors[1].moveDoor)
            door2Open = true;
        if (doors[2] && doors[2].moveDoor)
            door3Open = true;
        if (doors[3] && doors[3].moveDoor)
            door4Open = true;
        if (doors[4] && doors[4].moveDoor)
            door5Open = true;
        if (doors[5] && doors[5].moveDoor)
            door6Open = true;
        if (doors[6] && doors[6].moveDoor)
            door7Open = true;
        if (doors[7] && doors[7].moveDoor)
            door8Open = true;
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
                if (roundNumber <= 5)
                    enemyNumber += 2;
                else if (roundNumber > 5 && roundNumber < 10)
                    enemyNumber += 3;
                else if (roundNumber >= 10 && roundNumber < 20)
                    enemyNumber += 4;
                else if (roundNumber >= 20)
                    enemyNumber += 5;

                if (enemyNumber >= 75)
                {
                    enemyNumber = 75;
                }
                SpawnEnemyWave(enemyNumber);
                SpawnThirdEnemyWave(enemyNumber/2);
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
        float spawnPosX = Random.Range(-2f, 2f);
        float spawnPosZ = Random.Range(-2f, 2f);
        int spawnRandomizer;

        if (currentZone == 1)
        {

            if (door1Open && door5Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 5);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[1].position.x + spawnPosX, 0.25f, spawnPointLocs[1].position.z + spawnPosZ);
                        break;
                    case 3:
                        spawnPoint = new Vector3(spawnPointLocs[4].position.x + spawnPosX, 0.25f, spawnPointLocs[4].position.z + spawnPosZ);
                        break;
                    case 4:
                        spawnPoint = new Vector3(spawnPointLocs[5].position.x + spawnPosX, 0.25f, spawnPointLocs[5].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door1Open && door5Open && !PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 4);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[1].position.x + spawnPosX, 0.25f, spawnPointLocs[1].position.z + spawnPosZ);
                        break;
                    case 3:
                        spawnPoint = new Vector3(spawnPointLocs[5].position.x + spawnPosX, 0.25f, spawnPointLocs[5].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door1Open && !door5Open && !PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[1].position.x + spawnPosX, 0.25f, spawnPointLocs[1].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door5Open && !door1Open && !PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[5].position.x + spawnPosX, 0.25f, spawnPointLocs[5].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door1Open && !door5Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 4);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[1].position.x + spawnPosX, 0.25f, spawnPointLocs[1].position.z + spawnPosZ);
                        break;
                    case 3:
                        spawnPoint = new Vector3(spawnPointLocs[4].position.x + spawnPosX, 0.25f, spawnPointLocs[4].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door5Open && !door1Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 4);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[4].position.x + spawnPosX, 0.25f, spawnPointLocs[4].position.z + spawnPosZ);
                        break;
                    case 3:
                        spawnPoint = new Vector3(spawnPointLocs[5].position.x + spawnPosX, 0.25f, spawnPointLocs[5].position.z + spawnPosZ);
                        break;
                }
            }
            else
            {
                spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
            }
            
            return spawnPoint;

        }
        else if (currentZone == 2)
        {

            if (door1Open && door2Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 4);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[2].position.x + spawnPosX, 0.25f, spawnPointLocs[2].position.z + spawnPosZ);
                        break;
                    case 3:
                        spawnPoint = new Vector3(spawnPointLocs[8].position.x + spawnPosX, 0.25f, spawnPointLocs[8].position.z + spawnPosZ);
                        break;
                }
            }

            else if (door1Open && door2Open && !PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[2].position.x + spawnPosX, 0.25f, spawnPointLocs[2].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door1Open && !door2Open)
            {
                spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
            }
            else if (door2Open && !door1Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[2].position.x + spawnPosX, 0.25f, spawnPointLocs[2].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[8].position.x + spawnPosX, 0.25f, spawnPointLocs[8].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door2Open && !door1Open && !PowerSwitch.instance.powerOn)
            {
                spawnPoint = new Vector3(spawnPointLocs[2].position.x + spawnPosX, 0.25f, spawnPointLocs[2].position.z + spawnPosZ);
            }
            else
            {
                spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
            }
            
            return spawnPoint;
        }
        else if (currentZone == 3)
        {

            if (door2Open && door3Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 4);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[1].position.x + spawnPosX, 0.25f, spawnPointLocs[1].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[3].position.x + spawnPosX, 0.25f, spawnPointLocs[3].position.z + spawnPosZ);
                        break;
                    case 3:
                        spawnPoint = new Vector3(spawnPointLocs[8].position.x + spawnPosX, 0.25f, spawnPointLocs[8].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door2Open && door3Open & !PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[1].position.x + spawnPosX, 0.25f, spawnPointLocs[1].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[3].position.x + spawnPosX, 0.25f, spawnPointLocs[3].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door2Open && !door3Open && !PowerSwitch.instance.powerOn)
            {
                spawnPoint = new Vector3(spawnPointLocs[1].position.x + spawnPosX, 0.25f, spawnPointLocs[1].position.z + spawnPosZ);
            }
            else if (door3Open && !door2Open && !PowerSwitch.instance.powerOn)
            {
                spawnPoint = new Vector3(spawnPointLocs[3].position.x + spawnPosX, 0.25f, spawnPointLocs[3].position.z + spawnPosZ);
            }
            else if (door2Open && !door3Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[1].position.x + spawnPosX, 0.25f, spawnPointLocs[1].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[8].position.x + spawnPosX, 0.25f, spawnPointLocs[8].position.z + spawnPosZ);
                        break;
                }
            }
            else if (!door2Open && door3Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[3].position.x + spawnPosX, 0.25f, spawnPointLocs[3].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[8].position.x + spawnPosX, 0.25f, spawnPointLocs[8].position.z + spawnPosZ);
                        break;
                }
            }
            
            return spawnPoint;
        }
        else if (currentZone == 4)
        {
            if (door3Open && door4Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 4);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[2].position.x + spawnPosX, 0.25f, spawnPointLocs[2].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[4].position.x + spawnPosX, 0.25f, spawnPointLocs[4].position.z + spawnPosZ);
                        break;
                    case 3:
                        spawnPoint = new Vector3(spawnPointLocs[8].position.x + spawnPosX, 0.25f, spawnPointLocs[8].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door3Open && door4Open && !PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[2].position.x + spawnPosX, 0.25f, spawnPointLocs[2].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[4].position.x + spawnPosX, 0.25f, spawnPointLocs[4].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door3Open && !door4Open)
            {
                spawnPoint = new Vector3(spawnPointLocs[2].position.x + spawnPosX, 0.25f, spawnPointLocs[2].position.z + spawnPosZ);
            }
            else if (door4Open && !door3Open)
            {
                spawnPoint = new Vector3(spawnPointLocs[4].position.x + spawnPosX, 0.25f, spawnPointLocs[4].position.z + spawnPosZ);
            }
            
            return spawnPoint;
        }
        else if (currentZone == 5)
        {

            if (door4Open && door8Open && PowerSwitch.instance.powerOn)
            {

                spawnRandomizer = Random.Range(1, 4);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[3].position.x + spawnPosX, 0.25f, spawnPointLocs[3].position.z + spawnPosZ);
                        break;
                    case 3:
                        spawnPoint = new Vector3(spawnPointLocs[7].position.x + spawnPosX, 0.25f, spawnPointLocs[7].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door4Open && door8Open && !PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[3].position.x + spawnPosX, 0.25f, spawnPointLocs[3].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[7].position.x + spawnPosX, 0.25f, spawnPointLocs[7].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door4Open && !door8Open && !PowerSwitch.instance.powerOn)
            {
                spawnPoint = new Vector3(spawnPointLocs[3].position.x + spawnPosX, 0.25f, spawnPointLocs[3].position.z + spawnPosZ);
            }
            else if (door4Open && !door8Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[3].position.x + spawnPosX, 0.25f, spawnPointLocs[3].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                }
            }
            else if (!door4Open && door8Open && !PowerSwitch.instance.powerOn)
            {
                spawnPoint = new Vector3(spawnPointLocs[7].position.x + spawnPosX, 0.25f, spawnPointLocs[7].position.z + spawnPosZ);
            }
            else if (!door4Open && door8Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[7].position.x + spawnPosX, 0.25f, spawnPointLocs[7].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                }
            }
            
            return spawnPoint;
        }
        else if (currentZone == 6)
        {

            if (door5Open && door6Open && PowerSwitch.instance.powerOn)
            {

                spawnRandomizer = Random.Range(1, 4);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[6].position.x + spawnPosX, 0.25f, spawnPointLocs[6].position.z + spawnPosZ);
                        break;
                    case 3:
                        spawnPoint = new Vector3(spawnPointLocs[7].position.x + spawnPosX, 0.25f, spawnPointLocs[7].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door5Open && door6Open && !PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[6].position.x + spawnPosX, 0.25f, spawnPointLocs[6].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door5Open && !door6Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[7].position.x + spawnPosX, 0.25f, spawnPointLocs[7].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door5Open && !door6Open && !PowerSwitch.instance.powerOn)
            {
                spawnPoint = new Vector3(spawnPointLocs[0].position.x + spawnPosX, 0.25f, spawnPointLocs[0].position.z + spawnPosZ);
            }
            else if (!door5Open && door6Open && PowerSwitch.instance.powerOn)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[6].position.x + spawnPosX, 0.25f, spawnPointLocs[6].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[7].position.x + spawnPosX, 0.25f, spawnPointLocs[7].position.z + spawnPosZ);
                        break;
                }
            }
            else if (!door5Open && door6Open && !PowerSwitch.instance.powerOn)
            {
                spawnPoint = new Vector3(spawnPointLocs[6].position.x + spawnPosX, 0.25f, spawnPointLocs[6].position.z + spawnPosZ);
            }

            return spawnPoint;
        }
        else if (currentZone == 7)
        {

            if (door6Open && door7Open)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[5].position.x + spawnPosX, 0.25f, spawnPointLocs[5].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[7].position.x + spawnPosX, 0.25f, spawnPointLocs[7].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door6Open && !door7Open)
            {
                spawnPoint = new Vector3(spawnPointLocs[5].position.x + spawnPosX, 0.25f, spawnPointLocs[5].position.z + spawnPosZ);
            }
            else if (!door6Open && door7Open)
            {
                spawnPoint = new Vector3(spawnPointLocs[7].position.x + spawnPosX, 0.25f, spawnPointLocs[7].position.z + spawnPosZ);
            }

            return spawnPoint;
        }
        else if (currentZone == 8)
        {

            if (door7Open && door8Open)
            {
                spawnRandomizer = Random.Range(1, 3);

                switch (spawnRandomizer)
                {
                    case 1:
                        spawnPoint = new Vector3(spawnPointLocs[4].position.x + spawnPosX, 0.25f, spawnPointLocs[4].position.z + spawnPosZ);
                        break;
                    case 2:
                        spawnPoint = new Vector3(spawnPointLocs[6].position.x + spawnPosX, 0.25f, spawnPointLocs[6].position.z + spawnPosZ);
                        break;
                }
            }
            else if (door7Open && !door8Open)
            {
                spawnPoint = new Vector3(spawnPointLocs[6].position.x + spawnPosX, 0.25f, spawnPointLocs[6].position.z + spawnPosZ);
            }
            else if (!door7Open && door8Open)
            {
                spawnPoint = new Vector3(spawnPointLocs[4].position.x + spawnPosX, 0.25f, spawnPointLocs[4].position.z + spawnPosZ);
            }

            return spawnPoint;
        }
        else if (currentZone == 9)
        {
            return new Vector3(spawnPointLocs[2].position.x + spawnPosX, 0.25f, spawnPointLocs[2].position.z + spawnPosZ);
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

    void SpawnThirdEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyThirdPrefab, GenerateSpawnPoint(PlayerController.instance.playerZone), enemyThirdPrefab.transform.rotation);
        }
    }

}
