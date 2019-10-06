using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public float _jetPackRefuelRate = 0.15f;
    public float _jetPackFuelMax = 0.5f;
    private float _jetPackFuel = 0.5f;
    public ParticleSystem _jetPackFX;

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

    private PlacableCannon _spawnableCannon;
    private float _interactionDistance = 5.0f;

    private PlacableCannon _targetedCannon = null;
    private PlacableCannon _adjustedCannon = null;

    private AudioSource thumpSound;

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
        _groundDistance = GetComponent<Collider>().bounds.size.y * 1f;
        _playerModel = FindObjectOfType<PlayerModel>();
        cameraTarget = FindObjectOfType<PlayerCameraPositionTarget>();
        _rb.maxAngularVelocity = 50f;

        thumpSound = GetComponent<AudioSource>();

        LockCursor();
        transform.SetParent(null);
    }

    private void SwitchState(ControllerState newState)
    {
        switch (_placeMode)
        {
            case ControllerState.Adjusting:
                transform.SetParent(null);
                _playerModel.transform.rotation = Quaternion.Euler(_playerModel.transform.rotation.eulerAngles.y * Vector3.up);
                _adjustedCannon = null;
                break;
        }
        switch (newState)
        {
            case ControllerState.Placing:
                _spawnableCannon = Instantiate(Resources.Load<PlacableCannon>("MagnetCannon"), _playerModel.transform.position + _playerModel.transform.forward*3 + Vector3.down*0.1f, _playerModel.transform.rotation, _playerModel.transform);
                //_spawnableCannon.transform.Rotate(Vector3.up, 90);
                _spawnableCannon.EnableBlueprintMode();

                foreach(Collider c in _spawnableCannon.GetComponentsInChildren<Collider>())
                {
                    c.enabled = false;
                }
                
                break;
            case ControllerState.Adjusting:
                LockCursor();
                _adjustedCannon = _targetedCannon;
                break;
            case ControllerState.Moving:
                LockCursor();

                break;

        }
        _placeMode = newState;
    }

    private void Update()
    {
        if (_placeMode != ControllerState.Adjusting)
        {
            RaycastHit rch;
            if (Physics.Raycast(_playerModel.transform.position + Vector3.up, _playerModel.transform.forward, out rch, _interactionDistance, 1 << 9))
            {
                _targetedCannon = rch.transform.GetComponent<PlacableCannon>();
                _targetedCannon.SetHighlighted(true);
            } else
            {
                if (_targetedCannon != null) _targetedCannon.SetHighlighted(false);
                _targetedCannon = null;
            }

            movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            _isJumping = Input.GetButton("Jump");
            
            if (!_isJumping && _jetPackFuel < _jetPackFuelMax)
            {
                _jetPackFuel = Mathf.Clamp(_jetPackFuel + _jetPackRefuelRate * Time.deltaTime, 0, _jetPackFuelMax);
            }


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
        } else
        {
            _rb.Sleep();
            transform.SetPositionAndRotation(_adjustedCannon.AdjustSeat.position, _adjustedCannon.AdjustSeat.rotation);
            _playerModel.transform.rotation = _adjustedCannon.AdjustSeat.rotation;

            _adjustedCannon.AddAngle(Input.GetAxis("Mouse Y"));
            _adjustedCannon.AddRotation(Input.GetAxis("Mouse X"));
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleMouseLock();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (_placeMode)
            {
                case ControllerState.Moving:
                    if (_targetedCannon != null) SwitchState(ControllerState.Adjusting);
                    else SwitchState(ControllerState.Placing);
                    break;
                case ControllerState.Placing:
                    _spawnableCannon.transform.SetParent(null);
                    _spawnableCannon.DisableBlueprintMode();
                    foreach (Collider c in _spawnableCannon.GetComponentsInChildren<Collider>())
                    {
                        c.enabled = true;
                    }
                    SwitchState(ControllerState.Moving);
                    break;
                case ControllerState.Adjusting:
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
        if (_placeMode == ControllerState.Adjusting) return;
        Move(movement);
        if (_isJumping) Jump();
    }

    private void Move(Vector3 direction)
    {
        Vector3 rotatedDirection = Quaternion.Euler(0, 90, 0) * _playerModel.transform.TransformDirection(direction);
//        _rb.angularVelocity += rotatedDirection * speed;
        _rb.AddTorque(rotatedDirection * speed, ForceMode.VelocityChange);
    }

    private void Jump()
    {
        if (_jetPackFuel > 0)
        {
            Instantiate(_jetPackFX, _playerModel.transform.position + Vector3.down, Quaternion.identity);
            thumpSound.Play(0);
            _rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            _jetPackFuel -= Time.deltaTime;
        }
    }

    public void Die(string currentScene)
    {
        PlayerPrefs.SetString("currentWorldScene", currentScene);
        SceneManager.LoadScene("GameOverScene");
    }
}
