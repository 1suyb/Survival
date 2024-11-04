using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanageMaterial : MonoBehaviour
{

    private List<Collider> colliderList = new List<Collider> ();

    private Material green;
    private Material red;





    void Update()
    {

        ChangeColor();
    
    
    }

    private void SetColor(Material mat)
    {
        foreach (Transform tf_child in this.transform)
        { }
        
    }


    private void ChangeColor()
    {

        if (colliderList.Count > 0)
        { 
         

        }

        else 
        { 
        
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {

        colliderList.Add(other);


    }


    private void OnTriggerExit(Collider other)
    {

        colliderList.Remove(other);
    }


  
}
