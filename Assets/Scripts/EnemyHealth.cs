using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem enemyHitVFX;
    [SerializeField] ParticleSystem enemyDeathVFX;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    private void OnParticleCollision(GameObject other)
    {
        hitPoints--;
        enemyHitVFX.Play();
        AudioSource.PlayClipAtPoint(enemyHitSFX, Camera.main.transform.position);
        if (hitPoints < 1)
        {
            Instantiate(enemyDeathVFX, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
