using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public interface IEffectable
{
    void StartEffect();
    void EndEffect();
    bool HasEffectEnded();
}

public class VignetteController : MonoBehaviour, IEffectable
{
    [SerializeField] private float lengthOfEffect;
    [SerializeField] private float returnToNormalLength;

    [SerializeField] private AudioClip effectWarning;


    private Volume volume;
    private Vignette vignette;

    private bool isEffectActive = false;
    private bool isReturningToNormal = false;

    private float timeElapsed;

    private float effectProgressWhenEnded;

    private void Awake()
    {


        volume = GetComponent<Volume>();

        if (!volume.profile.TryGet(out vignette))
        {
            Debug.Log("vignette not found!");
            return;
        }

    }

    public void StartEffect()
    {
        isReturningToNormal = false;

        timeElapsed = 0;
        isEffectActive = true;
    }

    private void Update()
    {
        if (isEffectActive && timeElapsed < lengthOfEffect)
        {
            vignette.intensity.value = Mathf.Lerp(0, 1, timeElapsed / lengthOfEffect);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > lengthOfEffect / 2 && timeElapsed < lengthOfEffect / 2 - 1 &&  !AudioManager.Instance.isClipPlaying(effectWarning))
            {
                AudioManager.Instance.PlayClip(effectWarning);
            }
        }

        if (isReturningToNormal && timeElapsed < returnToNormalLength)
        {
            vignette.intensity.value = Mathf.Lerp(effectProgressWhenEnded, 0, timeElapsed / returnToNormalLength);
            timeElapsed += Time.deltaTime;
        }
    }

    public void EndEffect()
    {
        isEffectActive = false;
        effectProgressWhenEnded = vignette.intensity.value;

        timeElapsed = 0;
        isReturningToNormal = true;
    }

    public bool HasEffectEnded()
    {
        return !(isEffectActive || isReturningToNormal);
    }

}
