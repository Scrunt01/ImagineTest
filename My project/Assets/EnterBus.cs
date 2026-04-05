using NUnit.Framework.Internal;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class EnterBus : MonoBehaviour
{
    public Collider col;
    public bool isInRange;

    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private string dialogue;

    [SerializeField]
    private float interactableDistance;


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
            dialogueText.gameObject.SetActive(true);
            DisplayDialogue();

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.GoToNextScene();
            }
        }
        else dialogueText.gameObject.SetActive(false);
    }
    public void DisplayDialogue()
    {
        dialogueText.text = dialogue;
    }

}
