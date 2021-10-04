using UnityEngine;
using UnityEditor;

public class FindMesh : MonoBehaviour
{
    [MenuItem("Window/FindProblemMesh")]
    private static void FindProblemMesh()
    {
        string meshName = "pb_Mesh37878";

        var filters = FindObjectsOfType<MeshFilter>();
        foreach (var filter in filters)
        {
            if (meshName == filter.sharedMesh.name)
            {
                EditorGUIUtility.PingObject(filter.gameObject);
                break;
            }
        }
    }
}