using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreviewObject : MonoBehaviour
{


    //�̸����� ���� ���� 
    [SerializeField]
    private GameObject _goPreview;

    // �÷��̾� ��ġ
    [SerializeField]
    private Transform _tfPlayer;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask _layerMask;

    //������ ��ġ ��ġ
    [SerializeField]
    Vector3 _location;

    [SerializeField]
    private float range;

    public bool isPreview;


    public void OnCancle(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {

            isPreview = false;
            _goPreview = null;

        }
    }


    void Update()
    {

        PreviewPostionUpdate();


    }


  


    private void PreviewPostionUpdate()
    {

   
        Debug.Log(hitInfo.transform);
        Debug.DrawRay(_tfPlayer.position, _tfPlayer.forward, Color.red);

        if (Physics.Raycast(_tfPlayer.position, _tfPlayer.forward, out hitInfo, range, _layerMask))
        {

            if (hitInfo.transform != null)
            {
                _location = hitInfo.point;
                _goPreview.transform.position = _location; 
            }
        }
       

    
    }




}
