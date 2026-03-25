using UnityEngine;
using UnityEngine.UI;

public class Raycaster : MonoBehaviour
{
    public Camera cam;
    OutlineOnLook outline;

    public LayerMask layerMask;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 250, layerMask))
        {
            if (hit.transform.TryGetComponent(out OutlineOnLook outline))
            {
                if(this.outline != outline)
                {
                    this.outline?.Outline(false);
                }

                this.outline = outline;
                outline.Outline(true);
            }

            else
            {
                this.outline?.Outline(false);
                this.outline = default;
            }

        }

        else
        {
            outline?.Outline(false);
            outline = default;
        } 
    }


}
