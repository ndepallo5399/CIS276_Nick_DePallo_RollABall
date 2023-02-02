using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRigidbodyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    private Rigidbody rb;
    private Vector3 input;
    private bool isJumping;
    private float score;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            score++;
        }
        if (other.gameObject.CompareTag("Bouncy"))
        {
            rb.AddForce(speed * 2 * Vector3.up, ForceMode.Impulse);
            other.transform.Rotate(0, 45, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = (rb.velocity.y * Vector3.up) + (input * speed);
        if (isJumping)
        {
            isJumping = false;
            rb.AddForce(speed * Vector3.up, ForceMode.Impulse);
        }
    }

    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }
}