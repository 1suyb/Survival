using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{


   
    //�̸����� ���� ���� 
    [SerializeField]
    private GameObject go_Preview;

    // �÷��̾� ��ġ
    [SerializeField]
    private Transform tf_Player;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float range;

    public bool isPreview;
    public bool isdemolition;


    void Start()
    {


        go_Preview = Instantiate(go_Preview, tf_Player.position + tf_Player.forward * 10, Quaternion.identity);
   
    }


    public void SlotClick(int itemnum)
    {


        // Ŭ���� ��ġ�ؾ�.
        //Instantiate(, tf_Player.position + tf_Player.forward, Quaternion.identity);
        
      

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
        Debug.DrawRay(tf_Player.position, tf_Player.forward, Color.red);

        if (Physics.Raycast(tf_Player.position, tf_Player.forward, out hitInfo, range, layerMask))
        {

            if (hitInfo.transform != null)
            {
                Vector3 _location = hitInfo.point;
                go_Preview.transform.position = _location; 
            }
        }
       

    
    }




}
