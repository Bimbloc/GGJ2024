using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegoteController : MonoBehaviour
{
    private Vector3 initialPosition;
    private void Start()
    {
        initialPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 1 << 3)
        {
            transform.position = initialPosition;
            gameObject.SetActive(false);
        }
    }
}
