using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private const float cameraPositionOffset = 0.5f;
    private const float cameraSizeOffsetMultiplier = 1.2f;
    private Camera myCamera;

    private void Awake()
    {
        myCamera = Camera.main;
    }
    public  void CameraSetSize(int _size) 
    {
        myCamera.orthographicSize = _size * cameraSizeOffsetMultiplier;
        float gridWidth = _size;
        float gridHeight = _size;
        Vector2 centerPosition = new Vector2(gridWidth / 2 - cameraPositionOffset, gridHeight / 2 - cameraPositionOffset);
        myCamera.transform.position = centerPosition;
    }
}
