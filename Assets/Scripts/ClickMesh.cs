using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMesh : MonoBehaviour
{
    public GameObject WhoPerent = null;
    public int CoordX, CoordY;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(WhoPerent != null)
        {
            WhoPerent.GetComponent<GameMesh>().WhoClick(CoordX, CoordY);
        }
    }    
}
