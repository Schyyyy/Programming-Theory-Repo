using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemyPrefabs;
    [SerializeField] int spawnRange;
    [SerializeField] int spawnZ;
    [SerializeField] float startDelay;
    [SerializeField] float spawnRate;

    protected float currentHp;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        if(GameManager.instance.playing)
        {
            int randomNumber = Random.Range(0, enemyPrefabs.Count());
            Enemy e = Instantiate(enemyPrefabs[randomNumber], new Vector3(Random.Range(-spawnRange, spawnRange), 2, spawnZ), enemyPrefabs[randomNumber].transform.rotation);
            GameManager.instance.enemysOnField.Add(e);
            GameManager.instance.hpMultiplyer += 0.1f;
        }
    }
}
