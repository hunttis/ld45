using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldBoundsBehavior : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.Die(SceneManager.GetActiveScene().name);
        }

        Destroy(other.gameObject);
    }
}
