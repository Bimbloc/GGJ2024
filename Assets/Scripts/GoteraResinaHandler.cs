using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoteraResinaHandler : MonoBehaviour
{
    GameObject goteraObject;
    GameObject pegoteObject;
    public bool completed = false;
    // Start is called before the first frame update
    void Start()
    {
        goteraObject = transform.GetChild(0).gameObject;
        pegoteObject = transform.GetChild(1).gameObject;
        pegoteObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BarrelComponent>() != null)
        {
            complete();
        }
    }

    void complete()
    {
        goteraObject.SetActive(false);
        pegoteObject.SetActive(true);
        completed = true;
    }
    public void reset()
    {
        goteraObject.SetActive(true);
        pegoteObject.GetComponent<Rigidbody>().useGravity = true;
        completed = false;
    }
}
