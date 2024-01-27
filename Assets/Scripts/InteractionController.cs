using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionIndicator;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float distanceToInteract=10f;
    [SerializeField] private GameObject holdingPosition;
    private int interactableLayer = 1<<6;
    private GameObject currentlyHolding = null;


    //Inicialización de DoTween
    void Start()
    {
        DOTween.Init();
    }

    //Lanzamos un rayo hacia delante que cambia el indicador de la cámara a una o cuando con lo
    //que choca está en la layer Interactable
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, distanceToInteract,interactableLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Dependiendo del tag del objeto hace realiza distintas acciones, teniendo en cuenta lo que tiene en la mano cuando procede
                switch (hit.collider.gameObject.tag)
                {
                    default:
                        currentlyHolding = hit.collider.gameObject;
                        GrabItem();
                        break;
                }
            }
            interactionIndicator.text = "o";
        }
        else
        {
            interactionIndicator.text = "x";
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(currentlyHolding!=null)
            DropObject();
        }
    }

    private void DropObject()
    {
        currentlyHolding.transform.parent = transform.parent;
        currentlyHolding.GetComponent<Collider>().enabled = true;
        currentlyHolding.GetComponent<Rigidbody>().isKinematic = false;
        currentlyHolding = null;
    }

    private void GrabItem()
    {
        currentlyHolding.transform.parent = playerCamera.transform;
        currentlyHolding.transform.DOMove(holdingPosition.transform.position, 0.2f);
        currentlyHolding.transform.DORotate(playerCamera.transform.rotation.eulerAngles, 0.2f);
        currentlyHolding.GetComponent<Rigidbody>().isKinematic = true;
        currentlyHolding.GetComponent<Collider>().enabled = false;

    }

}
