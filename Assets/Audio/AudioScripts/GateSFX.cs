using UnityEngine;

public class GateSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance gateOpen;
    private FMOD.Studio.EventInstance gateClose;
    private FMOD.Studio.EventInstance gateLoop;
    private FMOD.Studio.EventInstance gateImpact;

    public FMODUnity.EventReference gateOpenEventPath;
    public FMODUnity.EventReference gateCloseEventPath;
    public FMODUnity.EventReference gateLoopEventPath;
    public FMODUnity.EventReference gateImpactEventPath;

    private void Start()
    {
        gateOpen = FMODUnity.RuntimeManager.CreateInstance(gateOpenEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(gateOpen, transform, GetComponent<Rigidbody>());
        gateClose = FMODUnity.RuntimeManager.CreateInstance(gateCloseEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(gateClose, transform, GetComponent<Rigidbody>());
        gateLoop = FMODUnity.RuntimeManager.CreateInstance(gateLoopEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(gateLoop, transform, GetComponent<Rigidbody>());
        gateImpact = FMODUnity.RuntimeManager.CreateInstance(gateImpactEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(gateImpact, transform, GetComponent<Rigidbody>());
    }

    public void PlayGateOpen()
    {
        gateOpen.start();
    }

    public void PlayGateClose()
    {
        gateClose.start();
    }

    public void PlayGateLoop()
    {
        gateLoop.start();
    }

    public void StopGateLoop()
    {
        gateLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayGateImpact()
    {
        gateImpact.start();
    }

}
