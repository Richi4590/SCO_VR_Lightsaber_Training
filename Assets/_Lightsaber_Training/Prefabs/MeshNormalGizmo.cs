using UnityEngine;

[RequireComponent(typeof(MeshFilter)), ExecuteInEditMode]
public class MeshNormalGizmo : MonoBehaviour
{
    public bool showVertexNormals = true; // Toggle vertex normals
    public bool showFaceNormals = true;   // Toggle face normals

    public float normalLength = 0.2f;  // Length of normal lines
    public Color vertexNormalColor = Color.green; // Color for vertex normals
    public Color faceNormalColor = Color.red; // Color for face normals

    private void OnDrawGizmos()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
            return;

        Mesh mesh = meshFilter.sharedMesh;

        if (showVertexNormals)
        {
            DrawVertexNormals(mesh);
        }

        if (showFaceNormals)
        {
            DrawFaceNormals(mesh);
        }
    }

    private void DrawVertexNormals(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        if (vertices.Length != normals.Length)
            return;

        Gizmos.color = vertexNormalColor;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldVertex = transform.TransformPoint(vertices[i]);
            Vector3 worldNormal = transform.TransformDirection(normals[i]);

            Gizmos.DrawLine(worldVertex, worldVertex + worldNormal * normalLength);
        }
    }

    private void DrawFaceNormals(Mesh mesh)
    {
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;

        Gizmos.color = faceNormalColor;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            // Get the three vertices of the triangle
            Vector3 v0 = transform.TransformPoint(vertices[triangles[i]]);
            Vector3 v1 = transform.TransformPoint(vertices[triangles[i + 1]]);
            Vector3 v2 = transform.TransformPoint(vertices[triangles[i + 2]]);

            // Compute the face normal
            Vector3 faceNormal = Vector3.Cross(v1 - v0, v2 - v0).normalized;

            // Compute the center of the triangle
            Vector3 faceCenter = (v0 + v1 + v2) / 3f;

            // Draw face normal
            Gizmos.DrawLine(faceCenter, faceCenter + faceNormal * normalLength);
        }
    }
}