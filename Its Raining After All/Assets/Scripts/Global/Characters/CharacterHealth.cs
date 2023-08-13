using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Color[] healthBarColors = new Color[4];

    [SerializeField] private int maxHealth;

    private Slider healthBarSlider;

    public int curHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider = healthBar.GetComponentInChildren<Slider>();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSlider.value = curHealth;

        ChangeFillColour();
    }

    public void Hurt(int dmg)
    {
        curHealth -= dmg;
        
        if (curHealth <= 0) { Die(); }
    }

    public void Heal(int healing)
    {
        curHealth += healing;

        if (curHealth > maxHealth) { curHealth = maxHealth; }
    }

    private void Die()
    {
        Debug.Log("Player Died");
    }

    private void ChangeFillColour()
    {
        if (curHealth > Mathf.CeilToInt(maxHealth / 2))
        {
            healthBarFill.color = healthBarColors[0];
        }
        else if (curHealth > Mathf.CeilToInt(maxHealth / 3))
        {
            healthBarFill.color = healthBarColors[1];
        }
        else if (curHealth > Mathf.CeilToInt(maxHealth / 4))
        {
            healthBarFill.color = healthBarColors[2];
        }
        else if (curHealth > Mathf.CeilToInt(maxHealth / 5))
        {
            healthBarFill.color = healthBarColors[3];
        }
    }
}
