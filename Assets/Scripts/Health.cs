using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject HealsChank,
                      GameMesh;

    GameObject[] HealsBar = new GameObject[20];

    void CreateHealsBar()
    {
        Vector3 GetPositionScreen = this.transform.position;
        float DX = 0.5f;

        for(int i = 0; i < 20; i++)
        {
            HealsBar[i] = Instantiate(HealsChank) as GameObject;
            HealsBar[i].transform.position = GetPositionScreen;
            GetPositionScreen.x += DX;
        }
    }

    void RefreshHeals()
    {
        int l = 0;
        for (int i = 0; i < 20; i++) HealsBar[i].GetComponent<Chanks>().index = 0;

        if (GameMesh != null) l = GameMesh.GetComponent<GameMesh>().LifeShip();

        for (int i = 0; i < l; i++) HealsBar[i].GetComponent<Chanks>().index = 1;
        
    }

    void Start()
    {
        if (HealsChank != null) CreateHealsBar();
    }

    void Update()
    {
        if((GameMesh != null) && (HealsChank != null)) RefreshHeals();
    }
}
