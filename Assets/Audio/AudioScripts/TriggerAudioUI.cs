using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TriggerAudioUI : MonoBehaviour
{
    public bool PlayOnAwake;
    public bool PlayOnDestory;
   
    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/Button_Pressed", gameObject);
      
    }
    private void Start()
    {
        if (PlayOnAwake)
        {
            PlayOneShot();
        }
    }

    private void OnDestroy()
    {
        if (PlayOnDestory)
        {
            PlayOneShot();
        }
    }
}
