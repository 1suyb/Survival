using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallableItem : MonoBehaviour
{
    // 충돌한 오브젝트의 컬라이더
    private List<Collider> colliderList = new List<Collider> ();

    [SerializeField]
    private int layerGround; // 지상 레이어
   
    private MeshRenderer[] meshRenderers;
    private Material[] originalMaterials;


    private Color Red = new Color(1.0f, 0.6f, 0.6f);
    private Color Green = new Color(0f, 1f, 0f);

    public bool isInstall = false;

    private void Start()
    {

        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        SaveOriginalMaterials();


        Debug.Log(colliderList.Count);
    }

    private void SaveOriginalMaterials()
    {
        originalMaterials = new Material[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            // 원본 material을 복제하여 저장
            originalMaterials[i] = new Material(meshRenderers[i].material);
        }
    }

    public void RestoreOriginalMaterials()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material = originalMaterials[i];
        }
    }

    public void Update()
    {
        if (!isInstall)
        {

            if (colliderList.Count > 0)
                InstallPosible(Red);
            else
                InstallPosible(Green);

        }


    }

 

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer != layerGround)
        colliderList.Add(other);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround)
            colliderList.Remove(other);

    }


    public void InstallPosible(Color color)
    {
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = color ;

    }

    public bool isBuildable()
    {

     
        return colliderList.Count == 0;
    
    }
  
}
