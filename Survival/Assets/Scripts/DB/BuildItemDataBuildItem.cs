
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Build

}


/// <summary>
///  필드 내용은 엑셀 파일의 형식과 이름 같아야 함.
///  현재 스크립트 명 : 엑셀파일명_엔티티명(엑셀 좌측 아래 이름)
/// </summary>

[System.Serializable]

public class BuildItemDataBuildItem
{


    // 엑셀 내부 내용과 동일

    public int Id;
    public string Name;
    public string Description;
    public ItemType Type;
    public string MaxStackAmount;
    public string RequireResourceName;
    public int RequireCount;
    public string SpritePath;
    public string PrefabPath;

    // 아래는 엑셀 내용을 토대로 Sprite / prefab 추가 로직 

    private GameObject _dropPrefab;
    private Sprite _icon;

    public GameObject DropPrefab
    {
        get
        {
            if (_dropPrefab == null)
            {
                _dropPrefab = Resources.Load(PrefabPath) as GameObject;

            }

            return _dropPrefab;
        }


    }
    public Sprite Icon
    {
        get
        {
            if (_dropPrefab == null)
            {
                _icon = Resources.Load(SpritePath) as Sprite;
            }

            return _icon;
        }
    }


}
