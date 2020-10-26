using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandelExplorer : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale;

    private Vector2 smoothPos;
    private float smoothScale;
    
    void UpdateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, 0.1f);
        smoothScale = Mathf.Lerp(smoothScale, scale, 0.1f);
        
        float aspect = (float) Screen.width / Screen.height;

        float scaleX = scale;
        float scaleY = scale;

        if (aspect > 1f)
        {
            scaleY /= aspect;
        }
        else
        {
            scaleX *= aspect;
        }
        
        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
    }

    private void FixedUpdate()
    {
        HandleInputs();
        UpdateShader();
    }

    private void HandleInputs()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            scale *= 0.99f;
        }
        
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            scale *= 1.01f;
        }
        
        
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += (float)0.005 * scale;
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= (float)0.005 * scale;
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += (float)0.005 * scale;
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= (float)0.005 * scale;
        }
        
       
    }
}
