using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteController : MonoBehaviour, IEffectable
{
    [SerializeField] private float growSpeed;
    [SerializeField] private float fadeSpeed;

    [SerializeField] private GameObject remedy;
    [SerializeField] private GameObject player;

    [SerializeField] private AudioClip effectWarning;



    public bool isActive { get; set; } = false;
    public bool isRemedied { get; set; } = false;

    private Volume volume;
    private Vignette vignette;

    private float effectProgressWhenEnded;
    private float startDelay;


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

    public void StartEffect(float delay)
    {
        isActive = true;
        isRemedied = false;
        startDelay = delay;
        StartCoroutine(WaitUntilEffectStart(delay));
    }

    private IEnumerator WaitUntilEffectStart(float delay)
    {
        yield return new WaitForSeconds(delay);

        state = EffectState.Growing;
    }


    private void Update()
    {
        if (state == EffectState.Growing)
        {
            float oldIntestity = vignette.intensity.value;

            vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, 1, growSpeed * Time.deltaTime);
            Debug.Log(vignette.intensity.value);

            if (vignette.intensity.value > 0.5f  && oldIntestity < 0.5f)
            {
                AudioManager.Instance.PlayClip(effectWarning);
            }
        }

        if (state == EffectState.Ending)
        {
            vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, 0, fadeSpeed * Time.deltaTime);

            if (vignette.intensity.value == 0)
            {
                state = EffectState.NotActive;
                StartEffect(startDelay);
            }
        }

        float distanceToRemedy = Vector3.Distance(player.transform.position, remedy.transform.position);

        if (distanceToRemedy < 5)
        {
            isRemedied = true;
        }

    }

    public void EndEffect()
    {
        isActive = false;
        state = EffectState.Ending;
        effectProgressWhenEnded = vignette.intensity.value;
    }

}
