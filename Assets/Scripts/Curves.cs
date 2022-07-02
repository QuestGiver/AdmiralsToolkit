using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curves : MonoBehaviour
{
    //create a bezier curve function for an array of points
    public static Vector3 Bezier(Vector3[] points, float t)
    {
        int count = points.Length;
        Vector3[] temp = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            temp[i] = points[i];
        }
        for (int i = 1; i < count; i++)
        {
            for (int j = 0; j < count - i; j++)
            {
                temp[j] = Vector3.Lerp(temp[j], temp[j + 1], t);
            }
        }
        return temp[0];
    }

    //Detect if 3 points are colinear
    public static bool PointsAreColinear(Vector3 a, Vector3 b, Vector3 c)
    {
        return  Vector3.Cross((a - b),(a - c)).magnitude < 0.001f;
    }




}
