using UnityEngine;

public class OutlineOnLook : MonoBehaviour
{
    // This script is on the player

    
    [SerializeField] int indexOfMat = 1;

    [SerializeField] Camera cam;

    private bool inBenchArea = false;
    private Material shaderMat;

    public bool IsOutlineActive { get { return isOutlineActive; } 
        set
        {
            if (value == isOutlineActive) return;

            isOutlineActive = value;
            SetOutline(value);
        }

    }

    private bool isOutlineActive;

    public void SetOutline(bool show)
    {
        Debug.Log(System.Convert.ToSingle(show));
        shaderMat.SetFloat("_ShowOutLine", System.Convert.ToSingle(show));
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (inBenchArea)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(cam.transform.position, cam.transform.forward);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.gameObject.CompareTag("Bench"))
                {
                    IsOutlineActive = true;
                }
                else
                {
                    IsOutlineActive = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bench"))
        {
            shaderMat = other.gameObject.GetComponentInParent<Renderer>().materials[indexOfMat];
            inBenchArea = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bench"))
        {

            inBenchArea = false;
            IsOutlineActive = false;
        }
    }
}
