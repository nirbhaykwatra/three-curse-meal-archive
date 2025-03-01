using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    //[SerializeField, FMODUnity.EventReference] private string eventPath; // This is the path to the event in FMOD. The "EventReference" attribute created the cool FMOD menu in the Inspector.

    private FMOD.Studio.EventInstance playerFootsteps; // This stores the event instance of footsteps.
    private FMOD.Studio.EventInstance playerDash;   //"" of dashes
    private FMOD.Studio.EventInstance playerLand;   //"" of landings
    private FMOD.Studio.EventInstance playerLightAttack1; //"" of lightAttack1
    private FMOD.Studio.EventInstance playerLightAttack2;
    private FMOD.Studio.EventInstance playerLightAttack3;

    public FMODUnity.EventReference playerFootstepsEventPath;
    public FMODUnity.EventReference playerDashEventPath;
    public FMODUnity.EventReference playerLandEventPath;
    public FMODUnity.EventReference playerLightAttack1EventPath;
    public FMODUnity.EventReference playerLightAttack2EventPath;
    public FMODUnity.EventReference playerLightAttack3EventPath;

    private void Start()
    {
        playerFootsteps = FMODUnity.RuntimeManager.CreateInstance(playerFootstepsEventPath); // Create an instance of the event. Footsteps
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerFootsteps, transform, GetComponent<Rigidbody>()); // Attach the instance of the event to the gameObject's transform and rigidbody.
        playerDash = FMODUnity.RuntimeManager.CreateInstance(playerDashEventPath); // Create an instance of the event. Dash
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerDash, transform, GetComponent<Rigidbody>()); // Attach the instance of the event to the gameObject's transform and rigidbody.
        playerLand = FMODUnity.RuntimeManager.CreateInstance(playerLandEventPath); // Create an instance of the event. Land
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerLand, transform, GetComponent<Rigidbody>()); // Attach the instance of the event to the gameObject's transform and rigidbody.
        
        playerLightAttack1 = FMODUnity.RuntimeManager.CreateInstance(playerLightAttack1EventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerLightAttack1, transform, GetComponent<Rigidbody>());
        playerLightAttack2 = FMODUnity.RuntimeManager.CreateInstance(playerLightAttack2EventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerLightAttack2, transform, GetComponent<Rigidbody>());
        playerLightAttack3 = FMODUnity.RuntimeManager.CreateInstance(playerLightAttack3EventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerLightAttack3, transform, GetComponent<Rigidbody>());
    }

    public void PlayPlayerFootsteps()
    {
        playerFootsteps.start(); // Play the sounds inside the event in FMOD.
    }

    public void PlayPlayerDash()
    {
        playerDash.start();
    }

    public void PlayPlayerLand()
    {
        playerLand.start();
    }

    public void PlayPlayerLightAttack1()
    {
        playerLightAttack1.start();
    }

    public void PlayPlayerLightAttack2()
    {
        playerLightAttack2.start();
    }

    public void PlayPlayerLightAttack3()
    {
        playerLightAttack3.start();
    }

}
