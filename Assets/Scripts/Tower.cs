using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //parameters of each tower
    [SerializeField] Transform objectToPan;
    [SerializeField] float fireRange = 40f;
    [SerializeField] ParticleSystem projectileParticle;

    //state of each tower
    EnemyMovement targetEnemy;
    Block baseBlock;

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();

        if (targetEnemy)
        {
            FireProjectile();
        }
        else
        {
            Shoot(false);
        }
        
    }

    private void SetTargetEnemy()
    {
        var enemies = FindObjectsOfType<EnemyMovement>();
        if (enemies.Length == 0) { return; }

        GetClosestEnemy(enemies);
    }

    private void GetClosestEnemy(EnemyMovement[] enemies)
    {
        targetEnemy = enemies[0];
        float closestDist = Vector3.Distance(targetEnemy.transform.position, transform.position);
        foreach (EnemyMovement enemy in enemies)
        {
            float nextClosestDist = Vector3.Distance(enemy.transform.position, transform.position);
            if (nextClosestDist < closestDist)
            {
                targetEnemy = enemy;
                closestDist = nextClosestDist;
            }
        }
    }

    private void FireProjectile()
    {
        if (Vector3.Distance(targetEnemy.transform.position, transform.position) <= fireRange)
        {
            objectToPan.LookAt(targetEnemy.transform);
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }

    public void SetBaseBlock(Block block)
    {
        baseBlock = block;
    }

    public Block GetBaseBlock()
    {
        return baseBlock;
    }
}
