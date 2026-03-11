using UnityEngine;

public class StartSlownessEffect : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponentInParent<EffectManager>().StartEffect("Slowness");
        }
    }
}
