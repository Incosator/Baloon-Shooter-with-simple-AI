using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follower : MonoBehaviour
{
    [SerializeField] Transform observer;
    [SerializeField] float aheadSpeed;
    [SerializeField] float followCamera;
    [SerializeField] float camerHeight;

    Rigidbody rb;

    void Start()
    {
        rb = observer.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (observer == null)
            return;

        Vector3 targetPostition = observer.position + Vector3.up * camerHeight + rb.velocity * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPostition, followCamera * Time.deltaTime);
    }
}
