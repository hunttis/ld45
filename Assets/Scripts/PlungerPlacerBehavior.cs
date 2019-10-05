using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Animations;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public enum PlungerPlacerState
{
  Idle,
  AdjustYaw,
  AdjustPitch,
}

public class PlungerPlacerBehavior : MonoBehaviour
{
  public GameObject plungerPrefab;

  public GameObject aimGuidePrefab;

  public Material placeholderMaterial;

  private PlungerPlacerState _state = PlungerPlacerState.Idle;

  private float _yaw;

  private Quaternion _yawRot;

  private float _pitch;

  private GameObject _aimGuide;
  private Material _originalMaterial;
  private GameObject _placeholder;
  void Update()
  {
    var done = Input.GetButtonDown("Fire1");
    switch (_state)
    {
      case PlungerPlacerState.AdjustYaw:
        UpdateYaw(done);
        break;
      case PlungerPlacerState.AdjustPitch:
        UpdatePitch(done);
        break;
    }
  }

  public void Activate()
  {
    if (_state != PlungerPlacerState.Idle) { return; }
    
    _state = PlungerPlacerState.AdjustYaw;

    var position = transform.position;
    _placeholder = Instantiate(plungerPrefab, position, Quaternion.identity);
    //_placeholder.GetComponent<Renderer>().material = placeholderMaterial;

    _aimGuide = Instantiate(aimGuidePrefab, position + (Vector3.forward * 2f), Quaternion.identity);
    UpdateAimGuide();
  }

  private void UpdateYaw(bool done)
  {
    if (done)
    {
      LockYaw();
      return;
    }

    var horizontal = Input.GetAxis("Mouse X");
    _placeholder.transform.Rotate(Vector3.up, horizontal);
    _yaw += horizontal;
    UpdateAimGuide();
  }

  private void UpdatePitch(bool done)
  {
    if (done)
    {
      LockPitch();
      return;
    }

    var vertical = Input.GetAxis("Mouse Y");
    _pitch += vertical;
    UpdateAimGuide();
  }

  private void UpdateAimGuide()
  {
    var yaw = Quaternion.AngleAxis(_yaw, Vector3.up);
    var pitch = Quaternion.Euler(_pitch, 0f, 0f);
    var angle = yaw * pitch;
    
    var distance = (Vector3.forward * 2f);
    if(_aimGuide == null) _aimGuide = Instantiate(aimGuidePrefab, transform.position + (Vector3.forward * 2f), Quaternion.identity);
        _aimGuide.transform.position = transform.position + angle * distance;
    _aimGuide.transform.rotation = angle * Quaternion.Euler(90f, 0f, 0f);
  }

  private void LockYaw()
  {
    _state = PlungerPlacerState.AdjustPitch;
  }

  private void LockPitch()
  {
    _state = PlungerPlacerState.Idle;
    Instantiate(plungerPrefab, transform.position, transform.rotation);
    // TODO: pass rotation, aim params
    Destroy(_placeholder);
    Destroy(_aimGuide);
    Destroy(this);
  }
}