using UnityEngine;
using TMPro;

public class BlockController : MonoBehaviour
{
    public int health = 3;
    public TextMeshPro healthText;
    public delegate void LogMessageDelegate(string message);
    public static event LogMessageDelegate OnLogMessage;

    private void Start()
    {
        health = Random.Range(ConstantsLoader.Instance.minBlockHealth, ConstantsLoader.Instance.maxBlockHealth);
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = health.ToString();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantsLoader.ProjectileTag))
        {
            if (ConstantsLoader.Instance.showJuice)
            {
                GetComponent<Animator>().SetBool(ConstantsLoader.BlockBounceBool, true);
            }

            // Decrement Health
            health--;
            if (health <= 0)
            {
                // No health left so destroy block
                OnLogMessage?.Invoke("Block Destroyed");
                GameManager.Instance.RemoveBlockFromList(gameObject);
                Camera.main.GetComponent<CameraShake>().Shake();
                Destroy(gameObject);
            }
            UpdateHealthText();
        }
    }

    public void ResetTriggerFlag()
    {
        GetComponent<Animator>().SetBool(ConstantsLoader.BlockBounceBool, false);
    }
}
