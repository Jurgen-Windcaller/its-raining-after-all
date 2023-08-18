using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public float curHealth { get; private set; }

    [SerializeField] private GameObject healthBarPrefab;

    [SerializeField] private Canvas worldspaceCanvas;

    [SerializeField] private Gradient healthGradient;

    [SerializeField] private Vector3 healthBarOffset = new Vector3(0f, 1.5f, 0f);

    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private float drainTime = 0.25f;

    private Slider healthBarSlider;

    private Image healthBarFill;

    private GameObject healthBarInstance;

    // Start is called before the first frame update
    void Start()
    {
        healthBarInstance = Instantiate(healthBarPrefab);

        healthBarInstance.SetActive(false); 
        healthBarInstance.transform.SetParent(worldspaceCanvas.transform);

        healthBarSlider = healthBarInstance.GetComponentInChildren<Slider>();
        healthBarFill = healthBarInstance.GetComponentInChildren<Image>();

        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;

        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBarInstance == null) { return; }
        HealthBarFollow();
    }

    public void UpdateHealth(float amount)
    {
        if (curHealth == maxHealth && amount < 0f) { healthBarInstance.SetActive(true); }

        curHealth += amount;

        StartCoroutine(MoveHealthBar());
        ChangeFillColour();

        if (curHealth <= 0f) { Die(); }
        else if (curHealth > maxHealth) { curHealth = maxHealth; }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " has died");

        Destroy(healthBarInstance);
        Destroy(gameObject);
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

    private void HealthBarFollow()
    {
        healthBarInstance.transform.position = transform.position + healthBarOffset;
    }
}
