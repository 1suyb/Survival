using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildItemDB : Singleton<BuildItemDB> 
{

    //���� ������ ��ũ��Ʈ 
    private Dictionary<int, BuildItemData_BuildItem> _itmes = new ();


    private void Start()
    {
        ItemDB();
    }


    public void ItemDB()
    {
            
        //���� ���� ReImport�� ������ Asset ��ũ��Ʈ Load, �ּҿ� ��ũ��Ʈ �̸� �ֱ� 
        var res = Resources.Load<BuildItemData>("DataSO/BuildItemData");
        var itemSO = Object.Instantiate(res);

        //BuildItemData�� List �̸�
        var entities = itemSO.BuildItemList;

        if (entities == null || entities.Count <= 0)
            return;
        
        var entitityCount = entities.Count;
        for (int i = 0; i < entitityCount; i++)
        { 
        
            var item = entities[i];

            if (_itmes.ContainsKey(item.Id))
                _itmes[item.Id] = item;
            else
                _itmes.Add(item.Id, item);
           
        }
       
    }


    //���� ������ ��ũ��Ʈ 
    public BuildItemData_BuildItem Get(int id)
    {

        if(_itmes.ContainsKey(id))
            return _itmes[id];

        return null;
    }


    public IEnumerator DbEnumerator()
    {

        return _itmes.GetEnumerator();
    }

}