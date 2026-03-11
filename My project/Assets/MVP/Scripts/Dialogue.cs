using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject player;


    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private string dialogue;

    [SerializeField]
    private float interactableDistance;


    private void Start()
    {
        dialogueText.text = "Press 'e' to ask for directions";
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < interactableDistance)
        {
            dialogueText.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                DisplayDialogue();
            }
        } else
        {
            dialogueText.gameObject.SetActive(false);
        }
    }

    public void DisplayDialogue()
    {
        dialogueText.text = dialogue;
    }


}
