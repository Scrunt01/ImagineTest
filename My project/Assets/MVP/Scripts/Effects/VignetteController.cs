using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteController : MonoBehaviour, IEffectable
{
    [SerializeField] private float growSpeed;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float startDelay;

    [SerializeField] private LayerMask remedyLayer;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject pickUpTooltip;

    [SerializeField] private AudioClip effectWarning;


    public string effectName { get; private set; } = "Vignette";
    public bool isActive { get; set; } = false;
    public bool isRemedied { get; set; } = false;

    private Volume volume;
    private Vignette vignette;


    public enum EffectState
    {
        NotActive = 0,
        Growing = 1,
        Ending = 2
    }

    private EffectState state;

    private void Awake()
    {
        volume = GetComponent<Volume>();

        if (!volume.profile.TryGet(out vignette))
        {
            Debug.Log("vignette not found!");
            return;
        }

        state = EffectState.NotActive;
    }

    public void StartEffect()
    {
        isActive = true;
        isRemedied = false;
        StartCoroutine(WaitUntilEffectStart(startDelay));
    }

    private IEnumerator WaitUntilEffectStart(float delay)
    {
        yield return new WaitForSeconds(delay);

        state = EffectState.Growing;
        Debug.Log("The vignette statusEffect-state changed to: " + state.ToString());
    }


    private void Update()
    {
        if (state == EffectState.Growing)
        {
            float oldIntestity = vignette.intensity.value;

            vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, 1, growSpeed * Time.deltaTime);

            if (vignette.intensity.value > 0.5f  && oldIntestity < 0.5f)
            {
                AudioManager.Instance.PlayClip(effectWarning);
            }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                    Debug.DrawRay(cam.transform.position, cam.transform.forward);

                    if (Physics.Raycast(ray, out var hit, remedyLayer))
                    {
                        if (hit.transform.gameObject.CompareTag("Bench"))
                        {
                            isRemedied = true;
                            
                            
                        }
                    }
                }
            
        }

        if (state == EffectState.Ending)
        {
            vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, 0, fadeSpeed * Time.deltaTime);

            if (vignette.intensity.value == 0)
            {
                state = EffectState.NotActive;
                Debug.Log("The vignette statusEffect-state changed to: " + state.ToString());
                StartEffect();
            }
        }
    }

    public void EndEffect()
    {
        isActive = false;
        state = EffectState.Ending;
        Debug.Log("The vignette statusEffect-state changed to: " + state.ToString());
    }


}
