using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ToroidalComponent : MonoBehaviour
{

    [SerializeField] private GameObject[] walls = new GameObject[4];

    Transform myTransform;
    Rigidbody myRigidbody;

    float maxXWall = 0,
        maxZWall = 0,
        minXWall = 0, 
        minZWall = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        myTransform = gameObject.transform;
        myRigidbody = GetComponent<Rigidbody>();

        foreach (GameObject wall in walls)
        {
            maxXWall = Mathf.Max(maxXWall, wall.transform.position.x);
            minXWall = Mathf.Min(minXWall, wall.transform.position.x);
            maxZWall = Mathf.Max(maxZWall, wall.transform.position.z);
            minZWall = Mathf.Min(minZWall, wall.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // x positiva
        if (myTransform.position.x >= maxXWall && myRigidbody.velocity.x > 0) setX(minXWall);
        // x negativa
        else if (myTransform.position.x <= minXWall && myRigidbody.velocity.x < 0) setX(maxXWall);
        // z positiva
        else if (myTransform.position.z >= maxZWall && myRigidbody.velocity.z > 0) setY(minZWall);
        // z negativa
        else if (myTransform.position.z <= minZWall && myRigidbody.velocity.z < 0) setY(maxZWall);
    }

    void setX(float x)
    {
        Vector3 pos = myTransform.position;
        pos.x = x;
        myTransform.SetPositionAndRotation(pos, myTransform.rotation);
    }

    void setY(float z)
    {
        Vector3 pos = myTransform.position;
        pos.z = z;
        myTransform.SetPositionAndRotation(pos, myTransform.rotation);
    }
}
