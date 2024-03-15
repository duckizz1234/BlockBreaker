using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIConsole : MonoBehaviour
{
    public TextMeshProUGUI consoleText;
    public int maxMessages = 5;



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

    private void LogMessage(string message)
    {
        consoleText.text += "\n" + message;
        TrimMessages();
    }

    private void TrimMessages()
    {
        string[] lines = consoleText.text.Split('\n');
        if (lines.Length > maxMessages)
        {
            consoleText.text = string.Join("\n", lines, lines.Length - maxMessages, maxMessages);
        }
    }
}