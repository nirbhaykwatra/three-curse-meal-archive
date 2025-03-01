using UnityEngine;
using FMOD.Studio;

public class EnemBroccoliSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance brocFootsteps;
    private FMOD.Studio.EventInstance brocAttack;
    private FMOD.Studio.EventInstance brocIdle;
    private FMOD.Studio.EventInstance brocGrunt;
    private FMOD.Studio.EventInstance brocHurt;
    private FMOD.Studio.EventInstance brocDeath;

    public FMODUnity.EventReference brocFootstepsEventPath;
    public FMODUnity.EventReference brocAttackEventPath;
    public FMODUnity.EventReference brocIdleEventPath;
    public FMODUnity.EventReference brocGruntEventPath;
    public FMODUnity.EventReference brocHurtEventPath;
    public FMODUnity.EventReference brocDeathEventPath;

    private void Start()
    {
        brocFootsteps = FMODUnity.RuntimeManager.CreateInstance(brocFootstepsEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(brocFootsteps, transform, GetComponent<Rigidbody>());
        brocAttack = FMODUnity.RuntimeManager.CreateInstance(brocAttackEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(brocAttack, transform, GetComponent<Rigidbody>());
        brocIdle = FMODUnity.RuntimeManager.CreateInstance(brocIdleEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(brocIdle, transform, GetComponent<Rigidbody>());
        brocGrunt = FMODUnity.RuntimeManager.CreateInstance(brocGruntEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(brocGrunt, transform, GetComponent<Rigidbody>());
        brocHurt = FMODUnity.RuntimeManager.CreateInstance(brocHurtEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(brocHurt, transform, GetComponent<Rigidbody>());
        brocDeath = FMODUnity.RuntimeManager.CreateInstance(brocDeathEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(brocDeath, transform, GetComponent<Rigidbody>());
    }

    public void PlayBrocFS()
    {
        brocFootsteps.start();
    }

    public void PlayBrocAttack()
    {
        brocAttack.start();
    }
    
    public void PlayBrocIdle()
    {
        brocIdle.start();
    }

    public void StopBrocIdle()
    {
        brocIdle.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void StopBrocGrunt()
    {
        brocGrunt.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayBrocGrunt()
    {
        brocGrunt.start();
    }

    public void PlayBrocHurt()
    {
        brocHurt.start();
    }

    public void PlayBrocDeath()
    {
        brocDeath.start();
    }

}
