using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickuppableObjects : MonoBehaviour
{


    [SerializeField] private GameObject[] objects;

    [SerializeField] private bool[] areObjectsHeld;

    [SerializeField] private LayerMask objectMask;

    [SerializeField] private Transform heldObjectsPosition;

    [SerializeField] private Canvas pickUpTooltip;

    private void Start()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (areObjectsHeld[i])
            {
                PickUpObject(i);

            }
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Drop a random object from the objects array
            if (areObjectsHeld.All(x => x == false) )
            {
                Debug.Log("No objects left!");
                return;
            }

            int objIndex = Random.Range(0, objects.Length);

            while (!areObjectsHeld[objIndex])
            {
                objIndex = Random.Range(0, objects.Length);
            }
            
            DropObject(objIndex);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Camera cam = GetComponentInChildren<Camera>();

            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(cam.transform.position, cam.transform.forward );
            
            if (Physics.Raycast(ray, out var hit, objectMask))
            {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.CompareTag("BreadcrumObject"))
                {
                    hitObj = hitObj.transform.parent.gameObject;
                }
                Debug.Log(hitObj.name); 
                

                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i] == hitObj)
                    {
                        PickUpObject(i);
                        
                    }
                }
            }
        }
    }

    private void DropObject(int objIndex)
    {
        objects[objIndex].transform.position = transform.position;
        objects[objIndex].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        areObjectsHeld[objIndex] = false;

        Debug.Log("Picked up the " + objects[objIndex].name);
    }

    private void PickUpObject(int objIndex)
    {
        objects[objIndex].transform.position = heldObjectsPosition.position;
        areObjectsHeld[objIndex] = true;

        Debug.Log("Dropped the " + objects[objIndex].name);
    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BreadcrumObject")) 
        {
            pickUpTooltip.transform.position = other.gameObject.transform.position + Vector3.up;
            pickUpTooltip.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BreadcrumObject"))
        {
            
            pickUpTooltip.gameObject.SetActive(false);
        }
    }
}
