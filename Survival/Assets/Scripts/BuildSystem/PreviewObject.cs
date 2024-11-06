using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class PreviewObject : MonoBehaviour
{


    //�̸����� ���� ���� 
    [SerializeField]
    private GameObject _goPreview;


    private InstallableItem _item;


    // �÷��̾� ��ġ
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

        //Todo : �� �κ��� ������ ��� ��ư Ŭ�� �� Ȱ��ȭ �ǵ��� �ؾ� ��.

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

        //�������� ��ġ ������ ��
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
