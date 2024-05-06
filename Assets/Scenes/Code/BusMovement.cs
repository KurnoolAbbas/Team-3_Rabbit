using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusMovement : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _xSpeed;
    [SerializeField] private float _ySpeed;

    void Update()
    {
        // Calculate the new UV position based on time and speed
        float newX = _img.uvRect.x + _xSpeed * Time.deltaTime;
        float newY = _img.uvRect.y + _ySpeed * Time.deltaTime;

        // Update the UV rectangle of the RawImage to move the texture
        _img.uvRect = new Rect(newX, newY, _img.uvRect.width, _img.uvRect.height);
    }
}
