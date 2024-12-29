using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

class MainMenuUI : UI
{
    public static MainMenuUI Instance;
    public Button playButton;
    public Button exitButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        playButton = document.rootVisualElement.Q("StartGameButton") as Button;
        playButton.RegisterCallback<ClickEvent>(OnStartGameClick);
        exitButton = document.rootVisualElement.Q("ExitButton") as Button;
        exitButton.RegisterCallback<ClickEvent>(OnExitButtonClick);
    }

    private void OnStartGameClick(ClickEvent evt)
    {
        GameManager.Instance.StartGame();
    }

    private void OnExitButtonClick(ClickEvent evt)
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

