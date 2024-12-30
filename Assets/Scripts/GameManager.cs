using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private VRInputManager vrInputManagerPrefab;
    [SerializeField]
    private KeyboardMouseInputManager keyboardMouseInputManager;
    [SerializeField]
    private bool useKeyboardMouse = false;

    public IInputProvider InputProvider { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

#if !UNITY_EDITOR
		useKeyboardMouse = false;
#endif
        if (useKeyboardMouse)
        {
            InputProvider = Instantiate(keyboardMouseInputManager, transform);
        }
        else
        {
            InputProvider = Instantiate(vrInputManagerPrefab, transform);
        }

    }

    public void GoToJoystickScene()
    {
        StartCoroutine(LoadScene("Game_Joystick", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<JoystickSceneController>();
            sceneController.Initialize(this);
        }));
    }

    public void GoToClimbingScene()
    {
        throw new NotImplementedException();
    }

    public void GoToTeleportScene()
    {
        throw new NotImplementedException();
    }

    public void GoToMenuScene()
    {
        StartCoroutine(LoadScene("Menu", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<MenuSceneController>();
            sceneController.Initialize(this);
        }));
    }

    private IEnumerator LoadScene(string sceneName, Action sceneLoadedCallback)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
        sceneLoadedCallback?.Invoke();
    }

    public static GameManager BootstrapFromEditor()
    {
#if UNITY_EDITOR
        var gameManagerPrefab = Resources.Load<GameManager>("GameManager");
        var gameManager = Instantiate(gameManagerPrefab);
        return gameManager;
#else
        throw new NotImplementedException();
#endif
    }
}

