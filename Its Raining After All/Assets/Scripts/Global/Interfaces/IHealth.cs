using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    void UpdateHealth(float amount);
    void Die();
}
