using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // reference to prefab for new audio sources
    [SerializeField] private AudioSource sourcePrefab;

    // singleton for easy access 
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        // setup singleton so that the new one will be destroyed
        // meaning sounds will keep playing on the old one
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool isClipPlaying(AudioClip clip)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.clip == clip)
            {
                Debug.Log($"The audioClip {source.clip.name} is already playing!");
                return true;
            }
        }
        return false;
    }

    public void PlayClip(AudioClip clip)
    {
        //if (clip == null)
        //{
        //    Debug.LogError($"There is no audioclip, please put one in the inspector!");
        //    return;
        //}

        // create a new audio source
        AudioSource source = Instantiate(sourcePrefab);

        // set its variables
        source.clip = clip;
        source.volume = 1f;

        // play the sound
        source.Play();

        // ensure it stays alive, say when we reload RN
        DontDestroyOnLoad(source);

        // add to list
        audioSources.Add(source);

        StartCoroutine(WaitForAudioClipEnd(source));
    }

    public void PlayClipLooped(AudioClip clip)
    {
        //if (clip == null)
        //{
        //    Debug.LogError($"There is no audioclip, please put one in the inspector!");
        //    return;
        //}


        StartCoroutine(LoopAfterAudioClipEnd(clip));
    }



    public void PauseAllAudio()
    {
        foreach(AudioSource source in audioSources)
        {
            source.Pause();
        }
    }

    public void UnPauseAllAudio()
    {
        foreach (AudioSource source in audioSources)
        {
            source.Play();
        }
    }

    public void PlayRandomClip(AudioClip[] clips)
    {
        // pick a random clip and pass it as the to be played clip
        PlayClip(clips[Random.Range(0, clips.Length)]);
    }


    private IEnumerator WaitForAudioClipEnd(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);

        // remove the source from the list
        audioSources.Remove(source);

        // destroy GO after play time
        Destroy(source.gameObject);
    }
    private IEnumerator LoopAfterAudioClipEnd(AudioClip clip)
    {
        while (true)
        {
            // create a new audio source
            AudioSource source = Instantiate(sourcePrefab);

            // set its variables
            source.clip = clip;
            source.volume = 1f;

            // play the sound
            source.Play();

            // ensure it stays alive, say when we reload RN
            DontDestroyOnLoad(source);

            // add to list
            audioSources.Add(source);


            yield return new WaitForSeconds(source.clip.length);

            // remove the source from the list
            audioSources.Remove(source);

            // destroy GO after play time
            Destroy(source.gameObject);
        }
    }

}
