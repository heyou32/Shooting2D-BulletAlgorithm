using System.Collections.Generic;
using UnityEngine;
 

public class PlayerGrid : MonoBehaviour
{
    public float deviationRange = .25f;
    public float deviationSpeed = .075f;
    public Vector2 viewportMargin = new Vector2(1.5f, 1.5f);
    public FloatRange randomPointDistance = new FloatRange(2.5f, 10f);

#if UNITY_EDITOR
    public bool drawGizmos = true;
    public Color gizmoColor = Color.cyan;
#endif
    public static PlayerGrid instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<PlayerGrid>();

            return _instance;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (drawGizmos == false) return;

        var viewportSize = GetViewportSize(viewportMargin);

        Gizmos.color = gizmoColor;

        Gizmos.DrawWireCube(transform.position, viewportSize * 2);
    }
#endif

    public static float GetDeviationRange()
    {
        if (instance) return instance.deviationRange; else return .25f;
    }
    public static float GetDeviationSpeed()
    {
        if (instance) return instance.deviationSpeed; else return .075f;
    }
    public static FloatRange GetRandomPointsDistance()
    {
        if (instance) return instance.randomPointDistance; else return new FloatRange(2.5f, 10f);
    }
    public static PlayerGridPosition GetFreePosition()
    {
        if (_gridPositions == null) _gridPositions = FindObjectsOfType<PlayerGridPosition>();

        var positions = new List<PlayerGridPosition>();

        for (int i = 0; i < _gridPositions.Length; i++)
        {
            if (_gridPositions[i].occupied == false) positions.Add(_gridPositions[i]);
        }

        if (positions.Count == 0) return null;

        return positions[Random.Range(0, positions.Count)];
    }

    /*
    public static Vector3[] GetRandomPoints(Vector2 range, int number, Vector2 position = default)
    {
        var points = new Vector3[number];

        for (int i = 0; i <= points.Length - 1; i++)
        {
            points[i].x = position.x;
            points[i].y = position.y;

            points[i].x += Random.Range(-range.x, range.x);
            points[i].y += Random.Range(-range.y, range.y);
        }

        return points;
    }
    */

    public static Vector3[] GetRandomPoints(Vector2 start, Vector2 range, int number, FloatRange distance, float failsafe = 10, Vector2 position = default)
    {
        var points = new Vector3[number];

        for (int i = 0; i <= points.Length - 1; i++)
        {
            points[i].x = Random.Range(-range.x, range.x);
            points[i].y = Random.Range(-range.y, range.y);

            var attempts = failsafe;

            var currentDistance = Vector2.Distance(start, points[i]);

            while (attempts > 0 && (currentDistance < distance.min || currentDistance > distance.max))
            {
                points[i].x = Random.Range(-range.x, range.x);
                points[i].y = Random.Range(-range.y, range.y);

                currentDistance = Vector3.Distance(start, points[i]);

                attempts -= 1;
            }

            //print(attempts);

            start = points[i];

            points[i].x += position.x;
            points[i].y += position.y;
        }

        return points;
    }
    public static Vector2 GetViewportMargin()
    {
        if (instance) return instance.viewportMargin;

        return default;
    }
    public static Vector2 GetViewportSize(Vector2 margin = default, float z = 0)
    {
        Vector2 size = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, z - Camera.main.transform.position.z));

        size -= margin;

        return size;
    }

    static PlayerGridPosition[] _gridPositions;

    static PlayerGrid _instance;
}