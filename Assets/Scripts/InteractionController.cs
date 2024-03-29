using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Security.Cryptography.X509Certificates;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionIndicator;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float distanceToInteract=10f;
    [SerializeField] private GameObject holdingPosition, examinationPosition, boxPosition, key;
    [SerializeField] private Scrollbar launchingProgressBar;
    [SerializeField] private float playerStrength= 5f;
    private float currentStrengthToThrow = 0f;
    [SerializeField] private float changeStrengthValue = 0.05f;
    private float timer=0;
    private float timeUntilDetection=1f;
    private int interactableLayer = 1<<6;
    private GameObject currentlyHolding = null;
    private Vector3 offset = new Vector3(0, 0, 0.37f);


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
            if (Input.GetMouseButtonDown(0))
            {
                //Dependiendo del tag del objeto hace realiza distintas acciones, teniendo en cuenta lo que tiene en la mano cuando procede
                switch (hit.collider.gameObject.tag)
                {
                    case "Box":
                        if (currentlyHolding == null)
                        {
                            currentlyHolding = hit.collider.gameObject;
                            GetCloseUp();
                        }
                        break;
                    case "Poster":
                        hit.transform.GetComponent<Rigidbody>().useGravity = true;
                        break;
                    case "Lock":
                        if (currentlyHolding.tag == "Key")
                        {
                            currentlyHolding.transform.parent = transform.parent;
                            currentlyHolding.transform.DOMove(hit.transform.position, 0.2f).OnComplete(() => currentlyHolding.transform.parent = hit.transform);
                            currentlyHolding.transform.DORotate(hit.transform.forward, 0.2f);
                            currentlyHolding.transform.GetComponent<Rigidbody>().isKinematic = false;
                            currentlyHolding=null;
                            hit.collider.gameObject.GetComponent<DoorLockBehavior>().abrir();
                        }
                            break;
                        
                    default:
                        if (currentlyHolding == null)
                        {
                            currentlyHolding = hit.collider.gameObject;
                            GrabItem();
                        }
                        break;
                }
            }
            if(currentlyHolding==null)
            interactionIndicator.text = "o";
        }
        else
        {
            interactionIndicator.text = "x";
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            if(currentlyHolding!=null && currentlyHolding.tag=="Box")
                ExitCloseUp(false);
            else if(currentlyHolding!=null)
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
        currentlyHolding.transform.parent = holdingPosition.transform;
        currentlyHolding.transform.DOLocalMove(offset, 0.2f);
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

    private void GetCloseUp()
    {
        currentlyHolding.transform.DOMove(examinationPosition.transform.position, 0.2f);
        currentlyHolding.transform.DORotate(examinationPosition.transform.rotation.eulerAngles, 0.2f);
        currentlyHolding.GetComponent<Rigidbody>().isKinematic = true;
        currentlyHolding.GetComponent<Collider>().enabled = false;
        GetComponent<FirstPersonMovement>().enabled = false;
        GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
        playerCamera.GetComponent<FirstPersonLook>().enabled = false;
        currentlyHolding.GetComponent<LockController>().enabled = true;
    }

    private void ExitCloseUp(bool spawnKey)
    {
        currentlyHolding.transform.parent = transform.parent;
        if (!spawnKey)
            currentlyHolding.transform.DOMove(boxPosition.transform.position, 0.5f);
        else
            currentlyHolding.transform.DOMove(boxPosition.transform.position, 0.5f).OnComplete(SpawnKey);
        currentlyHolding.transform.DORotate(boxPosition.transform.rotation.eulerAngles, 0.2f);
        GetComponent<FirstPersonMovement>().enabled = true;
        playerCamera.GetComponent<FirstPersonLook>().enabled = true;
        currentlyHolding.GetComponent<LockController>().enabled = false;
        currentlyHolding.GetComponent<Collider>().enabled = true;
        currentlyHolding.GetComponent<Rigidbody>().isKinematic = false;
        currentlyHolding = null;
    }

    public void SpawnKey()
    {
        Instantiate(key, boxPosition.transform.position, Quaternion.identity);
    }

    public void EndPuzzle3()
    {
        currentlyHolding.layer = 0;
        ExitCloseUp(true);
    }
}
