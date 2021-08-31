using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public int GameMode = 0;
    public GameObject PlayerMesh, ComputerMesh, Player;

    bool WhoseTurn = true;

    void OnGUI()
    {
        float CentrScreenX = Screen.width / 2;
        float CentrScrenY = Screen.height / 3;
        Rect LocationButton;
        GameMesh PlayerMeshContol = PlayerMesh.GetComponent<GameMesh>();
        Camera cam = GetComponent<Camera>();

        switch (GameMode)
        {
            case 0:
                cam.orthographicSize = 8;
                this.transform.position = new Vector3(-11, 0, -10);
                LocationButton = new Rect(new Vector2(CentrScreenX - 150, CentrScrenY - 50), new Vector2(300, 200));

                GUI.Box(LocationButton, "");

                LocationButton = new Rect(new Vector2(CentrScreenX - 20, CentrScrenY - 30), new Vector2(200, 30));
                GUI.Label(LocationButton, "MENU");

                LocationButton = new Rect(new Vector2(CentrScreenX - 100, CentrScrenY), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "START"))
                {
                    GameMode = 1;
                }
                LocationButton = new Rect(new Vector2(CentrScreenX - 100, CentrScrenY + 40), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "QUIT"))
                {
                    Application.Quit();
                }
                break;
            case 1:
                cam.orthographicSize = 8;
                this.transform.position = new Vector3(30, 0, -10);
                LocationButton = new Rect(new Vector2(CentrScreenX - 375, 0), new Vector2(300, 50));
                GUI.Box(LocationButton, "");
                LocationButton = new Rect(new Vector2(CentrScreenX - 375, 0), new Vector2(200, 30));
                GUI.Label(LocationButton, "Hangar");

                LocationButton = new Rect(new Vector2(CentrScreenX + 375, 0), new Vector2(50, 15));
                if (GUI.Button(LocationButton, "Menu"))
                {
                    PlayerMeshContol.ClearMesh();
                    GameMode = 0;
                }

                LocationButton = new Rect(new Vector2(CentrScreenX - 375, 20), new Vector2(100, 30));
                if (GUI.Button(LocationButton, "Random Locate"))
                    PlayerMeshContol.EnterRandomShip();

                if (PlayerMeshContol.LifeShip() == 20)
                {
                    LocationButton = new Rect(new Vector2(CentrScreenX - 250, 20), new Vector2(100, 30));
                    if (GUI.Button(LocationButton, "Fight"))
                    {
                        GameMode = 3;
                        PlayerMesh.GetComponent<GameMesh>().CopyMesh();

                        ComputerMesh.GetComponent<GameMesh>().EnterRandomShip();
                    }

                }
                break;
            case 3:
                this.transform.position = new Vector3(30, -28, -10);
                cam.orthographicSize = 10;

                break;

            case 4:
                this.transform.position = new Vector3(122, 0, -10);
                break;

            case 5:
                this.transform.position = new Vector3(71, 0, -10);
                break;
        }

    }


    void OpenAI()
    {
        if(!WhoseTurn)
        {
            int ShotX = Random.RandomRange(0, 9);
            int ShotY = Random.RandomRange(0, 9);

            WhoseTurn = !Player.GetComponent<GameMesh>().Shoot(ShotX, ShotY);
        }
    }
    
    public void UserClick(int x, int y)
    {
        Debug.Log("Click");
        if(WhoseTurn)
        {
            WhoseTurn = ComputerMesh.GetComponent<GameMesh>().Shoot(x, y);
        }
    }

    public void WhoWin()
    {
        int PC_Ship = ComputerMesh.GetComponent<GameMesh>().LifeShip();
        int Player_Ship = Player.GetComponent<GameMesh>().LifeShip();

        if (PC_Ship == 0) GameMode = 4;
        if (Player_Ship == 0) GameMode = 5;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(GameMode == 3)
        {
            WhoWin();
            OpenAI();
        }
    }
}
