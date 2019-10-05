using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public GameObject ResourceType;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.GetComponent<PlayerController>() != null)
        {
            MineController mine = Instantiate(Resources.Load<MineController>("Mine"), transform.position, transform.rotation);
            mine.resourceObject = ResourceType;
            mine.spawnRate = Random.Range(0.1f, 1.0f);
        }
    }
}
