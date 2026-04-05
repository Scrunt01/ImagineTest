using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLastScene : MonoBehaviour
{
    public bool isInRange;


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("In Range");
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;

        }
    }

    private void Update()
    {
        if (isInRange)
        {
            GameManager.Instance.GoToNextScene();

        }
    }

}
