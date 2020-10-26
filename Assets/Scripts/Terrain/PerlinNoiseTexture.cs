using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PerlinNoiseTexture : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20;

    //Updates the texture if the scale changed
    private float prevscale;

    private void Update()
    {
        if (prevscale != scale)
        {
            GeneratePerlinNoise();
            prevscale = scale;
        }
    }

    private void Start()
    { 
        prevscale = scale;
        GeneratePerlinNoise();
    }

    // Gets the object's renderer and applies the newly generated perlin texture onto it
    void GeneratePerlinNoise()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }
    
    // Generates a perlin taxture from the x and y axis
    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
              
                Color color =  GenerateColor(x, y);
                
                texture.SetPixel(x, y, color);
            }
        }
        
        texture.Apply();
        return texture;
    }

    // Generates the color for each iteration of the texture
    Color GenerateColor(int x, int y)
    {
        float xPerlinCoord = (float)x / width * scale;
        float yPerlinCoord = (float)y / height * scale;
                
        float perlin = Mathf.PerlinNoise(xPerlinCoord, yPerlinCoord);
        
        //Debug.Log("PerlinMap Color > " + perlin);

        return new Color(perlin, perlin, perlin);
    }
}
