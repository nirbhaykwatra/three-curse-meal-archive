using UnityEngine;

public class ChestSFX : MonoBehaviour
{
    //[SerializeField, FMODUnity.EventReference] private string eventPath; // This is the path to the event in FMOD. The "EventReference" attribute created the cool FMOD menu in the Inspector.

    //private FMOD.Studio.EventInstance chestClose;
    public FMODUnity.EventReference chestOpenEventPath;
   // public FMODUnity.EventReference chestCloseEventPath;


    public void PlayChestOpen()
    {
        FMODUnity.RuntimeManager.PlayOneShot(chestOpenEventPath, transform.position);
    }

}
