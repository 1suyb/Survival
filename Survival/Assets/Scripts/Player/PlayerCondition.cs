using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagaIbe
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }
    Condition moisture { get { return uiCondition.moisture; } }
    Condition temperature { get { return uiCondition.temperature; } }

    public float noHungerHealth;

    public event Action onTakeDamage;

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        moisture.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        if (hunger.curValue == 0)
        {
            health.Subtract(noHungerHealth * Time.deltaTime);
        }

        if (health.curValue == 0)
        {
            Die();
        }

    }

    public void Heal(float amout)
    {
        health.Add(amout);
    }

    public void Eat(float amout)
    {
        hunger.Add(amout);
    }
    public void Drink(float amout)
    {
        moisture.Add(amout);
    }

    public void UseStamina()
    {
        stamina.Subtract(stamina.passiveValue * 0.05f);
    }

    public float StaminaCheck()
    {
        return stamina.curValue;
    }

    public void Die()
    {
        Debug.Log("Die");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
