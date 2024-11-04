using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CharacterManger").AddComponent<PlayerManager>();
            }
            return instance;
        }
    }
    private Player player;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}
