using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public float curHealth { get; private set; }

    [SerializeField] private Slider healthBarSlider;

    [SerializeField] private Gradient healthGradient;

    [SerializeField] private Image healthBarFill;

    [SerializeField] private float maxHealth;
    [SerializeField] private float drainTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBarSlider.maxValue = maxHealth;

        ChangeFillColour();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealth(float amount)
    {
        curHealth += amount;

        StartCoroutine(MoveHealthBar());
        ChangeFillColour();

        if (curHealth <= 0f) { Die(); }
        else if (curHealth > maxHealth) { curHealth = maxHealth; }
    }

    public void Die()
    {
        Debug.Log("Player Died");
    }

    private void ChangeFillColour()
    {
        healthBarFill.color = healthGradient.Evaluate(curHealth / maxHealth);
    }

    private IEnumerator MoveHealthBar()
    {
        float sliderFill = healthBarSlider.value;
        float elapsedTime = 0f;

        while (elapsedTime < drainTime)
        {
            elapsedTime += Time.deltaTime;

            healthBarSlider.value = Mathf.Lerp(sliderFill, curHealth, elapsedTime / drainTime);

            yield return null;
        }
    }
}
