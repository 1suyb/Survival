using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTest : MonoBehaviour
{
    [SerializeField] private AreaRespawnSystem _respawnSystem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _respawnSystem.Spawn();
        }
    }
}
