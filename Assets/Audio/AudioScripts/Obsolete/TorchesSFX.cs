using UnityEngine;

public class TorchesSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance ambTorch;

    public FMODUnity.EventReference ambTorchEventPath;
    void Start()
    {
        ambTorch = FMODUnity.RuntimeManager.CreateInstance(ambTorchEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(ambTorch, transform, GetComponent<Rigidbody>());
    }

    public void PlayAmbTorch()
    {
        ambTorch.start();
    }

}
