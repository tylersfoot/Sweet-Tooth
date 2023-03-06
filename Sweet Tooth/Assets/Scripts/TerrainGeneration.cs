using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int seed; // seed used for noise generation
    public int terrainSize; // size of the terrain
    public int gridSize; // how far apart the vertices are
    public float maxHeight; // maximum height of the terrain
    public float noiseScale; // adjust this value to control the frequency of the noise

    private TerrainData terrainData; // data... about the terrain

    // start is called before the first frame updates
    void Start()
    {
        // ! commented so I can specify seed while testing
        // generate a random seed
        // seed = Random.Range(0, 1000000000);

        // get the terrain data
        terrainData = GetComponent<Terrain>().terrainData;
        
        // set the terrain size to be the same as the size of the terrain object
        terrainSize = (int)terrainData.size.x;

        // set the gridSize based on the heightmap resolution
        gridSize = terrainData.heightmapResolution - 1;

        // set the seed for the perlin noise
        Random.InitState(seed);

        float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
        // loops through all vertices
        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                // calculates the x and z for the perlin noise map
                float perlinX = (float)x / gridSize * terrainSize * noiseScale;
                float perlinZ = (float)z / gridSize * terrainSize * noiseScale;
                // generates the heights 
                heights[z, x] = Mathf.PerlinNoise(perlinX, perlinZ) * maxHeight / terrainSize;
            }
        }

        // set the generated vertice heights to the terrain
        terrainData.SetHeights(0, 0, heights);
    }

    public void Generate()
    {
        Debug.Log("Hello");
    }
}
