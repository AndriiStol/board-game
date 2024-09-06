using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    public static BoardManager Instance { get; set; }


    private bool[,] allMoves { get; set; }


    public cub[,] Cubs { get; set; }

    private void Start()
    {
        this.isWhiteTurn = true;
        BoardManager.Instance = this;
        this.AllSpawnCub();
        this.sel = true;
        this.turnNumber = 0;
        
    }

    private void Update()
    {
        if (this.debug)
        {
            this.DrawDuelBoard();
        }
        this.UpdateSelection();
        this.SetStrokeIndicator();
        if (this.viewHelpTurns && this.sel)
        {
            this.ViewHelpTurns();
        }
        if (Input.GetMouseButtonDown(0) && this.selectionX >= 0 && this.selectionY >= 0 && this.sel)
        {
            if (this.isNetworkGame && this.isWhiteTurn == this.myCubsWhite)
            {
                this.SelectOrMove(this.selectionX, this.selectionY);
            }
            else if (!this.isNetworkGame)
            {
                this.SelectOrMove(this.selectionX, this.selectionY);
            }
        }
    }

    private void SelectOrMove(int x, int y)
    {
        if (this.selectedCub == null)
        {
            this.SelectCub(x, y);
        }
        else
        {
            this.selectedCub.lineX = this.Direction(x, y, this.mousehitX, this.mousehitY);
            this.MoveCub(x, y);
        }
    }

    private void SelectCub(int x, int y)
    {
        if (!(this.Cubs[x, y] == null))
        {
            if (this.Cubs[x, y].isWhite == this.isWhiteTurn)
            {
                if (!this.isNetworkGame || this.isWhiteTurn == this.myCubsWhite)
                {
                    bool flag = false;
                    this.allMoves = this.Cubs[x, y].PossibleMove();
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (this.allMoves[i, j])
                            {
                                flag = true;
                            }
                        }
                    }
                    if (flag)
                    {
                        this.selectedCub = this.Cubs[x, y];
                        BoardTurn.Instance.turnAllMoves(this.allMoves, this.Cubs[x, y], this.Cubs);
                    }
                }
            }
        }
    }

    private void MoveCub(int x, int y)
    {
        if (this.allMoves[x, y])
        {
            bool flag = this.CheckCanTurn(x, y);
            cub c = this.Cubs[x, y];
            this.runX = x;
            this.runY = y;
            int currentX = this.selectedCub.CurrentX;
            int currentY = this.selectedCub.CurrentY;
            if (flag)
            {
                if (this.isNetworkGame)
                {
                    string text = "CMOV|";
                    text = text + this.runX.ToString() + '|';
                    text = text + this.runY.ToString() + '|';
                    text = text + currentX.ToString() + '|';
                    text = text + currentY.ToString() + '|';
                    text += this.selectedCub.lineX.ToString();
                }
                else
                {
                    this.Cubs[this.runX, this.runY] = this.selectedCub;
                    this.Cubs[this.runX, this.runY].SetPosition(this.runX, this.runY, currentX, currentY);
                    this.Cubs[currentX, currentY] = null;
                    this.CheckVictory(x, y, c);
                    this.sel = false;
                    this.selectedCub.run(this.runX, this.runY);
                }
            }
        }
        BoardTurn.Instance.Hideturns();
        StrokeIndicator.Instanse.Hidden();
        this.selectedCub = null;
    }

   

    private void CheckVictory(int x, int y, cub c)
    {
        if (this.selectedCub.name == "kingWhite(Clone)" && x == 4 && y == 7)
        {
            this.WhiteWins();
        }
        if (this.selectedCub.name == "kingBlack(Clone)" && x == 4 && y == 0)
        {
            this.BlackWins();
        }
        if (c != null && c.isWhite != this.isWhiteTurn)
        {
            if (c.GetType() == typeof(King))
            {
                Debug.Log("GAME OVER");
            }
            if (c.name == "kingWhite(Clone)")
            {
                this.BlackWins();
            }
            if (c.name == "kingBlack(Clone)")
            {
                this.WhiteWins();
            }
            if (c.isWhite)
            {
                c.transform.position = this.GetTileCenter(-2, this.deadWhite);
                this.deadWhite--;
            }
            if (!c.isWhite)
            {
                c.transform.position = this.GetTileCenter(10, this.deadBlack);
                this.deadBlack++;
            }
        }
    }

    public bool Direction(int x, int y, float pointX, float pointY)
    {
        bool result = true;
        float num = 0.01f;
        float num2 = (float)x;
        float num3 = (float)y;
        float x2 = num2 + 1f;
        float y2 = num3;
        float num4 = num2;
        float num5 = num3 + 1f;
        float x3 = num2 + 1f;
        float y3 = num3 + 1f;
        if ((this.selectedCub.CurrentX > x && this.selectedCub.CurrentY < y) || (this.selectedCub.CurrentX < x && this.selectedCub.CurrentY > y))
        {
            float num6 = this.S_triangle(num2, x2, num4, num3, y2, num5);
            float num7 = this.Point_triangle(num2, x2, num4, this.mousehitX, num3, y2, num5, this.mousehitY);
            if (Mathf.Abs(num6 - num7) < num)
            {
                result = (this.selectedCub.CurrentX > x && this.selectedCub.CurrentY < y);
            }
            else
            {
                result = (this.selectedCub.CurrentX <= x || this.selectedCub.CurrentY >= y);
            }
        }
        else if ((this.selectedCub.CurrentX > x && this.selectedCub.CurrentY > y) || (this.selectedCub.CurrentX < x && this.selectedCub.CurrentY < y))
        {
            float num6 = this.S_triangle(num4, num2, x3, num5, num3, y3);
            float num7 = this.Point_triangle(num4, num2, x3, this.mousehitX, num5, num3, y3, this.mousehitY);
            if (Mathf.Abs(num6 - num7) < num)
            {
                result = (this.selectedCub.CurrentX > x && this.selectedCub.CurrentY > y);
            }
            else
            {
                result = (this.selectedCub.CurrentX <= x || this.selectedCub.CurrentY <= y);
            }
        }
        else if (this.selectedCub.CurrentX == x)
        {
            if (this.selectedCub.CurrentY < y || this.selectedCub.CurrentY > y)
            {
                result = false;
            }
            else if (this.selectedCub.CurrentY == y && (this.selectedCub.CurrentX < x || this.selectedCub.CurrentX > x))
            {
                result = true;
            }
        }
        return result;
    }

    private float Point_triangle(float x1, float x2, float x3, float px, float y1, float y2, float y3, float py)
    {
        return this.S_triangle(x1, x2, px, y1, y2, py) + this.S_triangle(x1, px, x3, y1, py, y3) + this.S_triangle(px, x2, x3, py, y2, y3);
    }

    private float S_triangle(float x1, float x2, float x3, float y1, float y2, float y3)
    {
        return Mathf.Abs((x1 - x2) * (y3 - y2) - (y1 - y2) * (x3 - x2)) / 2f;
    }

    private bool CheckCanTurn(int x, int y)
    {
        int currentX = this.selectedCub.CurrentX;
        int currentY = this.selectedCub.CurrentY;
        if (this.selectedCub.lineX)
        {
            if (!this.selectedCub.left[x, y] && currentX > x)
            {
                return false;
            }
            if (!this.selectedCub.right[x, y] && currentX < x)
            {
                return false;
            }
        }
        else
        {
            if (!this.selectedCub.up[x, y] && currentY < y)
            {
                return false;
            }
            if (!this.selectedCub.down[x, y] && currentY > y)
            {
                return false;
            }
        }
        return true;
    }

    private void UpdateSelection()
    {
        if (Camera.main)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 25f, LayerMask.GetMask(new string[]
            {
                "DuelPlane"
            })))
            {
                this.selectionX = (int)raycastHit.point.x;
                this.selectionY = (int)raycastHit.point.z;
                this.mousehitX = raycastHit.point.x;
                this.mousehitY = raycastHit.point.z;
            }
            else
            {
                this.selectionX = -1;
                this.selectionY = -1;
                this.mousehitX = -1f;
                this.mousehitY = -1f;
            }
        }
    }

    private void ViewHelpTurns()
    {
        if (this.selectionX == -1)
        {
            this.HidenHelpTurns();
        }
        else if (!this.sel)
        {
            this.HidenHelpTurns();
        }
        else if (this.Cubs[this.selectionX, this.selectionY] == null)
        {
            this.HidenHelpTurns();
        }
        else
        {
            cub cub = this.Cubs[this.selectionX, this.selectionY];
            if (!this.viewSelect && this.sel)
            {
                this.viewSelectCub = cub;
                BoardTurnEnemy.Instance.TurnAllMoves(cub, this.Cubs);
                this.viewSelect = true;
            }
            if (this.viewSelectCub != cub)
            {
                this.HidenHelpTurns();
            }
        }
    }

    private void HidenHelpTurns()
    {
        BoardTurnEnemy.Instance.Hideturns();
        this.viewSelectCub = null;
        this.viewSelect = false;
    }

    private void SetStrokeIndicator()
    {
        if (this.selectedCub != null && this.selectionX >= 0 && this.selectionY >= 0 && this.indicator)
        {
            StrokeIndicator.Instanse.SetIndictor(this.selectedCub, this.selectionX, this.selectionY, this.allMoves, this.mousehitX, this.mousehitY, this.Cubs);
        }
    }

    private void SpawnCub(int index, int x, int y, Quaternion orientation)
    {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cubPrefads[index], this.GetTileCenter(x, y), orientation);
        gameObject.transform.SetParent(base.transform);
        this.Cubs[x, y] = gameObject.GetComponent<cub>();
        this.Cubs[x, y].SetPosition(x, y, x, y);
        this.activeCub.Add(gameObject);
    }

    private void AllSpawnCub()
    {
        this.activeCub = new List<GameObject>();
        this.Cubs = new cub[9, 8];
        this.SpawnCub(0, 4, 0, Quaternion.identity);
        this.SpawnCub(1, 0, 0, Quaternion.Euler(270f, 0f, 90f));
        this.SpawnCub(1, 1, 0, Quaternion.Euler(0f, 90f, 0f));
        this.SpawnCub(1, 2, 0, Quaternion.Euler(90f, 90f, 0f));
        this.SpawnCub(1, 3, 0, Quaternion.Euler(0f, 270f, 180f));
        this.SpawnCub(1, 5, 0, Quaternion.Euler(0f, 270f, 180f));
        this.SpawnCub(1, 6, 0, Quaternion.Euler(90f, 90f, 0f));
        this.SpawnCub(1, 7, 0, Quaternion.Euler(0f, 90f, 0f));
        this.SpawnCub(1, 8, 0, Quaternion.Euler(270f, 0f, 90f));
        this.SpawnCub(2, 4, 7, Quaternion.identity);
        this.SpawnCub(3, 0, 7, Quaternion.Euler(270f, 0f, 270f));
        this.SpawnCub(3, 1, 7, Quaternion.Euler(0f, 270f, 0f));
        this.SpawnCub(3, 2, 7, Quaternion.Euler(90f, 0f, 90f));
        this.SpawnCub(3, 3, 7, Quaternion.Euler(0f, 90f, 180f));
        this.SpawnCub(3, 5, 7, Quaternion.Euler(0f, 90f, 180f));
        this.SpawnCub(3, 6, 7, Quaternion.Euler(90f, 0f, 90f));
        this.SpawnCub(3, 7, 7, Quaternion.Euler(0f, 270f, 0f));
        this.SpawnCub(3, 8, 7, Quaternion.Euler(270f, 0f, 270f));
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 zero = Vector3.zero;
        zero.x += 1f * (float)x + 0.5f;
        zero.z += 1f * (float)y + 0.5f;
        zero.y = 0.4f;
        return zero;
    }

    private void DrawDuelBoard()
    {
        Vector3 b = Vector3.right * 9f;
        Vector3 b2 = Vector3.forward * 8f;
        for (int i = 0; i <= 8; i++)
        {
            Vector3 vector = Vector3.forward * (float)i;
            Debug.DrawLine(vector, vector + b);
            for (int j = 0; j <= 9; j++)
            {
                vector = Vector3.right * (float)j;
                Debug.DrawLine(vector, vector + b2);
            }
        }
        if (this.selectionX >= 0 && this.selectionY >= 0)
        {
            Debug.DrawLine(Vector3.forward * (float)this.selectionY + Vector3.right * (float)this.selectionX, Vector3.forward * (float)(this.selectionY + 1) + Vector3.right * (float)(this.selectionX + 1));
            Debug.DrawLine(Vector3.forward * (float)this.selectionY + Vector3.right * (float)(this.selectionX + 1), Vector3.forward * (float)(this.selectionY + 1) + Vector3.right * (float)this.selectionX);
        }
    }

    private void WhiteWins()
    {
       
        this.isNetworkGame = false;
        MenuInGame.Instance.ActiveWinMenu(true);
        this.sel = false;
    }

    private void BlackWins()
    {
       
        this.isNetworkGame = false;
        MenuInGame.Instance.ActiveWinMenu(false);
        this.sel = false;
    }

    public void RestartGame()
    {
        foreach (GameObject obj in this.activeCub)
        {
            UnityEngine.Object.Destroy(obj);
        }
        this.Start();
        MenuInGame.Instance.SetTurnNumber(this.turnNumber);
        MenuInGame.Instance.SetTurnPanel(true);
        CameraControl.Instance.Start();
    }

    public void EndGame()
    {
        foreach (GameObject obj in this.activeCub)
        {
            UnityEngine.Object.Destroy(obj);
        }
    }

    private bool debug = Setting.debug;

    private cub selectedCub;

    public BoardTurn enemyTurns;

    public bool isWhiteTurn;

    public bool myCubsWhite;

    public bool isNetworkGame;

    public bool sel;

    public int turnNumber;

    public cub viewSelectCub = null;

    public bool viewHelpTurns;

    private bool viewSelect = false;

    private bool indicator = true;

    private const float TILE_SIZE = 1f;

    private const float TILE_OFFSET = 0.5f;

    private float mousehitX = -1f;

    private float mousehitY = -1f;

    private int selectionX = -1;

    private int selectionY = -1;

    private int runX;

    private int runY;

    private int deadWhite = 7;

    private int deadBlack = 0;

    public List<GameObject> cubPrefads;

    private List<GameObject> activeCub;

}