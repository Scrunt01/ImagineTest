using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;


public interface IEffectable
{
    void StartEffect(float delay);
    void EndEffect();
    bool isActive { get; }
    bool isRemedied { get; }

}

// Once an effect has been started it loops 
// The order of the child-effects determines the order of the array

public class EffectManager : MonoBehaviour
{
    [SerializeField] private float timeUntilEffectStart;

    [SerializeField] private IEffectable[] statusEffects;
    private IEffectable currentEffect;

    private void Awake()
    {
        statusEffects = GetComponentsInChildren<IEffectable>();
    }

    private void StartEffect(IEffectable effect)
    {
        currentEffect = effect;

        effect.StartEffect(timeUntilEffectStart);

        Debug.Log("Start effect");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartEffect(statusEffects[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEffect.isActive && currentEffect.isRemedied)
        {
            currentEffect.EndEffect();
        } 
    }
}
