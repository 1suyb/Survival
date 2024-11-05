using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreviewObject : MonoBehaviour
{


    //미리보기 담을 변수 
    [SerializeField]
    private GameObject _goPreview;

    // 플레이어 위치
    [SerializeField]
    private Transform _tfPlayer;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask _layerMask;

    //아이템 설치 위치
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
