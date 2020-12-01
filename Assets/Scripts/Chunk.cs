using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
    public Vector3Int coord;
    public GameObject boidSpawner;
    public GameObject instantiatedSpawner;
    
    [HideInInspector]
    public Mesh mesh;
    

    private MeshFilter mFilter;
    private MeshRenderer mRenderer;
    private MeshCollider mCollider;
    


    public void DestroyOrDisable () {
        if (Application.isPlaying) {
            mesh.Clear ();
            boidSpawner.SetActive(false);
            gameObject.SetActive (false);
            
        } else {
            //DestroyImmediate(boidSpawner, false);
            DestroyImmediate (gameObject, true);
        }
    }

    // Add components/get references in case lost (references can be lost when working in the editor)
    public void SetUp (Material mat, bool generateCollider) {

        mFilter = GetComponent<MeshFilter> ();
        mRenderer = GetComponent<MeshRenderer> ();
        mCollider = GetComponent<MeshCollider> ();

        if (mFilter == null) {
            mFilter = gameObject.AddComponent<MeshFilter> ();
        }

        if (mRenderer == null) {
            mRenderer = gameObject.AddComponent<MeshRenderer> ();
        }
        
        if (mCollider != null && !generateCollider) {
            DestroyImmediate (mCollider);
        }


        if (mesh == null) {
            mesh = new Mesh ();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            mFilter.sharedMesh = mesh;
            mRenderer.material = mat;
            mCollider = gameObject.AddComponent<MeshCollider> ();
            mCollider = gameObject.AddComponent<MeshCollider> ();
        }

        
        int r = Random.Range(0, 20);
        if (r == 0)
        {
            Vector3 fishpos = new Vector3(coord.x * 16, coord.y * 16,coord.z * 16);
            instantiatedSpawner = Instantiate(boidSpawner, fishpos, Quaternion.identity, transform);
        }
        
        

    }

    public void UpdateColliders()
    {
        mCollider.enabled = false;
        mCollider.enabled = true;

        if (instantiatedSpawner != null)
        {
            Vector3 fishpos = new Vector3(coord.x * 16, coord.y * 16,coord.z * 16);
            instantiatedSpawner.transform.position = fishpos;
        }
    }
    
    
}