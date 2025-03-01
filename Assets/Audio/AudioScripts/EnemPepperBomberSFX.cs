using UnityEngine;

public class EnemPepperBomberSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance pepperFootsteps;
    private FMOD.Studio.EventInstance pepperAggroed;
    private FMOD.Studio.EventInstance pepperIdle;
    private FMOD.Studio.EventInstance pepperExplosion;
    private FMOD.Studio.EventInstance pepperHurt;
    private FMOD.Studio.EventInstance pepperDeath;

    public FMODUnity.EventReference pepperFootstepsEventPath;
    public FMODUnity.EventReference pepperAggroedEventPath;
    public FMODUnity.EventReference pepperIdleEventPath;
    public FMODUnity.EventReference pepperExplosionEventPath;
    public FMODUnity.EventReference pepperHurtEventPath;
    public FMODUnity.EventReference pepperDeathEventPath;


    private void Start()
    {
        pepperFootsteps = FMODUnity.RuntimeManager.CreateInstance(pepperFootstepsEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pepperFootsteps, transform, GetComponent<Rigidbody>());
        pepperAggroed = FMODUnity.RuntimeManager.CreateInstance(pepperAggroedEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pepperAggroed, transform, GetComponent<Rigidbody>());
        pepperIdle = FMODUnity.RuntimeManager.CreateInstance(pepperIdleEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pepperIdle, transform, GetComponent<Rigidbody>());
        pepperExplosion = FMODUnity.RuntimeManager.CreateInstance(pepperExplosionEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pepperExplosion, transform, GetComponent<Rigidbody>());
        pepperHurt = FMODUnity.RuntimeManager.CreateInstance(pepperHurtEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pepperHurt, transform, GetComponent<Rigidbody>());
        pepperDeath = FMODUnity.RuntimeManager.CreateInstance(pepperDeathEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pepperDeath, transform, GetComponent<Rigidbody>());
    }

    public void PlayPepperFS()
    {
        pepperFootsteps.start();
    }

    public void PlayPepperAggroed()
    {
        pepperAggroed.start();
    }

    public void StopPepperAggroed()
    {
        pepperAggroed.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayPepperIdle()
    {
        pepperIdle.start();
    }

    public void StopPepperIdle()
    {
        pepperIdle.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayPepperExplosion()
    {
        pepperExplosion.start();
    }

    public void PlayPepperHurt()
    {
        pepperHurt.start();
    }

    public void PlayPepperDeath()
    {
        pepperDeath.start();
    }


}
