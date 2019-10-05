using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpHeight = 2f;
    public Vector3 movement;
    public LayerMask ground;

    private Rigidbody _rb;
    private bool _isJumping;
    private float _groundDistance;
    private bool _isGrounded;
    private Transform _groundChecker;

    // Use this for initialization
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _groundDistance = (float) (GetComponent<Collider>().bounds.size.y * 0.55);
    }

    // Update is called once per frame
    private void Update()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        _isGrounded = Physics.CheckSphere(_rb.position, _groundDistance, ground, QueryTriggerInteraction.Ignore);
        _isJumping = Input.GetButtonDown("Jump") && _isGrounded;
    }

    private void FixedUpdate()
    {
        Move(movement);
        if (_isJumping) Jump();
    }

    private void Move(Vector3 direction)
    {
        _rb.AddForce(direction * speed);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}