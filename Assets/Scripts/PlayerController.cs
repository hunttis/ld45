using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10.0f;
    public float jumpHeight = 2f;
    public Vector3 movement;
    public LayerMask ground;

    private bool _isJumping;
    private float _groundDistance;
    private bool _isGrounded;
    private Transform _groundChecker;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _groundDistance = (float) (GetComponent<Renderer>().bounds.size.y * 0.55);
    }

    // Update is called once per frame
    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        _isGrounded = Physics.CheckSphere(rb.position, _groundDistance, ground, QueryTriggerInteraction.Ignore);
        _isJumping = Input.GetButtonDown("Jump") && _isGrounded;
    }

    private void FixedUpdate()
    {
        Move(movement);
        if (_isJumping) Jump();
    }

    private void Move(Vector3 direction)
    {
        rb.AddForce(direction * speed);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}