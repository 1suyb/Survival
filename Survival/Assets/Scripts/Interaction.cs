using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    private float _checkRrate = 0.05f;
    private float _lastCheckTime;
    public float maxCheckDistance;
    [SerializeField] private LayerMask _layerMask;

    public GameObject curInteractGameObject;
    private IInteractable interactable;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _lastCheckTime > _checkRrate)
        {
            _lastCheckTime = Time.time;
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, _layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    interactable = hit.collider.GetComponent<IInteractable>();
                    interactable.ShowPrompt();
                }
            }
            else
            {
                if(curInteractGameObject != null)
                {
                    interactable.ClosePrompt();
                }
				curInteractGameObject = null;
                interactable = null;
			}
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && interactable != null)
        {
            interactable.Interact();
            curInteractGameObject = null;
            interactable = null;
        }
    }
}
