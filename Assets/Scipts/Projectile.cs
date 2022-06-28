using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public float damage;
    public GameObject firePrefab;
    public GameObject explosionPrefab;
    void Update()
    {
        if (speed != 0)
            transform.position += transform.forward * (speed * Time.deltaTime);
    }
    private void Start()
    {
        var firePref = Instantiate(firePrefab, transform.position, Quaternion.identity);
        firePref.transform.forward = gameObject.transform.forward;
        var psFirePref = firePrefab.GetComponent<ParticleSystem>();

        if (psFirePref != null)
        {
            Destroy(firePref, psFirePref.main.duration);
        }

        else
        {
            var psChild = firePref.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(firePref, psChild.main.duration);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        speed = 0;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!CompareTag("AI_bullet"))
            {
                AI_brain ai = collision.gameObject.GetComponent<AI_brain>();
                ai.TakeDamge(damage);
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Controller pc = collision.gameObject.GetComponent<Player_Controller>();
            pc.TakeDamge(damage);
        }

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (explosionPrefab != null)
        {
            var exppref = Instantiate(explosionPrefab, pos, rot);
            var psexp = exppref.GetComponent<ParticleSystem>();
            if (psexp != null)
            {
                Destroy(exppref, psexp.main.duration);
            }

            else
            {
                var expchild = exppref.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(exppref, expchild.main.duration);
            }
        }
        Destroy(gameObject);
    }
}
