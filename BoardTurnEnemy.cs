using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardTurnEnemy : MonoBehaviour
{

    public static BoardTurnEnemy Instance { get; set; }

    private void Start()
    {
        BoardTurnEnemy.Instance = this;
        this.turns = new List<GameObject>();
    }

    private GameObject GetTurnObject(int t)
    {
        int index = t;
        if (t > 6)
        {
            if (t == 7)
            {
                t = 12;
            }
            else if (t == 8)
            {
                t = 13;
            }
            else if (t == 9)
            {
                t = 14;
            }
            else if (t == 10)
            {
                t = 15;
            }
            else if (t == 11)
            {
                t = 16;
            }
            else if (t == 12)
            {
                t = 23;
            }
            else if (t == 13)
            {
                t = 24;
            }
            else if (t == 14)
            {
                t = 25;
            }
            else if (t == 15)
            {
                t = 26;
            }
            else if (t == 16)
            {
                t = 34;
            }
            else if (t == 17)
            {
                t = 35;
            }
            else if (t == 18)
            {
                t = 36;
            }
            else if (t == 19)
            {
                t = 45;
            }
            else if (t == 20)
            {
                t = 46;
            }
            else if (t == 21)
            {
                t = 56;
            }
        }
        string name = t.ToString();
        if (this.cubHelp.isWhite)
        {
            name += "_white(Clone)";
        }
        else
        {
            name += "_black(Clone)";
        }
        GameObject gameObject = this.turns.Find((GameObject g) => name.Equals(g.name) && !g.activeSelf);
        if (gameObject == null && this.cubHelp.isWhite)
        {
            gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PrefabsWhite[index]);
            this.turns.Add(gameObject);
        }
        else if (gameObject == null && !this.cubHelp.isWhite)
        {
            gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PrefabsBlack[index]);
            this.turns.Add(gameObject);
        }
        return gameObject;
    }

    public void TurnAllMoves(cub c, cub[,] Cubs)
    {
        this.cubHelp = c;
        bool[,] array = c.PossibleMove();
        int num = c.Turns;
        float rY = c.rY;
        float rZ = c.rZ;
        int currentX = c.CurrentX;
        int currentY = c.CurrentY;
        Vector3 vector = new Vector3(0f, 0f, 0f);
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (array[i, j])
                {
                    float z;
                    if (Cubs[i, j] != null && Cubs[i, j].isWhite != c.isWhite)
                    {
                        z = 1f;
                    }
                    else
                    {
                        z = 0.025f;
                    }
                    if (num == 1)
                    {
                        if (c.name == "kingWhite(Clone)" || c.name == "kingBlack(Clone)")
                        {
                            this.setHelpturns(0, i, j, z, vector);
                        }
                        else if ((rY == 0f && rZ == 180f) || (rY == 180f && rZ == 0f))
                        {
                            if (i == currentX && j > currentY)
                            {
                                this.setHelpturns(5, i, j, z, vector);
                            }
                            if (i == currentX && j < currentY)
                            {
                                this.setHelpturns(2, i, j, z, vector);
                            }
                            if (i > currentX && j == currentY)
                            {
                                this.setHelpturns(3, i, j, z, vector);
                            }
                            if (i < currentX && j == currentY)
                            {
                                this.setHelpturns(4, i, j, z, vector);
                            }
                        }
                        else if ((rY == 90f && rZ == 180f) || (rY == 270f && rZ == 0f))
                        {
                            if (i == currentX && j > currentY)
                            {
                                this.setHelpturns(4, i, j, z, vector);
                            }
                            if (i == currentX && j < currentY)
                            {
                                this.setHelpturns(3, i, j, z, vector);
                            }
                            if (i > currentX && j == currentY)
                            {
                                this.setHelpturns(5, i, j, z, vector);
                            }
                            if (i < currentX && j == currentY)
                            {
                                this.setHelpturns(2, i, j, z, vector);
                            }
                        }
                        else if ((rY == 270f && rZ == 180f) || (rY == 90f && rZ == 0f))
                        {
                            if (i == currentX && j > currentY)
                            {
                                this.setHelpturns(3, i, j, z, vector);
                            }
                            if (i == currentX && j < currentY)
                            {
                                this.setHelpturns(4, i, j, z, vector);
                            }
                            if (i > currentX && j == currentY)
                            {
                                this.setHelpturns(2, i, j, z, vector);
                            }
                            if (i < currentX && j == currentY)
                            {
                                this.setHelpturns(5, i, j, z, vector);
                            }
                        }
                        else if (rY == rZ)
                        {
                            if (i == currentX && j > currentY)
                            {
                                this.setHelpturns(2, i, j, z, vector);
                            }
                            if (i == currentX && j < currentY)
                            {
                                this.setHelpturns(5, i, j, z, vector);
                            }
                            if (i > currentX && j == currentY)
                            {
                                this.setHelpturns(4, i, j, z, vector);
                            }
                            if (i < currentX && j == currentY)
                            {
                                this.setHelpturns(3, i, j, z, vector);
                            }
                        }
                    }
                    if (num == 2)
                    {
                        if ((i == currentX && (j < currentY || j > currentY)) || (j == currentY && (i < currentX || i > currentX)))
                        {
                            this.setHelpturns(5, i, j, z, vector);
                        }
                        if (rY == rZ)
                        {
                            if (i < currentX && j > currentY)
                            {
                                this.setHelpturns(18, i, j, z, new Vector3(0f, 270f, 0f));
                            }
                            if (i < currentX && j < currentY)
                            {
                                this.setHelpturns(8, i, j, z, new Vector3(0f, 180f, 0f));
                            }
                            if (i > currentX && j < currentY)
                            {
                                this.setHelpturns(9, i, j, z, new Vector3(0f, 270f, 0f));
                            }
                            if (i > currentX && j > currentY)
                            {
                                this.setHelpturns(20, i, j, z, new Vector3(0f, 180f, 0f));
                            }
                        }
                        if ((rY == 90f && rZ == 180f) || (rY == 0f && rZ == 90f) || (rY == 180f && rZ == 270f) || (rY == 270f && rZ == 0f))
                        {
                            if (i < currentX && j > currentY)
                            {
                                this.setHelpturns(20, i, j, z, new Vector3(0f, 90f, 0f));
                            }
                            if (i < currentX && j < currentY)
                            {
                                this.setHelpturns(18, i, j, z, new Vector3(0f, 180f, 0f));
                            }
                            if (i > currentX && j < currentY)
                            {
                                this.setHelpturns(8, i, j, z, new Vector3(0f, 90f, 0f));
                            }
                            if (i > currentX && j > currentY)
                            {
                                this.setHelpturns(9, i, j, z, new Vector3(0f, 180f, 0f));
                            }
                        }
                        if ((rY == 90f && rZ == 0f) || (rY == 0f && rZ == 270f) || (rY == 180f && rZ == 90f) || (rY == 270f && rZ == 180f))
                        {
                            if (i < currentX && j > currentY)
                            {
                                this.setHelpturns(8, i, j, z, new Vector3(0f, 270f, 0f));
                            }
                            if (i < currentX && j < currentY)
                            {
                                this.setHelpturns(9, i, j, z, new Vector3(0f, 0f, 0f));
                            }
                            if (i > currentX && j < currentY)
                            {
                                this.setHelpturns(20, i, j, z, new Vector3(0f, 270f, 0f));
                            }
                            if (i > currentX && j > currentY)
                            {
                                this.setHelpturns(18, i, j, z, new Vector3(0f, 0f, 0f));
                            }
                        }
                        if ((rY == 90f && rZ == 270f) || (rY == 0f && rZ == 180f) || (rY == 180f && rZ == 0f) || (rY == 270f && rZ == 90f))
                        {
                            if (i < currentX && j > currentY)
                            {
                                this.setHelpturns(9, i, j, z, new Vector3(0f, 90f, 0f));
                            }
                            if (i < currentX && j < currentY)
                            {
                                this.setHelpturns(20, i, j, z, new Vector3(0f, 0f, 0f));
                            }
                            if (i > currentX && j < currentY)
                            {
                                this.setHelpturns(18, i, j, z, new Vector3(0f, 90f, 0f));
                            }
                            if (i > currentX && j > currentY)
                            {
                                this.setHelpturns(8, i, j, z, new Vector3(0f, 0f, 0f));
                            }
                        }
                    }
                    if (num == 3)
                    {
                        if (rY == rZ)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (i == currentX)
                                {
                                    this.setHelpturns(1, i, j, z, vector);
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (i == currentX)
                                {
                                    this.setHelpturns(6, i, j, z, vector);
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                            }
                            if (j == currentY)
                            {
                                if (i < currentX)
                                {
                                    this.setHelpturns(2, i, j, z, vector);
                                }
                                if (i > currentX)
                                {
                                    this.setHelpturns(5, i, j, z, vector);
                                }
                            }
                        }
                        if (rY + rZ == 270f)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (i == currentX)
                                {
                                    this.setHelpturns(2, i, j, z, vector);
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (i == currentX)
                                {
                                    this.setHelpturns(5, i, j, z, vector);
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                            }
                            if (j == currentY)
                            {
                                if (i < currentX)
                                {
                                    this.setHelpturns(6, i, j, z, vector);
                                }
                                if (i > currentX)
                                {
                                    this.setHelpturns(1, i, j, z, vector);
                                }
                            }
                        }
                        if (rZ - rY == 90f)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (i == currentX)
                                {
                                    this.setHelpturns(5, i, j, z, vector);
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (i == currentX)
                                {
                                    this.setHelpturns(2, i, j, z, vector);
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                            }
                            if (j == currentY)
                            {
                                if (i < currentX)
                                {
                                    this.setHelpturns(1, i, j, z, vector);
                                }
                                if (i > currentX)
                                {
                                    this.setHelpturns(6, i, j, z, vector);
                                }
                            }
                        }
                        if (rY + rZ == 360f)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (i == currentX)
                                {
                                    this.setHelpturns(6, i, j, z, vector);
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (i == currentX)
                                {
                                    this.setHelpturns(1, i, j, z, vector);
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(14, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                            }
                            if (j == currentY)
                            {
                                if (i < currentX)
                                {
                                    this.setHelpturns(5, i, j, z, vector);
                                }
                                if (i > currentX)
                                {
                                    this.setHelpturns(2, i, j, z, vector);
                                }
                            }
                        }
                    }
                    if (num == 4)
                    {
                        if (i == currentX || j == currentY || i == currentX + 2 || i == currentX - 2)
                        {
                            this.setHelpturns(4, i, j, z, vector);
                        }
                        if (rY == rZ)
                        {
                            if (currentY < j)
                            {
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(15, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(10, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(7, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(21, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                            }
                            if (currentY > j)
                            {
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(7, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(21, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(15, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(10, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                            }
                        }
                        if (rZ - rY == 90f)
                        {
                            if (currentY < j)
                            {
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(21, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(7, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(15, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(10, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                            }
                            if (currentY > j)
                            {
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(15, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(10, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(21, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(7, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                            }
                        }
                        if (rY + rZ == 270f)
                        {
                            if (currentY < j)
                            {
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(7, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(21, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(10, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(15, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                            }
                            if (currentY > j)
                            {
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(10, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(15, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(7, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(21, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                            }
                        }
                        if (rY + rZ == 360f)
                        {
                            if (currentY < j)
                            {
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(10, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(15, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(21, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(7, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                            }
                            if (currentY > j)
                            {
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(21, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX + 1 == i)
                                {
                                    this.setHelpturns(7, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 1 == i)
                                {
                                    this.setHelpturns(10, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(15, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                            }
                        }
                    }
                    if (num == 5)
                    {
                        if (rY + rZ == 180f || rY + rZ == 540f)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 4 == i || currentX - 4 == i || currentX == i)
                                {
                                    this.setHelpturns(6, i, j, z, vector);
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(16, i, j, z, vector);
                                }
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, vector);
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 4 == i || currentX - 4 == i || currentX == i)
                                {
                                    this.setHelpturns(1, i, j, z, vector);
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, vector);
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(16, i, j, z, vector);
                                }
                            }
                            if (i < currentX && (currentY + 4 == j || currentY - 4 == j || currentY == j))
                            {
                                this.setHelpturns(4, i, j, z, vector);
                            }
                            if (i > currentX && (currentY + 4 == j || currentY - 4 == j || currentY == j))
                            {
                                this.setHelpturns(3, i, j, z, vector);
                            }
                        }
                        if (rY + rZ == 270f)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 4 == i || currentX - 4 == i || currentX == i)
                                {
                                    this.setHelpturns(4, i, j, z, vector);
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(16, i, j, z, vector);
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 4 == i || currentX - 4 == i || currentX == i)
                                {
                                    this.setHelpturns(3, i, j, z, vector);
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(16, i, j, z, vector);
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                            }
                            if (i < currentX && (currentY + 4 == j || currentY - 4 == j || currentY == j))
                            {
                                this.setHelpturns(1, i, j, z, vector);
                            }
                            if (i > currentX && (currentY + 4 == j || currentY - 4 == j || currentY == j))
                            {
                                this.setHelpturns(6, i, j, z, vector);
                            }
                        }
                        if (rY + rZ == 90f || rY + rZ == 450f)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 4 == i || currentX - 4 == i || currentX == i)
                                {
                                    this.setHelpturns(3, i, j, z, vector);
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 4 == i || currentX - 4 == i || currentX == i)
                                {
                                    this.setHelpturns(4, i, j, z, vector);
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                            }
                            if (i < currentX && (currentY + 4 == j || currentY - 4 == j || currentY == j))
                            {
                                this.setHelpturns(6, i, j, z, vector);
                            }
                            if (i > currentX && (currentY + 4 == j || currentY - 4 == j || currentY == j))
                            {
                                this.setHelpturns(1, i, j, z, vector);
                            }
                        }
                        if ((rY == 0f && rZ == 0f) || rY + rZ == 360f)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 4 == i || currentX - 4 == i || currentX == i)
                                {
                                    this.setHelpturns(1, i, j, z, vector);
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 4 == i || currentX - 4 == i || currentX == i)
                                {
                                    this.setHelpturns(6, i, j, z, vector);
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX + 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 2 == i)
                                {
                                    this.setHelpturns(11, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(16, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                            }
                            if (i < currentX && (currentY + 4 == j || currentY - 4 == j || currentY == j))
                            {
                                this.setHelpturns(3, i, j, z, vector);
                            }
                            if (i > currentX && (currentY + 4 == j || currentY - 4 == j || currentY == j))
                            {
                                this.setHelpturns(4, i, j, z, vector);
                            }
                        }
                    }
                    if (num == 6)
                    {
                        if (i == currentX || j == currentY || currentX + 4 == i || currentX + 2 == i || currentX - 2 == i || currentX - 4 == i)
                        {
                            this.setHelpturns(1, i, j, z, vector);
                        }
                        if (rY == rZ)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 5 == i || currentX + 1 == i)
                                {
                                    this.setHelpturns(19, i, j, z, vector);
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(12, i, j, z, vector);
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(13, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 5 == i || currentX - 1 == i)
                                {
                                    this.setHelpturns(17, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 5 == i || currentX + 1 == i)
                                {
                                    this.setHelpturns(13, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(17, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(19, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 5 == i || currentX - 1 == i)
                                {
                                    this.setHelpturns(12, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                            }
                        }
                        if (rY + rZ == 270f)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 5 == i || currentX + 1 == i)
                                {
                                    this.setHelpturns(13, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(17, i, j, z, vector);
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(12, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 5 == i || currentX - 1 == i)
                                {
                                    this.setHelpturns(19, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 5 == i || currentX + 1 == i)
                                {
                                    this.setHelpturns(12, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(19, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(13, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX - 5 == i || currentX - 1 == i)
                                {
                                    this.setHelpturns(17, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                            }
                        }
                        if (rY - rZ == 90f)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 5 == i || currentX + 1 == i)
                                {
                                    this.setHelpturns(17, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(13, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(19, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 5 == i || currentX - 1 == i)
                                {
                                    this.setHelpturns(12, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 5 == i || currentX + 1 == i)
                                {
                                    this.setHelpturns(19, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(12, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(17, i, j, z, new Vector3(0f, 180f, 0f));
                                }
                                if (currentX - 5 == i || currentX - 1 == i)
                                {
                                    this.setHelpturns(13, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                            }
                        }
                        if (rY + rZ == 180f)
                        {
                            if (j > currentY)
                            {
                                if (currentX + 5 == i || currentX + 1 == i)
                                {
                                    this.setHelpturns(12, i, j, z, vector);
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(19, i, j, z, vector);
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(17, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX - 5 == i || currentX - 1 == i)
                                {
                                    this.setHelpturns(13, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                            }
                            if (j < currentY)
                            {
                                if (currentX + 5 == i || currentX + 1 == i)
                                {
                                    this.setHelpturns(17, i, j, z, new Vector3(0f, 270f, 0f));
                                }
                                if (currentX + 3 == i)
                                {
                                    this.setHelpturns(13, i, j, z, new Vector3(0f, 90f, 0f));
                                }
                                if (currentX - 3 == i)
                                {
                                    this.setHelpturns(12, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                                if (currentX - 5 == i || currentX - 1 == i)
                                {
                                    this.setHelpturns(19, i, j, z, new Vector3(0f, 0f, 0f));
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void setHelpturns(int t, int x, int y, float z, Vector3 vector)
    {
        GameObject turnObject = this.GetTurnObject(t);
        turnObject.SetActive(true);
        turnObject.transform.position = new Vector3((float)x + 0.5f, z, (float)y + 0.5f);
        turnObject.transform.rotation = Quaternion.Euler(vector);
    }

    public void Hideturns()
    {
        foreach (GameObject gameObject in this.turns)
        {
            gameObject.SetActive(false);
        }
    }

    public GameObject turnPrefab;

    public List<GameObject> PrefabsWhite;

    public List<GameObject> PrefabsBlack;

    private List<GameObject> turns;

    private cub cubHelp;
}
