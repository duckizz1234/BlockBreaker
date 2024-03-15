using UnityEngine;
using TMPro;

public class BlockController : MonoBehaviour
{
    public int health = 3;
    public TextMeshPro healthText;

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
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                Debug.Log("A block was destroyed");
            }
            UpdateHealthText();
        }
    }
}
