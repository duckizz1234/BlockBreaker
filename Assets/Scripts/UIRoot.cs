using UnityEngine;

/// <summary>
/// UI Root class, used for storing references to UI views.
/// </summary>
public class UIRoot : MonoBehaviour
{
    [SerializeField]
    private MenuView menuView;
    public MenuView MenuView => menuView;

    [SerializeField]
    private GameView[] gameView;
    public GameView[] GameView => gameView;

    [SerializeField]
    private ReplayView replayView;
    public ReplayView ReplayView => replayView;

    private void Awake()
    {
        // Hide all other views and only show the main menu when game is loaded
        ReplayView.HideView();
        foreach (var view in gameView)
        {
            view.HideView();
        }
    }
}
