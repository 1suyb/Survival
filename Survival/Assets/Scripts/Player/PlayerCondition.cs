using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }
    Condition moisture { get { return uiCondition.moisture; } }
    Condition temperature { get { return uiCondition.temperature; } }

    private float _noHungerHealth = 1.0f;

    private float _noMoistureHealth = 2.0f;

    public event Action onTakeDamage;

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        moisture.Subtract(moisture.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        if (hunger.curValue == 0)
        {
            health.Subtract(_noHungerHealth * Time.deltaTime);
        }

        if (moisture.curValue == 0)
        {
            health.Subtract(_noMoistureHealth * Time.deltaTime);
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

    public bool UseStamina(float useStamina)
    {
        if (stamina.curValue < useStamina)
        {
            stamina.Subtract(useStamina);
            return true;
        }
        return false;
    }

    public float StaminaCheck()
    {
        return stamina.curValue;
    }

    public void Die()
    {
         PlayerManager.Instance.Player.controller.Die();
    }

	public void TakeDamage(int damage)
	{
		health.Subtract(damage);
		onTakeDamage?.Invoke();
	}
}
