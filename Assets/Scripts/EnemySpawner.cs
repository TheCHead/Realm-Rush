using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] AudioClip enemySpawnSFX;
    int numberOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (numberOfEnemies < 10)
        {
            GetComponent<AudioSource>().PlayOneShot(enemySpawnSFX);
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = transform;
            numberOfEnemies++;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
