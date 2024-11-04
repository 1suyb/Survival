using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{


   
    //미리보기 담을 변수 
    [SerializeField]
    private GameObject go_Preview;

    // 플레이어 위치
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


        // 클릭시 설치해야.
        //Instantiate(, tf_Player.position + tf_Player.forward, Quaternion.identity);
        
      

    }


    public void Cancle()
    {
        // 인풋시스템에서 키 취소


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
