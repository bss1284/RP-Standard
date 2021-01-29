using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer),typeof(MeshFilter))]
public class Cube : MonoBehaviour
{
    private MeshFilter mMeshFilter => GetComponent<MeshFilter>();
    private MeshRenderer mMeshRenderer => GetComponent<MeshRenderer>();

    private Mesh mesh;
    private Vector3[] vertices;

    public int xSize, ySize, zSize;

    private void Awake()
    {
        Generate();
    }

    public void Generate()
    {
        mesh = new Mesh();
        mesh.name = "Procedural Cube";
        mMeshFilter.mesh = mesh;

        CreateVertices();
        
    }

    private void CreateVertices()
    {
        int cornerVertices = 8;
        int edgeVertices = 4 * (xSize + ySize + zSize - 3);
        int faceVertices = 2 * ((xSize - 1) * (ySize - 1) + (xSize - 1) * (zSize - 1) + (ySize - 1) * (zSize - 1));

        int totalVertices = cornerVertices + edgeVertices + faceVertices;
        vertices= new Vector3[totalVertices];

        int vIdx = 0;
        for (int y=0;y<=ySize;y++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                vertices[vIdx++] = new Vector3(x, y, 0);
            }
            for(int z = 1; z <= zSize; z++)
            {
                vertices[vIdx++] = new Vector3(xSize, y, z);
            }
            for(int x = xSize - 1; x >= 0; x--)
            {
                vertices[vIdx++] = new Vector3(x, y, zSize);
            }
            for(int z = zSize - 1; z >= 0; z--)
            {
                vertices[vIdx++] = new Vector3(0, y, z);
            }
        }

        for(int z = 1; z < zSize; z++)
        {
            for(int x = 1; x < xSize; x++)
            {
                vertices[vIdx++] = new Vector3(x, ySize, z);
            }
        }
        for(int z = 1; z < zSize; z++)
        {
            for(int x = 1; x < xSize; x++)
            {
                vertices[vIdx++] = new Vector3(x, 0, z);
            }
        }

        mesh.vertices = vertices;
    }



    private void OnDrawGizmos()
    {
        if(vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
