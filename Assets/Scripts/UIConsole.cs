using UnityEngine;
using TMPro;

/// <summary>
/// Displays debug log in the console
/// </summary>
public class UIConsole : MonoBehaviour
{
    /// <summary>
    /// Reference to the UI text field
    /// </summary>
    public TextMeshProUGUI consoleText;
    /// <summary>
    /// Max number of messages to display. This will be updated by Constants.json
    /// </summary>
    private int maxMessages = 5;

    private void OnEnable()
    {
        TurretController.OnLogMessage += LogMessage;
        ProjectileStateMachine.OnLogMessage += LogMessage;
        BlockController.OnLogMessage += LogMessage;
    }

    private void OnDisable()
    {
        TurretController.OnLogMessage -= LogMessage;
        ProjectileStateMachine.OnLogMessage -= LogMessage;
        BlockController.OnLogMessage -= LogMessage;
    }

    private void Start()
    {
        maxMessages = ConstantsLoader.Instance.maxMessages;
    }

    /// <summary>
    /// Triggered by events to display messages
    /// </summary>
    /// <param name="message">Message to display</param>
    private void LogMessage(string message)
    {
        consoleText.text += "\n" + message;
        TrimMessages();
    }

    /// <summary>
    /// Ensure the list of messages being display doesn't exceed the max number
    /// </summary>
    private void TrimMessages()
    {
        string[] lines = consoleText.text.Split('\n');
        if (lines.Length > maxMessages)
        {
            consoleText.text = string.Join("\n", lines, lines.Length - maxMessages, maxMessages);
        }
    }
}