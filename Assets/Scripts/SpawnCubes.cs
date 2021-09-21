using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    public enum TileStates { 
        Mine,
        Flag,
        Empty,
    }

    public int XSize = 10;
    public int YSize = 10;
    public int chanceOfMines = 5;

    private int[,] map;

    public GameObject GridCube;
    public GameObject SafeCube;
    public GameObject Mine;

    private void Start()
    {
        map = new int[XSize, YSize];

        for (int i = 0; i < XSize; i++)
        {
            for (int I = 0; I < YSize; I++)
            {
                map[i, I] = Random.Range(1, chanceOfMines);
            }
        }
        Debug.Log("Created Grid");

        SpawnMines();
        SpawnBlueBlocks();
        SpawnGreenblocks();
    }

    void SpawnMines()
    {
        for (int i = 0; i < XSize; i++)
        {
            for (int I = 0; I < YSize; I++)
            {
                if(map[i, I] == 1)
                {
                    Instantiate(Mine, new Vector3(i, 0, I), Quaternion.identity);
                }
            }
        }
    }

    void SpawnNumberBlocks()
    {
        for (int i = 0; i < XSize; i++)
        {
            for (int I = 0; I < YSize; I++)
            {
                if(map[i, I] == 1)
                {
                    for (int X = -1; X <= 1; X++)
                    {
                        for (int Y = -1; Y <= 1; Y++)
                        {
                            if(i + X >= 0 && i + X < XSize && I + Y >= 0 && I + Y < YSize)
                            {
                                if (X != 0 || Y != 0)
                                {
                                    if (map[i + X, I + Y] != 1)
                                    {
                                        Instantiate(SafeCube, new Vector3(i + X, 0, I + Y), Quaternion.identity);
                                        map[i + X, I + Y] = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void SpawnBlueBlocks()
    {

        for (int i = 0; i < XSize; i++)
        {
            for (int I = 0; I < YSize; I++)
            {
                if (map[i, I] != 1)
                {
                    int minesAround = 0;

                    for (int X = -1; X <= 1; X++)
                    {
                        for (int Y = -1; Y <= 1; Y++)
                        {
                            if (i + X >= 0 && i + X < XSize && I + Y >= 0 && I + Y < YSize)
                            {
                                if (X != 0 || Y != 0)
                                {
                                    if(map[i + X, I + Y] == 1)
                                    {
                                        minesAround += 1;
                                    }
                                }
                            }
                        }
                    }

                    if(minesAround != 0)
                    {
                        var BlueCube = Instantiate(SafeCube, new Vector3(i, 0, I), Quaternion.identity);
                        var renderer = BlueCube.GetComponent<Renderer>();
                        renderer.material.color = new Color(.4f - .1f * minesAround, .4f - .1f * minesAround, 1);
                        map[i, I] = 0;
                    }
                }
            }
        }
    }

    void SpawnGreenblocks()
    {
        for (int i = 0; i < XSize; i++)
        {
            for (int I = 0; I < YSize; I++)
            {
                if(map[i, I] > 1)
                {
                    Instantiate(GridCube, new Vector3(i, 0, I), Quaternion.identity);
                }
            }
        }
    }
}
