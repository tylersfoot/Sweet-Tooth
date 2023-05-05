using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Utility for getting the dominant texture from a splat blend at a given point on a terrain.
public class TerrainTextureDetector : MonoBehaviour
{
    Terrain ThisTerrain;
    TerrainData ThisTerrainData => ThisTerrain.terrainData;

    float[,,] CachedTerrainAlphamapData;

    void Start()
    {
        ThisTerrain = GetComponent<Terrain>();

        CachedTerrainAlphamapData = ThisTerrainData.GetAlphamaps(0, 0, ThisTerrainData.alphamapWidth, ThisTerrainData.alphamapHeight);
    }

    // Gets the index of the most visible texture on the terrain at the specified point in world space.
    // These texture indexes are assigned in the "paint textures" tab of the terrain inspector.
    // If the supplied position is outside the bounds of the terrain, this function will return -1.
    public string GetBiomeAt(Vector3 worldPosition)
    {
        Vector3Int alphamapCoordinates = ConvertToAlphamapCoordinates(worldPosition);

        if (!CachedTerrainAlphamapData.ContainsIndex(alphamapCoordinates.x, dimension: 1))
            return "null";

        if (!CachedTerrainAlphamapData.ContainsIndex(alphamapCoordinates.z, dimension: 0))
            return "null";


        int mostDominantTextureIndex = 0;
        float greatestTextureWeight = float.MinValue;

        int textureCount = CachedTerrainAlphamapData.GetLength(2);
        for (int textureIndex = 0; textureIndex < textureCount; textureIndex++)
        {
            float textureWeight = CachedTerrainAlphamapData[alphamapCoordinates.z, alphamapCoordinates.x, textureIndex];

            if (textureWeight > greatestTextureWeight)
            {
                greatestTextureWeight = textureWeight;
                mostDominantTextureIndex = textureIndex;
            }
        }

        // map the texture index to a biome name
        string biomeName;
        switch (mostDominantTextureIndex)
        {
            case 0:
                biomeName = "candyCornFields";
                break;
            case 1:
                biomeName = "mountains";
                break;
            case 2:
                biomeName = "mapleForest";
                break;
            case 3:
                biomeName = "gumdropValley";
                break;
            case 4:
                biomeName = "peanutButterSwamp";
                break;
            case 5:
                biomeName = "peppermintForest";
                break;
            default:
                biomeName = "null";
                break;
        }

        return biomeName;
    }

    Vector3Int ConvertToAlphamapCoordinates(Vector3 _worldPosition)
    {
        Vector3 relativePosition = _worldPosition - transform.position;
        // Important note: terrains cannot be rotated, so we don't have to worry about rotation

        return new Vector3Int
        (
            x: Mathf.RoundToInt((relativePosition.x / ThisTerrainData.size.x) * ThisTerrainData.alphamapWidth),
            y: 0,
            z: Mathf.RoundToInt((relativePosition.z / ThisTerrainData.size.z) * ThisTerrainData.alphamapHeight)
        );
    }
}