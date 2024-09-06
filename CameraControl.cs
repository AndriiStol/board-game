using System;
using UnityEngine;
using DG.Tweening;


public class CameraControl : MonoBehaviour
{

    public static CameraControl Instance { get; set; }

    public void Start()
    {
        CameraControl.Instance = this;
       
        
        if (BoardManager.Instance.isNetworkGame)
        {
            if (BoardManager.Instance.myCubsWhite)
            {
                this.setWhiteTurn();
            }
            else
            {
                this.setBlackTurn();
            }
        }
        else
        {
            this.setWhiteTurn();
        }
    }

    private void Update()
    {
       
   
        
    }

    public void setWhiteTurn()
    {
        transform.DOLocalMove(whitePosition, 1).SetDelay(0.5f);
        transform.DOLocalRotate(whiteRotation, 1).SetDelay(0.5f);
    }


    public void setBlackTurn()
    {

        transform.DOLocalMove(blackPosition, 1).SetDelay(0.5f);
        transform.DOLocalRotate(blackRotation, 1).SetDelay(0.5f);
    }


    [SerializeField] private Vector3 whitePosition;
    [SerializeField] private Vector3 whiteRotation;
    [SerializeField] private Vector3 blackPosition;
    [SerializeField] private Vector3 blackRotation;

    private Vector3 offset;

    private float cameraSpeed = Setting.cameraSpeed;

    private float transitionTurn = Setting.transitionTurn;



    private float zoom = Setting.zoom;

    private float zoomMax = Setting.zoomMax;

    private float zoomMin = Setting.zoomMin;

    private float X;

    private float Y;

    private Vector3 newPosition;

    private Vector3 newRotation;
}
