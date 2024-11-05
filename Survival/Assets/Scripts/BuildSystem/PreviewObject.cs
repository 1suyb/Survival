using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start()
    {

        _goPreview = Instantiate(_goPreview, _tfPlayer.position + _tfPlayer.forward * 10, Quaternion.identity);
   
    }


    public void SlotClick(int itemnum)
    {


        // Ŭ���� ��ġ�ؾ�.
        //Instantiate(, _location, Quaternion.identity);



    }


    public void Cancle()
    {
        // ��ǲ�ý��ۿ��� Ű ���


        isPreview = false;
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
