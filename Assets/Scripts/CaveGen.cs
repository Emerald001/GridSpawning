using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveGen : MonoBehaviour
{
    public int XSize = 10;
    public int YSize = 10;

    private int[,] map;

    public GameObject SafeCube;

    private void Start()
    {
        map = new int[XSize, YSize];

        for (int Xcor = 0; Xcor<XSize; Xcor++)
        {
            for (int Ycor = 0; Ycor<YSize; Ycor++)
            {
                map[Xcor, Ycor] = 1;
            }
        }

        StartHole();
        CreateHole();
        SpawnGrid();
    }

    void SpawnGrid() 
    {
        for (int Xcor = 0; Xcor < XSize; Xcor++)
        {
            for (int Ycor = 0; Ycor < YSize; Ycor++)
            {
                if(map[Xcor, Ycor] == 1)
                {
                    Instantiate(SafeCube, new Vector3(Xcor, 0, Ycor), Quaternion.identity);
                }
            }
        }
    }

    void StartHole()
    {
        int xPositon = Mathf.RoundToInt(Random.Range(XSize / 3, XSize / 3 * 2));
        int yPositon = Mathf.RoundToInt(Random.Range(YSize / 3, YSize / 3 * 2));

        map[xPositon, yPositon] = 0;
    }

    void CreateHole()
    {
        for (int Xcor = 0; Xcor < XSize; Xcor++)
        {
            for (int Ycor = 0; Ycor < YSize; Ycor++)
            {
                if (map[Xcor, Ycor] == 0)
                {
                    for (int XNeighbor = -1; XNeighbor <= 1; XNeighbor++)
                    {
                        for (int YNeighbor = -1; YNeighbor <= 1; YNeighbor++)
                        {
                            if (Xcor + XNeighbor >= 0 && Xcor + XNeighbor < XSize && Ycor + YNeighbor >= 0 && Ycor + YNeighbor < YSize)
                            {
                                if (XNeighbor != 0 || YNeighbor != 0)
                                {
                                    if(Random.Range(0, 5) >= 3)
                                    {
                                        map[Xcor + XNeighbor, Ycor + YNeighbor] = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
