using UnityEngine;

public class PeaSMakerSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance peaPitchUp;
    private FMOD.Studio.EventInstance peaOnStart;
    private FMOD.Studio.EventInstance peaShootLoop;
    private FMOD.Studio.EventInstance peaPops;
    private FMOD.Studio.EventInstance peaPopFinal;
    private FMOD.Studio.EventInstance peaOnStop;
    private FMOD.Studio.EventInstance peaPitchDown;
    private FMOD.Studio.EventInstance peaSMakerComplete;

    public FMODUnity.EventReference peaPitchUpEventPath;
    public FMODUnity.EventReference peaOnStartEventPath;
    public FMODUnity.EventReference peaShootLoopEventPath;
    public FMODUnity.EventReference peaPopsEventPath;
    public FMODUnity.EventReference peaPopFinalEventPath;
    public FMODUnity.EventReference peaOnStopEventPath;
    public FMODUnity.EventReference peaPitchDownEventPath;
    public FMODUnity.EventReference peaSMakerCompleteEventPath;

    void Start()
    {
        peaPitchUp = FMODUnity.RuntimeManager.CreateInstance(peaPitchUpEventPath);
        peaOnStart = FMODUnity.RuntimeManager.CreateInstance(peaOnStartEventPath);
        peaShootLoop = FMODUnity.RuntimeManager.CreateInstance(peaShootLoopEventPath);
        peaPops = FMODUnity.RuntimeManager.CreateInstance(peaPopsEventPath);
        peaPopFinal = FMODUnity.RuntimeManager.CreateInstance(peaPopFinalEventPath);
        peaOnStop = FMODUnity.RuntimeManager.CreateInstance(peaOnStopEventPath);
        peaPitchDown = FMODUnity.RuntimeManager.CreateInstance(peaPitchDownEventPath);
        peaSMakerComplete = FMODUnity.RuntimeManager.CreateInstance(peaSMakerCompleteEventPath);

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaPitchUp, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaOnStart, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaShootLoop, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaPops, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaPopFinal, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaOnStop, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaPitchDown, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaSMakerComplete, transform, GetComponent<Rigidbody>());

    }

    public void PlayPeaPitchUp()
    {
        peaPitchUp.start();
    }

    public void PlayPeaOnStart()
    {
        peaOnStart.start();
    }

    public void PlayPeaShootLoop()
    {
        peaShootLoop.start(); 
    }

    public void StopPeaShootLoop()
    {
        peaShootLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayPeaPops()
    {
        peaPops.start();
    }

    public void StopPeaPops()
    {
        peaPops.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayPeaPopFinal()
    {
        peaPopFinal.start();
    }
    public void PlayPeaOnStop()
    {
        peaOnStop.start();
    }
    public void PlayPeaPitchDown()
    {
        peaPitchDown.start();
    }

    public void PlayPeaSMakerComplete()
    {
        peaSMakerComplete.start();
    }

}
