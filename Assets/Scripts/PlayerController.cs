using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum ControllerState { Moving, Placing, Adjusting }

public class PlayerController : MonoBehaviour
{
    public static bool MouseLocked { get { return Cursor.lockState != CursorLockMode.None; }}

    public float speed;
    public float jumpHeight = 2f;
    public Vector2 mouseSensitivity = new Vector2(2.0f, 0.4f);
    Vector3 movement;
    public LayerMask ground;

    private float _cameraHeightMin = -0.3f;
    private float _cameraHeightMax = 10.0f;


    private Rigidbody _rb;
    private bool _isJumping;
    private float _groundDistance;
    private bool _isGrounded;
    private Transform _groundChecker;
    private PlayerCameraPositionTarget cameraTarget;
    private PlayerModel _playerModel;

    private Vector3 _placeModeVelocity;

    private ControllerState _placeMode = ControllerState.Moving;
    private Vector3 _mouseWorldPos = new Vector3();

    private Transform _spawnableCannon;

    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public static void ReleaseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_mouseWorldPos, 1.0f);
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _groundDistance = GetComponent<Collider>().bounds.size.y * 0.55f;
        _playerModel = FindObjectOfType<PlayerModel>();
        cameraTarget = FindObjectOfType<PlayerCameraPositionTarget>();
        _rb.maxAngularVelocity = 100f;

        LockCursor();
        transform.SetParent(null);
    }

    private void SwitchState(ControllerState newState)
    {
        switch (newState)
        {
            case ControllerState.Placing:
                _spawnableCannon = Instantiate(Resources.Load<Transform>("Cannon"), _playerModel.transform.position + _playerModel.transform.forward*3 + Vector3.down*0.1f, _playerModel.transform.rotation, _playerModel.transform);
                //_spawnableCannon.transform.Rotate(Vector3.up, 90);
                _spawnableCannon.GetComponentInChildren<FireController>().enabled = false;

                foreach(Collider c in _spawnableCannon.GetComponentsInChildren<Collider>())
                {
                    c.enabled = false;
                }
                
                break;
            case ControllerState.Adjusting:
                LockCursor();
                break;
            case ControllerState.Moving:
                LockCursor();

                break;

        }
        _placeMode = newState;
    }

    private void Update()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        _isGrounded = Physics.CheckSphere(_rb.position, _groundDistance, ground, QueryTriggerInteraction.Ignore);
        _isJumping = Input.GetButtonDown("Jump") && _isGrounded;

        if (MouseLocked)
        {
            _playerModel.transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivity.x, 0);

            if (cameraTarget != null)
            {
                Vector3 cameraTargetPos = cameraTarget.transform.localPosition;
                cameraTargetPos.y = Mathf.Clamp(cameraTargetPos.y + Input.GetAxis("Mouse Y") * mouseSensitivity.y, _cameraHeightMin, _cameraHeightMax);
                cameraTarget.transform.localPosition = cameraTargetPos;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.LeftControl)) ToggleMouseLock();

        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (_placeMode)
            {
                case ControllerState.Moving:
                    SwitchState(ControllerState.Placing);
                    break;
                case ControllerState.Placing:
                    _spawnableCannon.transform.SetParent(null);
                    _spawnableCannon.GetComponentInChildren<FireController>().enabled = true;
                    foreach (Collider c in _spawnableCannon.GetComponentsInChildren<Collider>())
                    {
                        c.enabled = true;
                    }
                    SwitchState(ControllerState.Moving);
                    break;

            }
        }

    }

    private void ToggleMouseLock()
    {
        if (!MouseLocked)
        {
            LockCursor();
        }
        else
        {
            ReleaseCursor();
        }
}

    private void FixedUpdate()
    {
        Move(movement);
        if (_isJumping) Jump();
    }

    private void Move(Vector3 direction)
    {
        Vector3 rotatedDirection = Quaternion.Euler(0, 90, 0) * _playerModel.transform.TransformDirection(direction);
        _rb.angularVelocity += rotatedDirection * speed;
//        _rb.AddTorque(rotatedDirection * speed, ForceMode.VelocityChange);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}