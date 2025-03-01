using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class MixerParameterSetting : FloatSetting
{
    [Header("Mixer")]

    //FMOD parameter selector in inspector
    [FMODUnity.ParamRef] public string fmodParameter = "";


    public override void SetValue(float newValue)
    {
        base.SetValue(newValue);
        SetFmodMixerParameter(newValue);

    }
    public void SetFmodMixerParameter(float fmodValue)
    {

        //_audioMixer.SetFloat(_parameterName, newValue);
        if (!string.IsNullOrEmpty(fmodParameter))
        {
            FMOD.RESULT result = FMODUnity.RuntimeManager.StudioSystem.setParameterByName(fmodParameter, fmodValue);

            if (result != FMOD.RESULT.OK)
            {
                Debug.LogError(string.Format(("[FMOD] StudioGlobalParameterTrigger failed to set parameter {0} : result = {1}"), fmodParameter, result));
            }
        }
    }
}
