using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class IndexScript
{
    public static bool ContainsIndex(this Array array, int index, int dimension)
    {
        if (index < 0)
            return false;
    
        return index < array.GetLength(dimension);
    }
}
