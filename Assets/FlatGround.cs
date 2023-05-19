#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "FlatGround", menuName = "Brushes/FlatGround")]
public class FlatGround : GridBrush
{
    public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
    {
        // Your custom paint logic here, you can call base.Paint to use the default painting logic
        base.Paint(grid, brushTarget, position);
    }
}
#endif