using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition hunger;
    public Condition stamina;
    public Condition moisture;
    public Condition temperature;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.Player.condition.condition = this;
    }
}
