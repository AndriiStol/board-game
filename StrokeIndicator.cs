using System;
using System.Collections.Generic;
using UnityEngine;

public class StrokeIndicator : MonoBehaviour
{
    private void Start()
    {
        StrokeIndicator.Instanse = this;
        this.pointers = new List<GameObject>();
    }

    private GameObject GetIndicator()
    {
        GameObject gameObject = this.pointers.Find((GameObject g) => !g.activeSelf);
        if (gameObject == null)
        {
            gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pointer);
            this.pointers.Add(gameObject);
        }
        return gameObject;
    }

    public bool SetIndictor(cub c, int x, int y, bool[,] r, float mousehitX, float mousehitY, cub[,] Cubs)
    {
        this.Hidden();
        c.lineX = BoardManager.Instance.Direction(x, y, mousehitX, mousehitY);
        int currentX = c.CurrentX;
        int currentY = c.CurrentY;
        if (Cubs[x, y] != null && Cubs[x, y].isWhite != c.isWhite && c.Turns == 1)
        {
            this.z = 1.2f;
        }
        else
        {
            this.z = 0.2f;
        }
        if (r[x, y] && c.lineX)
        {
            if (c.left[x, y])
            {
                this.SpawnIndicator(currentX - 1, currentY, this.left);
                return false;
            }
            if (c.right[x, y])
            {
                this.SpawnIndicator(currentX + 1, currentY, this.right);
                return false;
            }
        }
        if (r[x, y] && !c.lineX)
        {
            if (c.up[x, y])
            {
                this.SpawnIndicator(currentX, currentY + 1, this.up);
                return false;
            }
            if (c.down[x, y])
            {
                this.SpawnIndicator(currentX, currentY - 1, this.down);
                return false;
            }
        }
        return true;
    }

    private void SpawnIndicator(int x, int y, Quaternion q)
    {
        GameObject indicator = this.GetIndicator();
        indicator.SetActive(true);
        indicator.transform.position = this.GetTileCenter(x, y);
        indicator.transform.rotation = q;
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 zero = Vector3.zero;
        zero.x += 1f * (float)x + 0.5f;
        zero.z += 1f * (float)y + 0.5f;
        zero.y = this.z;
        return zero;
    }

    public void Hidden()
    {
        foreach (GameObject gameObject in this.pointers)
        {
            gameObject.SetActive(false);
        }
    }

    public static StrokeIndicator Instanse;

    public GameObject pointer;

    private List<GameObject> pointers;

    private float z = 0.2f;

    private Quaternion left = Quaternion.Euler(0f, 90f, 0f);

    private Quaternion right = Quaternion.Euler(0f, 270f, 0f);

    private Quaternion up = Quaternion.Euler(0f, 180f, 0f);

    private Quaternion down = Quaternion.Euler(0f, 0f, 0f);

    private const float TILE_SIZE = 1f;

    private const float TILE_OFFSET = 0.5f;
}
