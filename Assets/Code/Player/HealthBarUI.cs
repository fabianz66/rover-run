using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Image fillImage;

    private static readonly Color ColorFull = Color.green;
    private static readonly Color ColorMid  = Color.yellow;
    private static readonly Color ColorLow  = Color.red;

    void Start()
    {
        healthSlider.maxValue = PlayerHealth.MaxHealth;
        healthSlider.value    = PlayerHealth.MaxHealth;
        playerHealth.OnHealthChanged += UpdateBar;
    }

    void OnDestroy()
    {
        playerHealth.OnHealthChanged -= UpdateBar;
    }

    private void UpdateBar(float current, float max)
    {
        healthSlider.value = current;

        float ratio = current / max;
        if (ratio > 0.6f)
            fillImage.color = ColorFull;
        else if (ratio > 0.3f)
            fillImage.color = ColorMid;
        else
            fillImage.color = ColorLow;
    }
}
