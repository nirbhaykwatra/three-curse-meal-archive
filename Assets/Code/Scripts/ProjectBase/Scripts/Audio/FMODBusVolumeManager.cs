using UnityEngine;

/// <summary>
/// This class can manage an FMOD Bus' volume.
/// </summary>
namespace FMODUnity
{
    public class FMODBusVolumeManager : MonoBehaviour
    {
        private static string PlayerPrefsPrefix = "FMOD_Bus_";

        [SerializeField] private bool subscribeToSlider = true;
        [SerializeField] private string busPath = "Master";
    
        private FMOD.Studio.Bus busRef = default;
        private UnityEngine.UI.Slider slider = null;

        private void Awake()
        {
            busRef = FMODUnity.RuntimeManager.GetBus($"bus:/{busPath}");
        }
        private void Start()
        {
            if (subscribeToSlider)
            {
                if (TryGetComponent<UnityEngine.UI.Slider>(out slider))
                {
                    slider.onValueChanged.AddListener(SetBusVolume);
                    slider.value = PlayerPrefs.GetFloat($"{PlayerPrefsPrefix}{busPath}", 1.0f);
                }
            }
        }

        public void SetBusVolume(float volume)
        {
            busRef.setVolume(volume);
            PlayerPrefs.SetFloat($"{PlayerPrefsPrefix}{busPath}", volume);
        }
    }
}