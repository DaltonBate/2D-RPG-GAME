using UnityEngine;


public class SceneWinCondition : MonoBehaviour
{
    public enum Mode
    {
        Timer,
        KillCount
    }

    [Header("General")]
    [SerializeField] private Mode mode = Mode.Timer;

    [Header("Timer Mode")]
    [SerializeField] private float winSeconds = 30f;

    [Header("KillCount Mode")]
    [SerializeField] private int targetKills = 10;

    private float startTime;
    private int currentKills;
    private bool hasWon;

    private void OnEnable()
    {
        ResetState();
        if (mode == Mode.KillCount)
            Enemy.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        if (mode == Mode.KillCount)
            Enemy.OnEnemyKilled -= HandleEnemyKilled;
    }

    private void ResetState()
    {
        startTime = Time.realtimeSinceStartup;
        currentKills = 0;
        hasWon = false;
    }

    private void Update()
    {
        if (hasWon) return;

        switch (mode)
        {
            case Mode.Timer:
                if (Time.realtimeSinceStartup - startTime >= winSeconds)
                {
                    TriggerWin($"Timer reached {winSeconds}s");
                }
                break;
            case Mode.KillCount:
                // kill handling is event-driven; we keep this branch for future checks or visuals
                break;
        }
    }

    private void HandleEnemyKilled(Enemy e)
    {
        if (hasWon) return;

        currentKills++;
        Debug.Log($"SceneWinCondition: Enemy killed. currentKills={currentKills}/{targetKills}");

        if (currentKills >= targetKills)
            TriggerWin($"Reached kill target {targetKills}");
    }

    private void TriggerWin(string reason)
    {
        hasWon = true;
        Debug.Log($"SceneWinCondition: Win triggered ({reason})");

        // Prefer UI.HandleWinCondition (it calls GameManager internally).
        if (UI.Instance != null)
        {
            UI.Instance.HandleWinCondition();
            return;
        }

        // Fallback: if UI isn't present, call GameManager directly.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.WinGame();
            return;
        }

        Debug.LogWarning("SceneWinCondition: No UI or GameManager instance available to handle win.");
    }
}