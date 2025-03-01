using UnityEngine;

public class PepperBlastSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance pepperBlast;

    public FMODUnity.EventReference pepperBlastEventPath;


    void Start()
    {
        pepperBlast = FMODUnity.RuntimeManager.CreateInstance(pepperBlastEventPath);

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pepperBlast, transform, GetComponent<Rigidbody>());
    }

    public void PlayPepperBlast()
    {
        pepperBlast.start();
    }

}
