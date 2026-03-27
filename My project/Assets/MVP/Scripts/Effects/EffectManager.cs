using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public interface IEffectable
{
    void StartEffect();
    void EndEffect();
    bool isActive { get; }
    bool isRemedied { get; }

    string effectName { get; }

}

// Once an effect has been started it loops 
// The order of the child-effects determines the order of the array

public class EffectManager : MonoBehaviour
{
    private IEffectable[] statusEffects;
    private List<IEffectable> currentEffects;

    private void Awake()
    {
        currentEffects = new List<IEffectable>();
        statusEffects = GetComponentsInChildren<IEffectable>();
        Debug.Log($"There have been {statusEffects.Length} effects detected");
    }

    private void StartEffect(IEffectable effect)
    {
        currentEffects.Add(effect);

        effect.StartEffect();

        Debug.Log("Start effect " + effect.effectName);
    }

    public void StartEffect(string effectName)
    {
        foreach (var effect in statusEffects)
        {
            if (effectName == effect.effectName)
            {
                currentEffects.Add(effect);

                effect.StartEffect();

                Debug.Log("Start effect " + effect.effectName);
            }
        }
    }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // Start the vignette effect
            StartEffect("Nausea");
            StartEffect("Vignette");
        }

        // Update is called once per frame
        void Update()
        {
        foreach (var effect in currentEffects)
        {
            if (effect.isActive && effect.isRemedied)
            {
                effect.EndEffect();
            }
        }
        }
    
}
