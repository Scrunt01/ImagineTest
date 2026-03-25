using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static VignetteController;

public class NauseaStatusEffect : MonoBehaviour, IEffectable
{
    [SerializeField] private float startDelay = 5.0f;
    [SerializeField] private float growSpeed;
    [SerializeField] private float fadeSpeed;

    [SerializeField] private float startEffectIntensity = 30.0f;
    [SerializeField] private float finalEffectIntensity = 3.0f;

    [SerializeField] private LayerMask remedyLayer;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject pickUpTooltip;

    private float effectIntensity;

    private float startLensDisIntensity;
    private float startDepthOfFieldFocalLength;

    public bool isActive { get; private set; }

    public bool isRemedied { get; private set; }

    public string effectName { get; set; } = "Nausea";


    private Volume volume;
    private DepthOfField depthOfField;
    private LensDistortion lensDistortion;




    private EffectState state;

    private void Awake()
    {
        volume = GetComponent<Volume>();

        if (!volume.profile.TryGet(out depthOfField))
        {
            Debug.Log("depth of field not found!");
            return;
        }

        if (!volume.profile.TryGet(out lensDistortion))
        {
            Debug.Log("depth of field not found!");
            return;
        }

        startDepthOfFieldFocalLength = depthOfField.focalLength.value;
        startLensDisIntensity = lensDistortion.intensity.value;

        state = EffectState.NotActive;
    }


    public void StartEffect()
    {
        isActive = true;
        isRemedied = false;
        effectIntensity = startEffectIntensity;

        StartCoroutine(WaitUntilEffectStart(startDelay));
    }

    private IEnumerator WaitUntilEffectStart(float delay)
    {
        yield return new WaitForSeconds(delay);

        state = EffectState.Growing;
        Debug.Log("The nausea statusEffect-state changed to: " + state.ToString());

        StartCoroutine(CreateEffect());
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EffectState.Growing)
        {
            effectIntensity = Mathf.MoveTowards(effectIntensity, finalEffectIntensity, growSpeed * Time.deltaTime);



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
            lensDistortion.intensity.value = 
                Mathf.MoveTowards(lensDistortion.intensity.value, startLensDisIntensity, fadeSpeed * Time.deltaTime);

            depthOfField.focalLength.value = 
                Mathf.MoveTowards(depthOfField.focalLength.value, startDepthOfFieldFocalLength, fadeSpeed * Time.deltaTime);

            if (depthOfField.focalLength.value == startDepthOfFieldFocalLength && lensDistortion.intensity.value == startLensDisIntensity)
            {
                state = EffectState.NotActive;
                Debug.Log("The nausea statusEffect-state changed to: " + state.ToString());
                StartEffect();
            }
        }
    }

    private IEnumerator CreateEffect()
    {
        float newLensIntensity = lensDistortion.intensity.value;
        float newDoFFocalLength = depthOfField.focalLength.value;
        float oldLensIntensity = lensDistortion.intensity.value;
        float oldDoFFocalLength = depthOfField.focalLength.value;
        float timeElapsed = 0;

        while (state == EffectState.Growing) 
        { 

            if (lensDistortion.intensity.value == newLensIntensity && depthOfField.focalLength.value == newDoFFocalLength)
            {
                timeElapsed = 0;

                newLensIntensity = Random.Range(-0.6f, 0.6f);
                newDoFFocalLength = Random.Range(70, 140);

                oldLensIntensity = lensDistortion.intensity.value;
                oldDoFFocalLength = depthOfField.focalLength.value;

                Debug.Log("new nausea goal");
            }

            lensDistortion.intensity.value = Mathf.Lerp(oldLensIntensity, newLensIntensity, timeElapsed/ effectIntensity);

            //lensDistortion.intensity.value =
            //Mathf.MoveTowards(lensDistortion.intensity.value, newLensIntensity, effectIntensity * Time.deltaTime);

            //Debug.Log(lensDistortion.intensity.value.ToString() + " " + newLensIntensity);

            depthOfField.focalLength.value = Mathf.Lerp(oldDoFFocalLength, newDoFFocalLength, timeElapsed / effectIntensity);

            //depthOfField.focalLength.value =
            //    Mathf.MoveTowards(depthOfField.focalLength.value, newDoFFocalLength, effectIntensity * Time.deltaTime);

            //Debug.Log(depthOfField.focalLength.value.ToString() + " " + newDoFFocalLength);


            timeElapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    
    }

    public void EndEffect()
    {
        isActive = false;
        state = EffectState.Ending;
        Debug.Log("The nausea statusEffect-state changed to: " + state.ToString());
    }

    
}
