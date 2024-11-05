using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanageMaterial : MonoBehaviour
{

    private List<Collider> colliderList = new List<Collider> ();


    private MeshRenderer[] meshRenderers;
    private Material[] originalMaterials;


    private Color Red = new Color(1.0f, 0.6f, 0.6f);
    private Color Green = new Color(0f, 1f, 0f);


    private void Start()
    {

        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        SaveOriginalMaterials();

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

    private void OnTriggerEnter(Collider other)
    {

        
        InstallPosible(Red);

      
    }


    public void InstallPosible(Color color)
    {
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = color ;

    }


  
}
