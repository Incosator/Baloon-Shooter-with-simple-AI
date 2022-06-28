using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Planet : MonoBehaviour
{
    public GameObject deathPrefab;
    public GameObject firePoint;
    public List<GameObject> effects = new List<GameObject>();

    public float health;
    protected float timeToFire = 0;
    protected GameObject effectToSpawn;

    protected void Initialize()
    {
        effectToSpawn = effects[0];
    }
    protected void SpawnFVX()
    {
        if (firePoint != null)
            Instantiate(effectToSpawn,
               firePoint.transform.position,
               firePoint.transform.rotation);
    }

    protected void Exploed()
    {
        var death = Instantiate(deathPrefab, transform.position, Quaternion.identity);
        Destroy(death, 10f);
    }

    public void TakeDamge(float dmg)
    {
        health -= dmg;
    }
}
