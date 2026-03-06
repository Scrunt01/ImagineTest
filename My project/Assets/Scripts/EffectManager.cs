using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    [SerializeField] private bool isEffect0Active = false;

    [SerializeField] private float timeUntilEffectStart;

    [SerializeField] private GameObject effect0Remedy;


    private IEffectable effect0;

    private void Awake()
    {
        effect0 = GetComponentInChildren<IEffectable>();
    }
   

    private void StartEffect(IEffectable effect)
    {
        effect.StartEffect(timeUntilEffectStart);
        isEffect0Active = true;
        Debug.Log("Start effect 0");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartEffect(effect0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEffect0Active)
        {
            float distanceToRemedy = Vector3.Distance(player.transform.position, effect0Remedy.transform.position);
            
            if (distanceToRemedy < 5)
            {
                effect0.EndEffect();
                Debug.Log("End the effect 0");
            }
        } 
    }
}
