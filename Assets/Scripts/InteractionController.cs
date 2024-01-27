using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionIndicator;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float distanceToInteract=10f;
    [SerializeField] private GameObject holdingPosition;
    [SerializeField] private Scrollbar launchingProgressBar;
    [SerializeField] private float playerStrength= 5f;
    private float currentStrengthToThrow = 0f;
    [SerializeField] private float changeStrengthValue = 0.05f;
    private float timer=0;
    private float timeUntilDetection=1f;
    private int interactableLayer = 1<<6;
    private GameObject currentlyHolding = null;
    

    //Inicializaci�n de DoTween
    void Start()
    {
        DOTween.Init();
        launchingProgressBar.size = 0;
    }

    //Lanzamos un rayo hacia delante que cambia el indicador de la c�mara a una o cuando con lo
    //que choca est� en la layer Interactable
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, distanceToInteract,interactableLayer))
        {
            if (Input.GetMouseButtonDown(0)&&currentlyHolding==null)
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
        
        if (Input.GetMouseButtonDown(1))
        {
            if(currentlyHolding!=null)
            DropObject();
        }
        if (currentlyHolding != null && Input.GetMouseButton(0))
        {
            AccumulateStrength();
        }
        if (currentStrengthToThrow>0&& currentlyHolding != null && Input.GetMouseButtonUp(0))
        {
            launchingProgressBar.gameObject.SetActive(false);
            ThrowObject();
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

    private void AccumulateStrength()
    {
        if (timer>timeUntilDetection && currentStrengthToThrow <= 1)
        {
            launchingProgressBar.gameObject.SetActive(true);
            currentStrengthToThrow += changeStrengthValue*Time.deltaTime;
            launchingProgressBar.size = currentStrengthToThrow;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void ThrowObject()
    {
        currentlyHolding.transform.parent = transform.parent;
        currentlyHolding.GetComponent<Collider>().enabled = true;
        currentlyHolding.GetComponent<Rigidbody>().isKinematic = false;
        currentlyHolding.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward*currentStrengthToThrow*playerStrength, ForceMode.Impulse);
        currentlyHolding = null;
        launchingProgressBar.size = 0;
        currentStrengthToThrow = 0;
        timer = 0;
    }
}
