using UnityEngine;

public class PlayerSpecialAttacksSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance peaPitchUp; //PeaSMaker sfx
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

    private FMOD.Studio.EventInstance pepperBlast; //pepper blast sfx

    public FMODUnity.EventReference pepperBlastEventPath;

    private FMOD.Studio.EventInstance broccoliRing; //broccoli ring sfx
    public FMODUnity.EventReference broccoliRingEventPath;

    void Start()
    {
        peaPitchUp = FMODUnity.RuntimeManager.CreateInstance(peaPitchUpEventPath); //peaSMaker
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

        pepperBlast = FMODUnity.RuntimeManager.CreateInstance(pepperBlastEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pepperBlast, transform, GetComponent<Rigidbody>());

        broccoliRing = FMODUnity.RuntimeManager.CreateInstance(broccoliRingEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(broccoliRing, transform, GetComponent<Rigidbody>());

    }

    //PeaSMaker
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

    public void PlayPepperBlast()
    {
        pepperBlast.start();
    }   

    public void PlayBroccoliRing()
    {
        broccoliRing.start();
    }

}
