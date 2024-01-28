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
            reset(this);
        }
    }

    private void reset(PegoteController peg)
    {
        peg.transform.position = peg.initialPosition;
        peg.GetComponent<Rigidbody>().useGravity = false;
        peg.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        peg.gameObject.SetActive(false);
    }
}
