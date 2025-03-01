using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class SpecialAttacks : MonoBehaviour
{
    [FoldoutGroup("Unlockables")]
    [SerializeField] private CharacterAbilitiesUnlock _characterAbilitiesUnlockParams;
    [FoldoutGroup("Pea-S-Maker")]
    [SerializeField] private float _shootCooldown;
    [FoldoutGroup("Pea-S-Maker")]
    [SerializeField] private float _duration;
    [FoldoutGroup("Pea-S-Maker")]
    [SerializeField] private Transform _muzzle;
    [FoldoutGroup("Pea-S-Maker")]
    [SerializeField] private Transform _muzzle1;
    [FoldoutGroup("Pea-S-Maker")]
    [SerializeField] private Transform _muzzle2;
    [FoldoutGroup("Pea-S-Maker")]
    [SerializeField] private GameObject _projectilePrefab;
    [FoldoutGroup("Resources Serialized Object")]
    [SerializeField] private Resources _resources;
    [SerializeField] private bool _resourcesEnabled = true;
    public BroccoliRing _broccoliParams;

    private GameObject _broccoliRing;
    private CharacterMovement3D _characterMovement;
    private Animator _animator;
    private Button _peaSMaker;
    private Button _pepperBlast;
    private Button _broccoliRingButton;
    private bool _brocRingInUse = false;
    private bool _peaSMakerInUse = false;

    public bool ResourcesEnabled 
    {
        get => _resourcesEnabled;
        set => _resourcesEnabled = value;
    }

    #region SFX

    [FoldoutGroup("SFX Variables")]
    private FMOD.Studio.EventInstance broccoliRing; //broccoli ring sfx
    [FoldoutGroup("SFX Variables")]
    public FMODUnity.EventReference broccoliRingEventPath;
    [FoldoutGroup("SFX Variables")]
    private FMOD.Studio.EventInstance peaPitchUp; //PeaSMaker sfx
    [FoldoutGroup("SFX Variables")]
    private FMOD.Studio.EventInstance peaOnStart;
    [FoldoutGroup("SFX Variables")]
    private FMOD.Studio.EventInstance peaShootLoop; //peaSmaker sfx
    [FoldoutGroup("SFX Variables")]
    private FMOD.Studio.EventInstance peaPops;
    [FoldoutGroup("SFX Variables")]
    private FMOD.Studio.EventInstance peaPopFinal;
    [FoldoutGroup("SFX Variables")]
    private FMOD.Studio.EventInstance peaOnStop;
    [FoldoutGroup("SFX Variables")]
    private FMOD.Studio.EventInstance peaPitchDown;
    [FoldoutGroup("SFX Variables")]
    public FMODUnity.EventReference peaPitchUpEventPath;
    [FoldoutGroup("SFX Variables")]
    public FMODUnity.EventReference peaOnStartEventPath;
    [FoldoutGroup("SFX Variables")]
    public FMODUnity.EventReference peaShootLoopEventPath;
    [FoldoutGroup("SFX Variables")]
    public FMODUnity.EventReference peaPopsEventPath;
    [FoldoutGroup("SFX Variables")]
    public FMODUnity.EventReference peaPopFinalEventPath;
    [FoldoutGroup("SFX Variables")]
    public FMODUnity.EventReference peaOnStopEventPath;
    [FoldoutGroup("SFX Variables")]
    public FMODUnity.EventReference peaPitchDownEventPath;

    #endregion

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement3D>();
        _animator = GetComponent<Animator>();
        
        
        

        broccoliRing = FMODUnity.RuntimeManager.CreateInstance(broccoliRingEventPath); //broccoli ring sfx
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(broccoliRing, transform, GetComponent<Rigidbody>());

        
        peaShootLoop = FMODUnity.RuntimeManager.CreateInstance(peaShootLoopEventPath); //PeaSmaker sfx
        peaPops = FMODUnity.RuntimeManager.CreateInstance(peaPopsEventPath);
        peaPopFinal = FMODUnity.RuntimeManager.CreateInstance(peaPopFinalEventPath);
        peaOnStop = FMODUnity.RuntimeManager.CreateInstance(peaOnStopEventPath);
        peaPitchDown = FMODUnity.RuntimeManager.CreateInstance(peaPitchDownEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaShootLoop, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaPops, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaPopFinal, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaOnStop, transform, GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(peaPitchDown, transform, GetComponent<Rigidbody>());
    }

    public void OnPeaSMaker()
    {
        _peaSMaker = GameObject.Find("PeaSMaker")?.GetComponent<Button>();
        if (_resourcesEnabled)
        {
            if (!_peaSMakerInUse && _resources.Pea >= 2 && _resources.Pepper > 0 && _characterAbilitiesUnlockParams.peaSMakerUnlocked)
            {
                _peaSMakerInUse = true;
                _animator.SetTrigger("PeaSMaker");
                _resources.Pea -= 2;
                _resources.Pepper -= 1;
            }
        }
        else
        {
            _peaSMakerInUse = true;
            _animator.SetTrigger("PeaSMaker");
        }
    }
    public void OnPepperBlast()
    {
        _pepperBlast = GameObject.Find("PepperBlast")?.GetComponent<Button>();
        if (_resourcesEnabled)
        {
            if (_resources.Pepper >= 2 && _resources.Broccoli > 0 && _characterAbilitiesUnlockParams.pepperBlastUnlocked)
            {
                _animator.SetTrigger("PepperBlast");
                _resources.Pepper -= 2;
                _resources.Broccoli -= 1;
                //_characterMovement.PepperBlast();
            }
        }
        else
        {
            _animator.SetTrigger("PepperBlast");
        }
    }
    public void ShootPeaSMaker()
    {
        StartCoroutine(PeasCoroutine());
        //StopCoroutine(PeasCoroutine());
    }
    public IEnumerator PeasCoroutine()
    {
        peaShootLoop.start();
        peaPops.start();

        float totalTime = 0f;
        while (totalTime <= _duration)
        {
            yield return new WaitForSeconds(_shootCooldown);
            //nextShootTime = Time.timeSinceLevelLoad + _shootCooldown;
            Instantiate(_projectilePrefab, _muzzle.transform.position, _muzzle.transform.rotation, _muzzle);
            Instantiate(_projectilePrefab, _muzzle1.transform.position, _muzzle1.transform.rotation, _muzzle1);
            Instantiate(_projectilePrefab, _muzzle2.transform.position, _muzzle2.transform.rotation, _muzzle2);
            totalTime = totalTime + Time.deltaTime * 10;
        }
        _animator.SetTrigger("StopPeaSMaker");
        _characterMovement.MoveSpeedMultiplier = 1;
        _peaSMakerInUse = false;
        yield return null;

        peaShootLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); //peaSmaker sfx stop
        peaPops.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        peaPopFinal.start();
        peaPitchDown.start();
        peaOnStop.start();
    }

    //broccoli stuff
    public void OnBroccoliRing()
    {
        _broccoliRingButton = GameObject.Find("BroccoliRing")?.GetComponent<Button>();
        if (_resourcesEnabled)
        {
            if (_brocRingInUse == false && _resources.Broccoli >= 4 && _resources.Pea > 0 && _characterAbilitiesUnlockParams.broccoliRingUnlocked)
            {
                _resources.Broccoli -= 4;
                _resources.Pea -= 1;
                _animator.SetTrigger("BroccoliRing");
            }
        }
        else
        {
            _animator.SetTrigger("BroccoliRing");
        }
    }
    public void BroccoliRing()
    {
        if (_brocRingInUse == false)
        {
            //spawn and call coroutine
            _broccoliRing = Instantiate(_broccoliParams._broccoliPrefab, transform.position, Quaternion.identity);
            StartCoroutine(RotateBroccoli());

            broccoliRing.start(); //broccoli ring sfx
        }
    }
    
    public IEnumerator RotateBroccoli()
    {
            _brocRingInUse = true;
            float totalTime = 0f;
            while (totalTime <= _broccoliParams._duration)
            {
                yield return new WaitForEndOfFrame();
                _broccoliRing.transform.position = transform.position;
                _broccoliRing.transform.Rotate(_broccoliRing.transform.up, _broccoliParams._speed * Time.deltaTime);
                totalTime = totalTime + Time.deltaTime;
            }
            _brocRingInUse = false;
            Destroy(_broccoliRing);
        broccoliRing.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        yield return null;
    }
}
