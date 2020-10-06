using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnCore : MonoBehaviour
{
    [BoxGroup("SpawnPoints")]
    [GUIColor(.9f, .5f, .8f)]
    public Transform[] spawnPoints;

    [TabGroup("EnemyAndTime")]
    [GUIColor(.9f, .7f, .8f)]
    public float delayTime = .1f;

    [TabGroup("Missiles")]
    [GUIColor(.3f, .7f, .8f)]
    public GameObject[] missilePrefabs;

    [TabGroup("EnemyAndTime")]
    [GUIColor(.9f, .7f, .8f)]
    public int enemyCount, maxEnemyCount, currentMissile, delayToUpgrade = 20;


    [BoxGroup("SpawnCore")]
    [GUIColor(.9f, .7f, .8f)]
    public static SpawnCore instance;

    bool coroutineFinished;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        StartCoroutine(MissileSpawner());
    }

    void Update()
    {
        if (coroutineFinished)
        {
            StartCoroutine(DificultUpgrader());

            coroutineFinished = false;
        }
    }

    IEnumerator DificultUpgrader()
    {
        yield return new WaitForSeconds(20);

        maxEnemyCount++;
        delayTime /= 1.2f;

        coroutineFinished = true;
    }
    IEnumerator MissileSpawner()
    {

        while (enemyCount <= maxEnemyCount)
        {
            int randomPoint = Random.Range(0, spawnPoints.Length);
            int missileSpawnChance = Random.Range(0, 10);

            if (spawnPoints[randomPoint] == null)
                this.StopAllCoroutines();

            if (missileSpawnChance <= 2)
                currentMissile = 2;

            else if (missileSpawnChance > 2 && missileSpawnChance <= 6)
                currentMissile = 1;

            else if (missileSpawnChance > 6 && missileSpawnChance <= 10)
                currentMissile = 0;


            Instantiate(missilePrefabs[currentMissile], spawnPoints[randomPoint].position, Quaternion.identity);


            enemyCount++;
            yield return new WaitForSeconds(delayTime);
        }
        Debug.Log("something");
        StartCoroutine(MissileSpawner());
    }
}
