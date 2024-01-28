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
    private void Update()
    {
        if (transform.position.y<0.5f)
        {
            transform.position = initialPosition;
            GetComponent<Rigidbody>().useGravity = false;
            gameObject.SetActive(false);
        }
    }
}
