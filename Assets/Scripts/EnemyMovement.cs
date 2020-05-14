using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] ParticleSystem selfDestructVFX;
    [SerializeField] float enemySpeed = 1f;
    [SerializeField] int enemyDamage = 1;
    [SerializeField] AudioClip selfDestruct;


    // Start is called before the first frame update
    void Start()
    {
        PathFinder pathfinder = FindObjectOfType<PathFinder>();
        var path = pathfinder.GetShortestPath();
        StartCoroutine(FollowPath(path));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FollowPath(List<Block> path)
    {
        foreach (Block block in path)
        {
            yield return new WaitForSeconds(enemySpeed);
            transform.position = block.transform.position;
        }

        SelfDestruct();
    }

    private void SelfDestruct()
    {
        FindObjectOfType<PlayerHealth>().DecreaseHealth(enemyDamage);
        AudioSource.PlayClipAtPoint(selfDestruct, Camera.main.transform.position);
        var vfx = Instantiate(selfDestructVFX, transform.position, Quaternion.identity);
        Destroy(vfx.gameObject, vfx.main.duration);

        Destroy(gameObject);
    }
}
