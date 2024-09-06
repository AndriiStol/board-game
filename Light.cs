using System;
using UnityEngine;

public class Light : MonoBehaviour
{
    private void Start()
    {
        global::Light.Instanse = this;
    }

    public void SetPositionGame()
    {
        base.transform.position = new Vector3(4.73f, 4.64f, 4.59f);
        base.transform.rotation = Quaternion.Euler(48f, -47f, -18f);
    }

    public void SetPositionMenu()
    {
        base.transform.position = new Vector3(12.27f, 22.47f, 0f);
        base.transform.rotation = Quaternion.Euler(28.776f, -21.396f, -86.003f);
    }

    public static global::Light Instanse;
}
