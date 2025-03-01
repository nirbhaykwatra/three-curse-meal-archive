using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Image _blackScreen;
    [SerializeField] private IntEvent _currentCheckpoint;
    [SerializeField] private CharacterAbilitiesUnlock _characterAbilities;
    [SerializeField] private Resources _resources;
    private void Awake()
    {
        Time.timeScale = 1;
        _animator = GameObject.Find("DurnanMesh").GetComponent<Animator>();
    }
    public void LoadMainScene()
    {
        StartCoroutine("PreLoading");
    }
    public void LoadScene(string sceneName)
    {
        _characterAbilities.LockAll();
        _resources.ResetResources();
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #endif
    }

    public IEnumerator PreLoading()
    {
        _animator.SetTrigger("StandUp");
        while(_blackScreen.color.a < 1)
        {
            yield return new WaitForEndOfFrame();
            _blackScreen.color = new Color(_blackScreen.color.r, _blackScreen.color.g, _blackScreen.color.b, _blackScreen.color.a + Time.deltaTime/2);
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("LVL_Gameplay");
    }
}