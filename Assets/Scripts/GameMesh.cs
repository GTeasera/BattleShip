using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMesh : MonoBehaviour
{
    public GameObject GameMain;

    public GameObject eliters, enums, emesh, estate;

    public GameObject MapDestination;

    public bool HideShip = false;

    GameObject[] liters;
    GameObject[] nums;
    public GameObject[,] mesh;

    int time = 2300,
        deltatime = 0;

    int MeshRange = 10;
    public int[] ShipCount = {0, 4, 3, 2, 1 };

    bool CountShip()
    {
        int count = 0;
        foreach (int ship in ShipCount) count += ship;
        if (count != 0) return true;
        return false;
    }
    public void ClearMesh()
    {
        ShipCount = new int[] { 0, 4, 3, 2, 1 };
        ListShip.Clear();
        for (int y = 0; y < MeshRange; y++)
        {
            for (int x = 0; x < MeshRange; x++)
            {
                mesh[x, y].GetComponent<Chanks>().index = 0;
            }
        }
    }

    public void EnterRandomShip()
    {
        ClearMesh();
        int SelectShip = 4;
        int x, y;
        int Direction;

        while(CountShip())
        {
            x = Random.RandomRange(0, 10);
            y = Random.RandomRange(0, 10);
            Direction = Random.RandomRange(0, 2);

            if (EnterDeck(SelectShip, Direction, x, y))
            {
                ShipCount[SelectShip]--;
                if(ShipCount[SelectShip]==0)
                {
                    SelectShip--;
                }
            }
        }
    }


    public struct TestCoord
    {
        public int x, y;
    }

    public struct Ship
    {
        public TestCoord[] ShipCoord;
    }

    public void CopyMesh()
    {
        if(MapDestination != null)
        {
            for(int y = 0; y < MeshRange; y++)
            {
                for(int x = 0; x < MeshRange; x++)
                {
                    MapDestination.GetComponent<GameMesh>().mesh[x, y].GetComponent<Chanks>().index = mesh[x, y].GetComponent<Chanks>().index;
                }
            }
            MapDestination.GetComponent<GameMesh>().ListShip.Clear();

            MapDestination.GetComponent<GameMesh>().ListShip.AddRange(ListShip);
        }
    }

    public List<Ship> ListShip = new List<Ship>();
    void MeshGeneration()
    {
        Vector3 StartPose = transform.position;

        float XX = StartPose.x + 1;
        float YY = StartPose.y - 1;

        liters = new GameObject[MeshRange];
        nums = new GameObject[MeshRange];

        for (int TextScreen = 0; TextScreen < MeshRange; TextScreen++)
        {
            liters[TextScreen] = Instantiate(eliters);
            liters[TextScreen].transform.position = new Vector3(XX, StartPose.y, StartPose.z);
            liters[TextScreen].GetComponent<Chanks>().index = TextScreen;
            XX++;

            nums[TextScreen] = Instantiate(enums);
            nums[TextScreen].transform.position = new Vector3(StartPose.x, YY, StartPose.z);
            nums[TextScreen].GetComponent<Chanks>().index = TextScreen;
            YY--;
        }

        XX = StartPose.x + 1;
        YY = StartPose.y - 1;

        mesh = new GameObject[MeshRange, MeshRange];

        for (int y = 0; y < MeshRange; y++)
        {
            for(int x = 0; x < MeshRange; x++)
            {
                mesh[x,y] = Instantiate(emesh);
                mesh[x, y].GetComponent<Chanks>().index = 0;
                mesh[x, y].GetComponent<Chanks>().HideChank = HideShip;

                mesh[x, y].transform.position = new Vector3(XX, YY, StartPose.z);

                if(HideShip)
                mesh[x, y].GetComponent<ClickMesh>().WhoPerent = this.gameObject;
                
                mesh[x, y].GetComponent<ClickMesh>().CoordX = x;
                mesh[x, y].GetComponent<ClickMesh>().CoordY = y;



                XX++;
            }
            XX = StartPose.x + 1;
            YY--;
        }
    }

    bool EnterDeck(int x, int y)
    {
        if((x > -1) && (y > -1) && (x < 10) && (y < 10))
        {
            int[] XX = new int[9], YY = new int[9];
            XX[0] = x + 1;  YY[0] = y + 1;
            XX[1] = x;      YY[1] = y + 1;
            XX[2] = x - 1;  YY[2] = y + 1;
            XX[3] = x + 1;  YY[3] = y;
            XX[4] = x;      YY[4] = y;
            XX[5] = x - 1;  YY[5] = y;
            XX[6] = x + 1;  YY[6] = y - 1;
            XX[7] = x;      YY[7] = y - 1;
            XX[8] = x - 1;  YY[8] = y - 1;
            for (int i = 0; i < 9; i++)
            {
                if ((XX[i] > -1) && (YY[i] > -1) && (XX[i] < 10) && (YY[i] < 10))
                {
                    if (mesh[XX[i], YY[i]].GetComponent<Chanks>().index != 0) return false;
                }
            }
            return true;
        }
        return false;
    }

    TestCoord[] TestEnterShipDirect(int ShipType, int XD, int YD, int x, int y)
    {
        TestCoord[] ResultCoord = new TestCoord[ShipType];
        for(int p = 0; p < ShipType; p++)
        {
            if (EnterDeck(x, y))
            {
                ResultCoord[p].x = x;
                ResultCoord[p].y = y;
            }
            else
                return null;
            x += XD;
            y += YD;
        }
        return ResultCoord;
    }

    TestCoord[] TestEnterShip(int ShipType, int Direction, int x, int y)
    {
        TestCoord[] ResultCoord = new TestCoord[ShipType];
        if (EnterDeck(x, y))
        {
            switch (Direction)
            {
                case 0:
                    ResultCoord = TestEnterShipDirect(ShipType, 1, 0, x, y);
                    if (ResultCoord == null) ResultCoord = TestEnterShipDirect(ShipType, -1, 0, x, y);
                    break;
                case 1:
                    ResultCoord = TestEnterShipDirect(ShipType, 0, -1, x, y);
                    if (ResultCoord == null) ResultCoord = TestEnterShipDirect(ShipType, 0, 1, x, y);
                    break;
            }
            return ResultCoord;
        }
        return null;
    }

    bool EnterDeck(int ShipType, int Direction,int x, int y)
    {
        TestCoord[] p = TestEnterShip(ShipType, Direction, x, y);
        if (p != null)
        {
            foreach(TestCoord t in p)
            {
                mesh[t.x, t.y].GetComponent<Chanks>().index = 1;
            }
            Ship Deck;
            Deck.ShipCoord = p;
            ListShip.Add(Deck);

            return true;
        }
        return false;
    }

    void Start()
    {
        MeshGeneration();
        if (HideShip) EnterRandomShip();
    }

    void Update()
    {
        deltatime++;
        if(deltatime > time)
        {
            if (estate != null) estate.GetComponent<Chanks>().index = 0;
            deltatime = 0;
        }
    }

    public void WhoClick(int x, int y)
    {
        // if(EnterDeck(x,y)) mesh[x, y].GetComponent<Chanks>().index = 1;
        // EnterDeck(4, 0, x, y);
        // Shoot(x,y);
        if (GameMain != null) GameMain.GetComponent<MainGame>().UserClick(x, y);
        
    }

    public bool Shoot(int x, int y)
    {
        if(estate != null)  estate.GetComponent<Chanks>().index = 0;

        int MeshSelect = mesh[x, y].GetComponent<Chanks>().index;
        bool result = false;
        switch(MeshSelect)
        {
            case 0:
                mesh[x, y].GetComponent<Chanks>().index = 2;
                result = false;
                estate.GetComponent<Chanks>().index = 3;
                break;
            case 1:
                mesh[x, y].GetComponent<Chanks>().index = 3;
                result = true;
                
                if(TestShoot(x,y))
                {
                    if (estate != null) estate.GetComponent<Chanks>().index = 1;
                }
                else
                {
                    if (estate != null) estate.GetComponent<Chanks>().index = 2;
                }
                
                break;
        }
        return result;
    }

    bool TestShoot(int x, int y)
    {
        bool result = false;

        foreach(Ship Test in ListShip)
        {
            foreach (TestCoord Paluba in Test.ShipCoord)
            {
                if((Paluba.x == x) && (Paluba.y == y))
                {
                    int CountKill = 0;
                    foreach(TestCoord KillPaluba in Test.ShipCoord)
                    {
                        int TestBlock = mesh[KillPaluba.x, KillPaluba.y].GetComponent<Chanks>().index;
                        if (TestBlock == 3) CountKill++;
                    }
                    if (CountKill == Test.ShipCoord.Length)
                        result = true;
                    else
                        result = false;

                    return result;
                }
            }

        }

        return result;
    }

    public int LifeShip()
    {
        int countLife = 0;

        foreach(Ship Test in ListShip)
        {
            foreach(TestCoord Paluba in Test.ShipCoord)
            {
                int TestBlock = mesh[Paluba.x, Paluba.y].GetComponent<Chanks>().index;
                if (TestBlock == 1) countLife++;
            }
        }



        return countLife;
    }

}
