using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapn_Projectile : MonoBehaviour
{
    [SerializeField] GameObject playerFirePoint;
    [SerializeField] GameObject enemyFirePoint;
    [SerializeField] List<GameObject> PlayerEffects = new List<GameObject>();
    [SerializeField] List<GameObject> EnemyEffects = new List<GameObject>();

    private GameObject playerEffectsToSpawn;
    private GameObject enemyEffectsToSpawn;
    private float timeToFire = 0;

    void Start()
    {
        playerEffectsToSpawn = PlayerEffects[0];
        enemyEffectsToSpawn = EnemyEffects[0];
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / playerEffectsToSpawn.GetComponent<Projectile>().fireRate;
            timeToFire = Time.time + 1 / enemyEffectsToSpawn.GetComponent<Projectile>().fireRate;
            SpawnFVX();
        }


        void SpawnFVX()
        {
            if (playerFirePoint != null)
            {
                _ = Instantiate(playerEffectsToSpawn, playerFirePoint.transform.position, playerFirePoint.transform.rotation);
                _ = Instantiate(enemyEffectsToSpawn, enemyFirePoint.transform.position, enemyFirePoint.transform.rotation);
            }
        }
    }
}
