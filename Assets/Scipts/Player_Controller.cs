using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : Base_Planet
{
    public float moveSpeed = 5f;
    public float maxDistance = 100f;

    Vector3 move;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Initialize();
    }

    private void Update()
    {
        SetRotationToPoint();
        if (Input.GetMouseButton(0) && Time.time >= timeToFire)
        {
            Attack();
        }
        if (health <= 0)
        {
            Exploed();
            Destroy(gameObject);
        }
    }

    void SetRotationToPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitinfo, maxDistance))
        {
            var target = hitinfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private void FixedUpdate()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        timeToFire = Time.time + 1 / effectToSpawn.GetComponent<Projectile>().fireRate;
        SpawnFVX();
    }

}
