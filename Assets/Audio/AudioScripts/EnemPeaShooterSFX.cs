using UnityEngine;

public class EnemPeaShooterSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance peaFootsteps;
    private FMOD.Studio.EventInstance peaAttack;
    private FMOD.Studio.EventInstance peaIdle;
    private FMOD.Studio.EventInstance peaHiss;
    private FMOD.Studio.EventInstance peaHurt;
    private FMOD.Studio.EventInstance peaDeath;

    public FMODUnity.EventReference peaFootstepsEventPath;
    public FMODUnity.EventReference peaAttackEventPath;
    public FMODUnity.EventReference peaIdleEventPath;
    public FMODUnity.EventReference peaHissEventPath;
    public FMODUnity.EventReference peaHurtEventPath;
    public FMODUnity.EventReference peaDeathEventPath;

    private void Start()
    {
        peaFootsteps = FMODUnity.RuntimeManager.CreateInstance(peaFootstepsEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaFootsteps, transform, GetComponent<Rigidbody>());
        peaAttack = FMODUnity.RuntimeManager.CreateInstance(peaAttackEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaAttack, transform, GetComponent<Rigidbody>());
        peaIdle = FMODUnity.RuntimeManager.CreateInstance(peaIdleEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaIdle, transform, GetComponent<Rigidbody>());
        peaHiss = FMODUnity.RuntimeManager.CreateInstance(peaHissEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaHiss, transform, GetComponent<Rigidbody>());
        peaHurt = FMODUnity.RuntimeManager.CreateInstance(peaHurtEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaHurt, transform, GetComponent<Rigidbody>());
        peaDeath = FMODUnity.RuntimeManager.CreateInstance(peaDeathEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaDeath, transform, GetComponent<Rigidbody>());
    }

    public void PlayPeaFS()
    {
        peaFootsteps.start();
    }

    public void PlayPeaAttack()
    {
        peaAttack.start();
    }

    public void PlayPeaIdle()
    {
        peaIdle.start();
    }

    public void StopPeaIdle()
    {
        peaIdle.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayPeaHiss()
    {
        peaHiss.start();
    }

    public void PlayPeaHurt()
    {
        peaHurt.start();
    }

    public void PlayPeaDeath()
    {
        peaDeath.start();
    }
}
