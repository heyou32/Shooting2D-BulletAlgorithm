using UnityEngine;
public class BoundsHelper : MonoBehaviour
{
    public Vector3 size;

#if UNITY_EDITOR
    [Tooltip("Gizmos color settings.")]
    public Color gizmoColor = new Color(1, 0, 0, 0.5f);
    [Tooltip("Whether to show the gizmos or not.")]
    public bool showGizmos = true;
#endif

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (showGizmos == false) return;

        var bounds = _GetBounds();

        Gizmos.color = gizmoColor;

        Gizmos.DrawWireCube(transform.position, bounds.size);
    }
#endif
    public Bounds GetBounds()
    {
        return _GetBounds();
    }

    Bounds _GetBounds()
    {
        var bounds = RendererHelpers.GetBounds(gameObject);

        var size = bounds.size;

        if (this.size.x != 0) size.x = this.size.x;
        if (this.size.y != 0) size.y = this.size.y;
        if (this.size.z != 0) size.z = this.size.z;

        bounds.size = size;

        return bounds;
    }
}