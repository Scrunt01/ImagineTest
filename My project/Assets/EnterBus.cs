using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

public class EnterBus : MonoBehaviour
{
    public Collider col;
    public bool isInRange;

   

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("In Range");
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;

            
        }
    }
    public void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }
    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.GoToNextScene();
            }
        }
    }
}
