using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void WinGame()
    {
        UI.Instance?.EnableGameWonUI();
    }
}