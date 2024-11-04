using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{

    [SerializeField]
    private GameObject go_Preview;
    [SerializeField]
    private LayerMask layerMask;

    // �÷��̾� ��ġ �ʵ� �� �ʿ�
    private Transform tf_Player;

    private RaycastHit hitInfo;

    [SerializeField]
    private float range;


    void Start()
    {

        tf_Player = Camera.main.transform;

        go_Preview = Instantiate(go_Preview, tf_Player.position + tf_Player.forward * 10, Quaternion.identity);
   
    }


    public void SlotClick(int itemnum)
    {


        //�κ��丮���� ������ ����
        //Instantiate(, tf_Player.position + tf_Player.forward, Quaternion.identity);

        //�׽�Ʈ �ڵ�
        Instantiate(go_Preview, tf_Player.position + tf_Player.forward, Quaternion.identity);

    }


    void Update()
    {

        PreviewPostionUpdate();


    }


    private void PreviewPostionUpdate()
    {

        tf_Player = Camera.main.transform;

        Debug.Log(hitInfo.transform);
        Debug.DrawRay(tf_Player.position, go_Preview.transform.position, Color.red);

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
