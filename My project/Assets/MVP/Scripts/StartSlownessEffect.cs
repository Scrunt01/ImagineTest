using System.Collections;
using UnityEngine;

public class StartSlownessEffect : MonoBehaviour
{
    private EffectManager effectManager;
    // Update is called once per frame
    private void Start()
    {
        effectManager = GetComponent<EffectManager>();
        StartCoroutine(StartSlowness());
    }

    private IEnumerator StartSlowness()
    {
        while (true)
        {
            float randomInterval = Random.Range(5, 40);
            yield return new WaitForSeconds(randomInterval);
            effectManager.StartEffect("Slowness");
        }
    }
}
