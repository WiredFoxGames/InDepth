using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChunckGenerator : MonoBehaviour
{
    public bool generateChildren = false;
    public GameObject spawnerObject;
    public GameObject cubeObject;

    public float height = 16;
    public float width = 16;
    public float depth = 16;
    public float separation = 1;
    public float scale = 50;

    [Range(0, 1)] public float threshold = 0;
    private float oldThreshold;
    private float oldScale;
    private int oldOffset;
    ArrayList sphereList = new ArrayList();

    private Vector3 spawnerPos;
    [Range(13600,13800)]public int offsetX;
    public float offsetY;
    public float offsetZ;


    // Start is called before the first frame update

    public override string ToString()
    {
        string curpos = "Pos[" + gameObject.transform.position.ToString();
        string curscale = "] Scal[H" + height + "-W" + width + "-D"+ depth;
        return base.ToString() + curpos + curscale;
    }

    void Start()
    {
        spawnerPos = gameObject.transform.position;

        oldThreshold = threshold;
        oldScale = scale;
        
        GenerateSpheres();
        ColorSpheres();
        UpdateHidden();
        
    }

    private void Update()
    {
        if (oldScale != scale)
        {
            oldScale = scale;
            ColorSpheres();
        }

        if (oldThreshold != threshold)
        {
            oldThreshold = threshold;
            UpdateHidden();
        }

        if (oldOffset != offsetX)
        {
            oldOffset = offsetX;
            UpdateEverything();
        }

        if (generateChildren)
        {
            generateChildren = false;
            Vector3 newSpawnerPos = new Vector3(spawnerPos.x + width, spawnerPos.y, spawnerPos.z);
            GameObject newSpawner = Instantiate(spawnerObject, newSpawnerPos, transform.rotation );
            
            //CombineMeshes();
            Debug.Log(ToString()); 
        }

        //offsetX += Time.deltaTime * (float)3;
        //UpdateEverything();
    }

    // Starts the process all over again
    void UpdateEverything()
    {
        foreach (GameObject sphere in sphereList)
        {
            Vector3 v3 = sphere.transform.position;

            float xpos = v3.x / width * scale;
            float ypos = v3.y / width * scale;
            float zpos = v3.z / width * scale;

            float perlinColor = GetPerlinColor(xpos, ypos, zpos).b;
            if (perlinColor < threshold)
            {
                sphere.SetActive(false);
            }
            else
            {
                sphere.SetActive(true);
            }
        }
    }

    // Deactivates the cubes that have a color bellow the threshold
    void UpdateHidden()
    {
        foreach (GameObject sphere in sphereList)
        {
            float perlinValue = sphere.GetComponent<PerlinItemValue>().perlinvalue;
            if (perlinValue < threshold)
            {
                sphere.SetActive(false);
            }
            else
            {
                sphere.SetActive(true);
            }
        }
    }

    // Generates a cube of Width*Height*Depth cubes
    void GenerateSpheres()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    GameObject sphere = Instantiate(cubeObject, gameObject.transform);
                    sphere.transform.position = new Vector3(spawnerPos.x + x * separation,
                        spawnerPos.y + y * separation, spawnerPos.z + z * separation);
                    sphereList.Add(sphere);
                }
            }
        }
    }

    // Iterates over all of the cubes and colors them
    void ColorSpheres()
    {
        foreach (GameObject sphere in sphereList)
        {
            Texture2D texture = new Texture2D(1, 1);
            Vector3 v3 = sphere.transform.position;

            float xpos = v3.x / width * scale;
            float ypos = v3.y / width * scale;
            float zpos = v3.z / width * scale;

            Color perlinColor = GetPerlinColor(xpos, ypos, zpos);

            //Renderer renderer = sphere.GetComponent<Renderer>();
            //texture.SetPixel(1, 1, perlinColor);
            //texture.Apply();
            //renderer.material.mainTexture = texture;

            sphere.GetComponent<PerlinItemValue>().perlinvalue = perlinColor.b;
        }
    }

    // Returns the color a cube should be painted with
    Color GetPerlinColor(float xpos, float ypos, float zpos)
    {
        float xPerlin = xpos / width * scale + offsetX;
        float yPerlin = ypos / height * scale + offsetY;
        float zPerlin = zpos / depth * scale + offsetZ;

         float xy = Mathf.PerlinNoise(xPerlin, yPerlin);
         float xz = Mathf.PerlinNoise(xPerlin, zPerlin);
         float yz = Mathf.PerlinNoise(yPerlin, zPerlin);
         // float yx = Mathf.PerlinNoise(yPerlin, xPerlin);
         // float zx = Mathf.PerlinNoise(zPerlin, xPerlin);
         // float zy = Mathf.PerlinNoise(zPerlin, yPerlin);
        //float perlinAvg = (xy + xz + yz + yx + zx + zy) / 6;
        
        float perlinAvg = (xy + xz + yz ) / 3;
        

        // string sphereid = "[" + xpos + "," + ypos + "," + zpos + "]";
        // Debug.Log("Current SphereId > " + sphereid +" current PerlinAvg > " + perlinAvg);

        return new Color(perlinAvg, perlinAvg, perlinAvg);
    }
    
    static float _perlin3DFixed(float a, float b)
    {
        return Mathf.Sin(Mathf.PI * Mathf.PerlinNoise(a, b));
    }

    
    // Merges all the cubes into one single mesh, deleting excessive vertexes in between
    // Doesn't work
    
    // void CombineMeshes()
    // {
    //     MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
    //     CombineInstance[] combine = new CombineInstance[meshFilters.Length];
    //
    //     int i = 0;
    //     while (i < meshFilters.Length)
    //     {
    //         combine[i].mesh = meshFilters[i].sharedMesh;
    //         combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
    //         meshFilters[i].gameObject.SetActive(false);
    //
    //         i++;
    //     }
    //
    //     transform.GetComponent<MeshFilter>().mesh = new Mesh();
    //     transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
    //     transform.gameObject.SetActive(true);
    //     gameObject.AddComponent<BoxCollider>();
    // }
}