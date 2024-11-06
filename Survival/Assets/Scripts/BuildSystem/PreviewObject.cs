using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class PreviewObject : MonoBehaviour
{


    //미리보기 담을 변수 
    [SerializeField]
    private GameObject _goPreview;


    private InstallableItem _item;


    // 플레이어 위치
    [SerializeField]
    private Transform _tfPlayer;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask _layerMask;

   

    [SerializeField]
    private float range;

    public bool isPreview;

    private Camera _camera;


    public void Start()
    {
        _camera = Camera.main;

        //Todo : 이 부분을 아이템 사용 버튼 클릭 시 활성화 되도록 해야 함.

        _goPreview = Instantiate(_goPreview, _tfPlayer.position + _tfPlayer.forward, Quaternion.identity);
        _goPreview.GetComponent<GameObject>();
        _item = _goPreview.GetComponent<InstallableItem>();
    }


    public void OnCancle(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isPreview)
        {

            isPreview = false;
            Destroy(_goPreview);
            _goPreview = null;

        }
    }

    public void Oninstall(InputAction.CallbackContext context)
    {

        //아이템이 설치 가능할 때
        if (context.phase == InputActionPhase.Started && _goPreview.GetComponent<InstallableItem>().isBuildable())
        {

            
            isPreview = false;
            _item.isInstall = true;
            _item.RestoreOriginalMaterials();
           _goPreview =null;


        }
    }


    void Update()
    {

        if(isPreview)
        PreviewPostionUpdate();


    }


    private void PreviewPostionUpdate()
    {


        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hitInfo, range, _layerMask))
        {

            if (hitInfo.transform != null)
            {
               Vector3 _location = hitInfo.point;
                _goPreview.transform.position = _location; 
            }
        }
       

    
    }




}
