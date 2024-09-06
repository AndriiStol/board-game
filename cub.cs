using System;
using UnityEngine;

public abstract class cub : MonoBehaviour
{
    public int CurrentX { get; set; }

    public int CurrentY { get; set; }

    public int runX { get; set; }

    public int runY { get; set; }

    public void Start()
    {
        this.up = new bool[9, 8];
        this.down = new bool[9, 8];
        this.left = new bool[9, 8];
        this.right = new bool[9, 8];
        this.saverotation();
        this.Turns = this.getTurns();
        this.rotateX = base.transform.rotation;
        this.rotateY = base.transform.rotation;
        this.v = base.transform.position;
        base.gameObject.GetComponent<AudioSource>().clip = this.turnOffSound;
    }

    public void Update()
    {
        if (this.t)
        {
            if (this.Turns > 0)
            {
                base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, this.finalRotation, Time.deltaTime * (float)MenuInGame.Instance.rotationSpeed);
                base.transform.position = Vector3.Lerp(base.transform.position, this.v, Time.deltaTime * (float)MenuInGame.Instance.positionSpeed);
                if (base.transform.rotation == this.finalRotation && base.transform.position == this.v)
                {
                    this.saverotation();
                    this.Turns--;
                    if (this.Turns > 0)
                    {
                        this.run(this.runX, this.runY);
                    }
                }
            }
            else if (this.Turns == 0)
            {
                this.t = false;
                this.CurrentX = this.runX;
                this.CurrentY = this.runY;
                this.saverotation();
                this.Turns = this.getTurns();
                BoardManager.Instance.sel = true;
                BoardManager.Instance.isWhiteTurn = !BoardManager.Instance.isWhiteTurn;
                base.gameObject.GetComponent<AudioSource>().Play();
                MenuInGame.Instance.SetTurnPanel(BoardManager.Instance.isWhiteTurn);
                MenuInGame.Instance.SetTurnNumber(++BoardManager.Instance.turnNumber);
                if (!BoardManager.Instance.isNetworkGame)
                {
                    if (BoardManager.Instance.isWhiteTurn)
                    {
                        CameraControl.Instance.setWhiteTurn();
                    }
                    else
                    {
                        CameraControl.Instance.setBlackTurn();
                    }
                }
            }
        }
    }

    public void SetPosition(int runX, int runY, int CurrentX, int CurrentY)
    {
        this.CurrentX = CurrentX;
        this.CurrentY = CurrentY;
        this.runX = runX;
        this.runY = runY;
    }

    public void clearDirection()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                this.up[i, j] = false;
                this.down[i, j] = false;
                this.left[i, j] = false;
                this.right[i, j] = false;
            }
        }
    }

    public bool[,] PossibleMove()
    {
        this.clearDirection();
        bool[,] array = new bool[9, 8];
        if ((this.CurrentX < 9 && this.CurrentX >= 0) || (this.CurrentY >= 0 && this.CurrentY < 8))
        {
            for (int i = 0; i <= this.Turns; i++)
            {
                for (int j = this.Turns; j >= 0; j--)
                {
                    if (i + j == this.Turns)
                    {
                        if (i != 0 && j != 0)
                        {
                            if (this.CurrentX - i >= 0 && this.CurrentY + j < 8)
                            {
                                for (int k = this.CurrentX - 1; k >= this.CurrentX - i; k--)
                                {
                                    int l = this.CurrentY;
                                    if (BoardManager.Instance.Cubs[k, l] != null)
                                    {
                                        break;
                                    }
                                    if (k == this.CurrentX - i)
                                    {
                                        while (l < j + this.CurrentY)
                                        {
                                            if (!(BoardManager.Instance.Cubs[k, l] == null))
                                            {
                                                this.left[this.CurrentX - i, this.CurrentY + j] = false;
                                                break;
                                            }
                                            this.left[this.CurrentX - i, this.CurrentY + j] = true;
                                            l++;
                                        }
                                    }
                                }
                                for (int m = this.CurrentY + 1; m <= j + this.CurrentY; m++)
                                {
                                    int n = this.CurrentX;
                                    if (BoardManager.Instance.Cubs[n, m] != null)
                                    {
                                        break;
                                    }
                                    if (m == j + this.CurrentY)
                                    {
                                        while (n > this.CurrentX - i)
                                        {
                                            if (!(BoardManager.Instance.Cubs[n, m] == null))
                                            {
                                                this.up[this.CurrentX - i, this.CurrentY + j] = false;
                                                break;
                                            }
                                            this.up[this.CurrentX - i, this.CurrentY + j] = true;
                                            n--;
                                        }
                                    }
                                }
                                if (this.left[this.CurrentX - i, this.CurrentY + j] || this.up[this.CurrentX - i, this.CurrentY + j])
                                {
                                    cub cub = BoardManager.Instance.Cubs[this.CurrentX - i, this.CurrentY + j];
                                    if (cub == null)
                                    {
                                        array[this.CurrentX - i, this.CurrentY + j] = true;
                                    }
                                    else if (cub != null && cub.isWhite != this.isWhite)
                                    {
                                        array[this.CurrentX - i, this.CurrentY + j] = true;
                                    }
                                }
                            }
                            if (this.CurrentX + i < 9 && this.CurrentY + j < 8)
                            {
                                for (int num = this.CurrentX + 1; num <= i + this.CurrentX; num++)
                                {
                                    int num2 = this.CurrentY;
                                    if (BoardManager.Instance.Cubs[num, num2] != null)
                                    {
                                        break;
                                    }
                                    if (num == i + this.CurrentX)
                                    {
                                        while (num2 < j + this.CurrentY)
                                        {
                                            if (!(BoardManager.Instance.Cubs[num, num2] == null))
                                            {
                                                this.right[this.CurrentX + i, this.CurrentY + j] = false;
                                                break;
                                            }
                                            this.right[this.CurrentX + i, this.CurrentY + j] = true;
                                            num2++;
                                        }
                                    }
                                }
                                for (int num3 = this.CurrentY + 1; num3 <= j + this.CurrentY; num3++)
                                {
                                    int num4 = this.CurrentX;
                                    if (BoardManager.Instance.Cubs[num4, num3] != null)
                                    {
                                        break;
                                    }
                                    if (num3 == j + this.CurrentY)
                                    {
                                        while (num4 < i + this.CurrentX)
                                        {
                                            if (!(BoardManager.Instance.Cubs[num4, num3] == null))
                                            {
                                                this.up[this.CurrentX + i, this.CurrentY + j] = false;
                                                break;
                                            }
                                            this.up[this.CurrentX + i, this.CurrentY + j] = true;
                                            num4++;
                                        }
                                    }
                                }
                                if (this.up[this.CurrentX + i, this.CurrentY + j] || this.right[this.CurrentX + i, this.CurrentY + j])
                                {
                                    cub cub = BoardManager.Instance.Cubs[this.CurrentX + i, this.CurrentY + j];
                                    if (cub == null)
                                    {
                                        array[this.CurrentX + i, this.CurrentY + j] = true;
                                    }
                                    else if (cub != null && cub.isWhite != this.isWhite)
                                    {
                                        array[this.CurrentX + i, this.CurrentY + j] = true;
                                    }
                                }
                            }
                            if (this.CurrentX + i < 9 && this.CurrentY - j >= 0)
                            {
                                for (int num5 = this.CurrentX + 1; num5 <= i + this.CurrentX; num5++)
                                {
                                    int num6 = this.CurrentY;
                                    if (BoardManager.Instance.Cubs[num5, num6] != null)
                                    {
                                        break;
                                    }
                                    if (num5 == i + this.CurrentX)
                                    {
                                        while (num6 > this.CurrentY - j)
                                        {
                                            if (!(BoardManager.Instance.Cubs[num5, num6] == null))
                                            {
                                                this.right[this.CurrentX + i, this.CurrentY - j] = false;
                                                break;
                                            }
                                            this.right[this.CurrentX + i, this.CurrentY - j] = true;
                                            num6--;
                                        }
                                    }
                                }
                                for (int num7 = this.CurrentY - 1; num7 >= this.CurrentY - j; num7--)
                                {
                                    int num8 = this.CurrentX;
                                    if (BoardManager.Instance.Cubs[num8, num7] != null)
                                    {
                                        break;
                                    }
                                    if (num7 == this.CurrentY - j)
                                    {
                                        while (num8 < i + this.CurrentX)
                                        {
                                            if (!(BoardManager.Instance.Cubs[num8, num7] == null))
                                            {
                                                this.down[this.CurrentX + i, this.CurrentY - j] = false;
                                                break;
                                            }
                                            this.down[this.CurrentX + i, this.CurrentY - j] = true;
                                            num8++;
                                        }
                                    }
                                }
                                if (this.right[this.CurrentX + i, this.CurrentY - j] || this.down[this.CurrentX + i, this.CurrentY - j])
                                {
                                    cub cub = BoardManager.Instance.Cubs[this.CurrentX + i, this.CurrentY - j];
                                    if (cub == null)
                                    {
                                        array[this.CurrentX + i, this.CurrentY - j] = true;
                                    }
                                    else if (cub != null && cub.isWhite != this.isWhite)
                                    {
                                        array[this.CurrentX + i, this.CurrentY - j] = true;
                                    }
                                }
                            }
                            if (this.CurrentX - i >= 0 && this.CurrentY - j >= 0)
                            {
                                for (int num9 = this.CurrentX - 1; num9 >= this.CurrentX - i; num9--)
                                {
                                    int num10 = this.CurrentY;
                                    if (BoardManager.Instance.Cubs[num9, num10] != null)
                                    {
                                        break;
                                    }
                                    if (num9 == this.CurrentX - i)
                                    {
                                        while (num10 > this.CurrentY - j)
                                        {
                                            if (!(BoardManager.Instance.Cubs[num9, num10] == null))
                                            {
                                                this.left[this.CurrentX - i, this.CurrentY - j] = false;
                                                break;
                                            }
                                            this.left[this.CurrentX - i, this.CurrentY - j] = true;
                                            num10--;
                                        }
                                    }
                                }
                                for (int num11 = this.CurrentY - 1; num11 >= this.CurrentY - j; num11--)
                                {
                                    int num12 = this.CurrentX;
                                    if (BoardManager.Instance.Cubs[num12, num11] != null)
                                    {
                                        break;
                                    }
                                    if (num11 == this.CurrentY - j)
                                    {
                                        while (num12 > this.CurrentX - i)
                                        {
                                            if (!(BoardManager.Instance.Cubs[num12, num11] == null))
                                            {
                                                this.down[this.CurrentX - i, this.CurrentY - j] = false;
                                                break;
                                            }
                                            this.down[this.CurrentX - i, this.CurrentY - j] = true;
                                            num12--;
                                        }
                                    }
                                }
                                if (this.down[this.CurrentX - i, this.CurrentY - j] || this.left[this.CurrentX - i, this.CurrentY - j])
                                {
                                    cub cub = BoardManager.Instance.Cubs[this.CurrentX - i, this.CurrentY - j];
                                    if (cub == null)
                                    {
                                        array[this.CurrentX - i, this.CurrentY - j] = true;
                                    }
                                    else if (cub != null && cub.isWhite != this.isWhite)
                                    {
                                        array[this.CurrentX - i, this.CurrentY - j] = true;
                                    }
                                }
                            }
                        }
                        if (i == 0 && this.CurrentY + this.Turns < 8)
                        {
                            for (int num13 = this.CurrentY + 1; num13 < this.CurrentY + j; num13++)
                            {
                                if (BoardManager.Instance.Cubs[this.CurrentX, num13] != null)
                                {
                                    this.up[this.CurrentX, this.CurrentY + j] = false;
                                    break;
                                }
                                this.up[this.CurrentX, this.CurrentY + j] = true;
                            }
                            if (this.Turns == 1)
                            {
                                this.up[this.CurrentX, this.CurrentY + j] = true;
                            }
                            if (this.up[this.CurrentX, this.CurrentY + j])
                            {
                                cub cub = BoardManager.Instance.Cubs[this.CurrentX, this.CurrentY + j];
                                if (cub == null)
                                {
                                    array[this.CurrentX, this.CurrentY + j] = true;
                                }
                                else if (cub != null && cub.isWhite != this.isWhite)
                                {
                                    array[this.CurrentX, this.CurrentY + j] = true;
                                }
                            }
                        }
                        if (i == this.Turns && this.CurrentX - this.Turns >= 0)
                        {
                            for (int num14 = this.CurrentX - 1; num14 > this.CurrentX - i; num14--)
                            {
                                if (BoardManager.Instance.Cubs[num14, this.CurrentY] != null)
                                {
                                    this.left[this.CurrentX - i, this.CurrentY] = false;
                                    break;
                                }
                                this.left[this.CurrentX - i, this.CurrentY] = true;
                            }
                            if (this.Turns == 1)
                            {
                                this.left[this.CurrentX - i, this.CurrentY] = true;
                            }
                            if (this.left[this.CurrentX - i, this.CurrentY])
                            {
                                cub cub = BoardManager.Instance.Cubs[this.CurrentX - i, this.CurrentY];
                                if (cub == null)
                                {
                                    array[this.CurrentX - i, this.CurrentY] = true;
                                }
                                else if (cub != null && cub.isWhite != this.isWhite)
                                {
                                    array[this.CurrentX - i, this.CurrentY] = true;
                                }
                            }
                        }
                        if (i == this.Turns && j == 0 && this.CurrentX + this.Turns < 9)
                        {
                            for (int num15 = this.CurrentX + 1; num15 < this.CurrentX + i; num15++)
                            {
                                if (BoardManager.Instance.Cubs[num15, this.CurrentY] != null)
                                {
                                    this.right[this.CurrentX + i, this.CurrentY] = false;
                                    break;
                                }
                                this.right[this.CurrentX + i, this.CurrentY] = true;
                            }
                            if (this.Turns == 1)
                            {
                                this.right[this.CurrentX + i, this.CurrentY] = true;
                            }
                            if (this.right[this.CurrentX + i, this.CurrentY])
                            {
                                cub cub = BoardManager.Instance.Cubs[this.CurrentX + i, this.CurrentY];
                                if (cub == null)
                                {
                                    array[this.CurrentX + i, this.CurrentY] = true;
                                }
                                else if (cub != null && cub.isWhite != this.isWhite)
                                {
                                    array[this.CurrentX + i, this.CurrentY] = true;
                                }
                            }
                        }
                        if (i == 0 && j == this.Turns && this.CurrentY - this.Turns >= 0)
                        {
                            for (int num16 = this.CurrentY - 1; num16 > this.CurrentY - j; num16--)
                            {
                                if (BoardManager.Instance.Cubs[this.CurrentX, num16] != null)
                                {
                                    this.down[this.CurrentX, this.CurrentY - j] = false;
                                    break;
                                }
                                this.down[this.CurrentX, this.CurrentY - j] = true;
                            }
                            if (this.Turns == 1)
                            {
                                this.down[this.CurrentX, this.CurrentY - j] = true;
                            }
                            if (this.down[this.CurrentX, this.CurrentY - j])
                            {
                                cub cub = BoardManager.Instance.Cubs[this.CurrentX, this.CurrentY - j];
                                if (cub == null)
                                {
                                    array[this.CurrentX, this.CurrentY - j] = true;
                                }
                                else if (cub != null && cub.isWhite != this.isWhite)
                                {
                                    array[this.CurrentX, this.CurrentY - j] = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        return array;
    }

    public void saverotation()
    {
        this.rX = base.transform.eulerAngles.x;
        this.rY = base.transform.eulerAngles.y;
        this.rZ = base.transform.eulerAngles.z;
        this.AccurateRotation();
    }

    public void AccurateRotation()
    {
        if (this.rX >= -10f && this.rX <= 10f)
        {
            this.rX = 0f;
        }
        if (this.rX >= 80f && this.rX <= 100f)
        {
            this.rX = 90f;
        }
        if (this.rX >= 170f && this.rX <= 190f)
        {
            this.rX = 180f;
        }
        if (this.rX >= 260f && this.rX <= 280f)
        {
            this.rX = 270f;
        }
        if (this.rX >= 350f && this.rX <= 370f)
        {
            this.rX = 0f;
        }
        if (this.rY >= -10f && this.rY <= 10f)
        {
            this.rY = 0f;
        }
        if (this.rY >= 80f && this.rY <= 100f)
        {
            this.rY = 90f;
        }
        if (this.rY >= 170f && this.rY <= 190f)
        {
            this.rY = 180f;
        }
        if (this.rY >= 260f && this.rY <= 280f)
        {
            this.rY = 270f;
        }
        if (this.rY >= 350f && this.rY <= 370f)
        {
            this.rY = 0f;
        }
        if (this.rZ >= -10f && this.rZ <= 10f)
        {
            this.rZ = 0f;
        }
        if (this.rZ >= 80f && this.rZ <= 100f)
        {
            this.rZ = 90f;
        }
        if (this.rZ >= 170f && this.rZ <= 190f)
        {
            this.rZ = 180f;
        }
        if (this.rZ >= 260f && this.rZ <= 280f)
        {
            this.rZ = 270f;
        }
        if (this.rZ >= 350f && this.rZ <= 370f)
        {
            this.rZ = 0f;
        }
        base.transform.rotation = Quaternion.Euler(this.rX, this.rY, this.rZ);
    }

    public void turnUp()
    {
        this.rotateX = Quaternion.AngleAxis(base.transform.rotation.x + 90f, Vector3.right);
        this.rotateY = Quaternion.AngleAxis(base.transform.rotation.y, Vector3.forward);
        this.v = new Vector3(this.v.x, this.v.y, this.v.z + 1f);
        this.CurrentY++;
    }

    public void turnDown()
    {
        this.rotateX = Quaternion.AngleAxis(base.transform.rotation.x - 90f, Vector3.right);
        this.rotateY = Quaternion.AngleAxis(base.transform.rotation.y, Vector3.forward);
        this.v = new Vector3(this.v.x, this.v.y, this.v.z - 1f);
        this.CurrentY--;
    }

    public void turnLeft()
    {
        this.rotateY = Quaternion.AngleAxis(base.transform.rotation.y + 90f, Vector3.forward);
        this.rotateX = Quaternion.AngleAxis(base.transform.rotation.x, Vector3.right);
        this.v = new Vector3(this.v.x - 1f, this.v.y, this.v.z);
        this.CurrentX--;
    }

    public void turnRight()
    {
        this.rotateY = Quaternion.AngleAxis(base.transform.rotation.y - 90f, Vector3.forward);
        this.rotateX = Quaternion.AngleAxis(base.transform.rotation.x, Vector3.right);
        this.v = new Vector3(this.v.x + 1f, this.v.y, this.v.z);
        this.CurrentX++;
    }

    public virtual void run(int x, int y)
    {
    }

    public void FinalRotation()
    {
        Debug.Log(this.CurrentX + ", " + this.CurrentY);
        this.finalRotation = this.rotateY * this.rotateX * base.transform.rotation;
        this.t = true;
    }

    public virtual int getTurns()
    {
        return 0;
    }

    public Quaternion rotateX;

    public Quaternion rotateY;

    public Vector3 v;

    public float rX;

    public float rY;

    public float rZ;

    public bool isWhite;

    public bool lineX;

    public bool[,] up;

    public bool[,] down;

    public bool[,] left;

    public bool[,] right;

    public Quaternion finalRotation;

    public int rotationSpeed = 1000;

    public int positionSpeed = 20;

    public int Turns;

    public bool t;

    public AudioClip turnOffSound;
}