using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Camera gameCamera;
    private Vector3 targetPos = new(-20, 18, -20);
    public float startGameDelay = 2f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        gameCamera = Camera.main;
        PauseGame();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        MainMenuUI.Instance.Hide();
        StartCoroutine(NewGameSequentialSteps());
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SpawnManager.Instance.Reset();
        // remove weapons
        GlobalEnemyModifiers.Reset();
        GlobalWeaponModifiers.Reset();
    }

    IEnumerator NewGameSequentialSteps()
    {
        yield return MoveCamera();
        gameCamera.transform.position = targetPos;
        SpawnManager.Instance.Run();
        GameUI.Instance.Show();
    }

    IEnumerator MoveCamera()
    {
        Vector3 startPosition = gameCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < startGameDelay)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / startGameDelay);
            gameCamera.transform.position = Vector3.Lerp(startPosition, targetPos, t);
            // Yield return null makes script wait for frame to finish
            yield return null;
        }
    }
}
