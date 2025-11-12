using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject portal;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (portal != null)
            portal.SetActive(false);
    }

    public void WinGame()
    {
        Debug.Log("GameManager.WinGame() called!");

        // Activate the portal
        if (portal != null)
        {
            portal.SetActive(true);
            Debug.Log("Portal activated by GameManager!");
        }
        else
        {
            Debug.LogWarning("No portal assigned in GameManager!");
        }
    }
}

