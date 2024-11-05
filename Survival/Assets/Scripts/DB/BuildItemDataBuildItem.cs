
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Build

}


/// <summary>
///  �ʵ� ������ ���� ������ ���İ� �̸� ���ƾ� ��.
///  ���� ��ũ��Ʈ �� : �������ϸ�_��ƼƼ��(���� ���� �Ʒ� �̸�)
/// </summary>

[System.Serializable]

public class BuildItemDataBuildItem
{


    // ���� ���� ����� ����

    public int Id;
    public string Name;
    public string Description;
    public ItemType Type;
    public string MaxStackAmount;
    public string RequireResourceName;
    public int RequireCount;
    public string SpritePath;
    public string PrefabPath;

    // �Ʒ��� ���� ������ ���� Sprite / prefab �߰� ���� 

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
