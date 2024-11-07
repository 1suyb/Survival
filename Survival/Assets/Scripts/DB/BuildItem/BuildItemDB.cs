using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildItemDB : Singleton<BuildItemDB> 
{

    //엑셀 데이터 스크립트 
    private Dictionary<int, BuildItemData> _itmes = new ();


    private void Start()
    {
        ItemDB();
    }

    public void ItemDB()
    {
            
        //엑셀 파일 ReImport시 생성된 Asset 스크립트 Load, 주소에 스크립트 이름 넣기 
        var res = Resources.Load<BuildItemSheet>("DataSO/BuildItemSheet");
        var itemSO = Object.Instantiate(res);

        //BuildItemData에 List 이름
        var entities = itemSO.BuildItemData;

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
    //엑셀 데이터 스크립트 
    public BuildItemData Get(int id)
    {
        if(_itmes.ContainsKey(id))
            return _itmes[id];

        return null;
    }
    public Dictionary<int, BuildItemData> DbEnumerator()
    {
        return _itmes;
    }

}
