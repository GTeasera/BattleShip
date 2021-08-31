using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject MyMap;

    void OnGUI()
    {
        Rect LocationButton;
        LocationButton = new Rect(new Vector2(10, 10), new Vector2(200, 40));
        if (GUI.Button(LocationButton, "Generate")) MyMap.GetComponent<GameMesh>().EnterRandomShip();
        LocationButton = new Rect(new Vector2(10, 50), new Vector2(200, 40));
        if (GUI.Button(LocationButton, "Copy")) MyMap.GetComponent<GameMesh>().CopyMesh();
    }
}
