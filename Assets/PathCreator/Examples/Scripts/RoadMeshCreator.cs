using System.Collections.Generic;
using PathCreation.Utility;
using UnityEngine;

namespace PathCreation.Examples
{
    public class RoadMeshCreator : PathSceneTool
    {
        [Header("Road settings")]
        public float roadWidth = .4f;
        [Range(0, 3f)]
        public float thickness = .15f;
        public bool flattenSurface;

        [Header("Material settings")]
        public Material roadMaterial;
        public Material undersideMaterial;
        public float textureTiling = 1;

        //
        [SerializeField, HideInInspector]
        GameObject meshHolder;

        MeshFilter meshFilter;
        MeshRenderer meshRenderer;
        Mesh mesh;
        MeshCollider meshCollider;

        [SerializeField, HideInInspector]
        GameObject meshCornerHolder1;

        MeshFilter meshCornerFilter1;
        MeshRenderer meshCornerRenderer1;
        Mesh meshCorner1;

        [SerializeField, HideInInspector]
        GameObject meshCornerHolder2;

        MeshFilter meshCornerFilter2;
        MeshRenderer meshCornerRenderer2;
        Mesh meshCorner2;

        public int CircleSegmentCount = 64;

        private int CircleVertexCount;
        private int CircleIndexCount;

        protected override void PathUpdated()
        {
            if (pathCreator != null)
            {
                AssignMeshComponents();
                AssignMaterials();
                CreateRoadMesh();
            }
        }

        void CreateRoadMesh()
        {
            Vector3[] verts = new Vector3[path.NumPoints * 8];
            Vector2[] uvs = new Vector2[verts.Length];
            Vector3[] normals = new Vector3[verts.Length];

            int numTris = 2 * (path.NumPoints - 1) + ((path.isClosedLoop) ? 2 : 0);
            int[] roadTriangles = new int[numTris * 3];
            int[] underRoadTriangles = new int[numTris * 3];
            int[] sideOfRoadTriangles = new int[numTris * 2 * 3];

            int vertIndex = 0;
            int triIndex = 0;

            // Vertices for the top of the road are layed out:
            // 0  1
            // 8  9
            // and so on... So the triangle map 0,8,1 for example, defines a triangle from top left to bottom left to bottom right.
            int[] triangleMap = { 0, 8, 1, 1, 8, 9 };
            int[] sidesTriangleMap = { 4, 6, 14, 12, 4, 14, 5, 15, 7, 13, 15, 5 };

            bool usePathNormals = !(path.space == PathSpace.xyz && flattenSurface);

            for (int i = 0; i < path.NumPoints; i++)
            {
                Vector3 localUp = (usePathNormals) ? Vector3.Cross(path.GetTangent(i), path.GetNormal(i)) : path.up;
                Vector3 localRight = (usePathNormals) ? path.GetNormal(i) : Vector3.Cross(localUp, path.GetTangent(i));

                // Find position to left and right of current path vertex
                Vector3 vertSideA = path.GetPoint(i) - localRight * Mathf.Abs(roadWidth);
                Vector3 vertSideB = path.GetPoint(i) + localRight * Mathf.Abs(roadWidth);

                // Add top of road vertices
                verts[vertIndex + 0] = vertSideA;
                verts[vertIndex + 1] = vertSideB;
                // Add bottom of road vertices
                verts[vertIndex + 2] = vertSideA - localUp * thickness;
                verts[vertIndex + 3] = vertSideB - localUp * thickness;

                // Duplicate vertices to get flat shading for sides of road
                verts[vertIndex + 4] = verts[vertIndex + 0];
                verts[vertIndex + 5] = verts[vertIndex + 1];
                verts[vertIndex + 6] = verts[vertIndex + 2];
                verts[vertIndex + 7] = verts[vertIndex + 3];

                // Set uv on y axis to path time (0 at start of path, up to 1 at end of path)
                uvs[vertIndex + 0] = new Vector2(0, path.times[i]);
                uvs[vertIndex + 1] = new Vector2(1, path.times[i]);

                // Top of road normals
                normals[vertIndex + 0] = localUp;
                normals[vertIndex + 1] = localUp;
                // Bottom of road normals
                normals[vertIndex + 2] = -localUp;
                normals[vertIndex + 3] = -localUp;
                // Sides of road normals
                normals[vertIndex + 4] = -localRight;
                normals[vertIndex + 5] = localRight;
                normals[vertIndex + 6] = -localRight;
                normals[vertIndex + 7] = localRight;

                // Set triangle indices
                if (i < path.NumPoints - 1 || path.isClosedLoop)
                {
                    for (int j = 0; j < triangleMap.Length; j++)
                    {
                        roadTriangles[triIndex + j] = (vertIndex + triangleMap[j]) % verts.Length;
                        // reverse triangle map for under road so that triangles wind the other way and are visible from underneath
                        underRoadTriangles[triIndex + j] = (vertIndex + triangleMap[triangleMap.Length - 1 - j] + 2) % verts.Length;
                    }
                    for (int j = 0; j < sidesTriangleMap.Length; j++)
                    {
                        sideOfRoadTriangles[triIndex * 2 + j] = (vertIndex + sidesTriangleMap[j]) % verts.Length;
                    }

                }

                vertIndex += 8;
                triIndex += 6;
            }

            //
            mesh.Clear();
            mesh.vertices = verts;
            mesh.uv = uvs;
            mesh.normals = normals;
            mesh.subMeshCount = 3;
            mesh.SetTriangles(roadTriangles, 0);
            mesh.SetTriangles(underRoadTriangles, 1);
            mesh.SetTriangles(sideOfRoadTriangles, 2);
            mesh.RecalculateBounds();
            meshCollider.sharedMesh = mesh;


            //coner 1
            meshCorner1.Clear();
            meshCornerFilter1.sharedMesh = GenerateCircleMesh();
            meshCornerHolder1.transform.position = path.GetPoint(0);
            //meshCornerHolder1.transform.up = path.GetTangent(0);
            meshCorner1.RecalculateBounds();

            //coner 2
            meshCornerFilter2.sharedMesh = GenerateCircleMesh();
            meshCornerHolder2.transform.position = path.GetPoint(path.NumPoints-1);
            //meshCornerHolder2.transform.up = -path.GetTangent(path.NumPoints - 1);
            meshCorner2.RecalculateBounds();

        }
        private Mesh GenerateCircleMesh()
        {
            CircleVertexCount = (CircleSegmentCount + 2);
            CircleIndexCount = CircleSegmentCount * 3;

            var circle = new Mesh();
            var vertices = new List<Vector3>(CircleVertexCount);
            Vector3[] normals = new Vector3[CircleVertexCount+1];

            var indices = new int[CircleIndexCount];
            var segmentWidth = Mathf.PI * 2f / CircleSegmentCount;
            var angle = 0f;
            vertices.Add(Vector3.zero);
            for (int i = 0; i < CircleVertexCount; ++i)
            {
                vertices.Add(((new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f)) * roadWidth));
                normals[i] = Vector3.back;
                angle -= segmentWidth;
                if (i > 1)
                {
                    var j = (i - 2) * 3;
                    indices[j + 0] = 0;
                    indices[j + 1] = i - 1;
                    indices[j + 2] = i;
                }
            }

            circle.SetVertices(vertices);
            circle.SetIndices(indices, MeshTopology.Triangles, 0);
            circle.normals = normals;

            circle.RecalculateBounds();
            return circle;
        }
        // Add MeshRenderer and MeshFilter components to this gameobject if not already attached
        void AssignMeshComponents()
        {
            #region Main Road
            if (meshHolder == null)
            {
                meshHolder = new GameObject("Road Mesh Holder");
            }

            meshHolder.transform.rotation = Quaternion.identity;
            meshHolder.transform.position = Vector3.zero;
            meshHolder.transform.localScale = Vector3.one;

            // Ensure mesh renderer and filter components are assigned
            if (!meshHolder.gameObject.GetComponent<MeshFilter>())
            {
                meshHolder.gameObject.AddComponent<MeshFilter>();
            }
            if (!meshHolder.GetComponent<MeshRenderer>())
            {
                meshHolder.gameObject.AddComponent<MeshRenderer>();
            }
            if (!meshHolder.GetComponent<MeshCollider>())
            {
                meshHolder.gameObject.AddComponent<MeshCollider>();
            }

            meshRenderer = meshHolder.GetComponent<MeshRenderer>();
            meshFilter = meshHolder.GetComponent<MeshFilter>();
            meshCollider = meshHolder.GetComponent<MeshCollider>();
            if (mesh == null)
            {
                mesh = new Mesh();
            }
            meshFilter.sharedMesh = mesh;
            #endregion

            #region Corner Road 1
            if (meshCornerHolder1 == null)
            {
                meshCornerHolder1 = new GameObject("Corner 1 Holder");
            }

            meshCornerHolder1.transform.rotation = Quaternion.identity;
            meshCornerHolder1.transform.position = Vector3.zero;
            meshCornerHolder1.transform.localScale = Vector3.one;

            // Ensure mesh renderer and filter components are assigned
            if (!meshCornerHolder1.gameObject.GetComponent<MeshFilter>())
            {
                meshCornerHolder1.gameObject.AddComponent<MeshFilter>();
            }
            if (!meshCornerHolder1.GetComponent<MeshRenderer>())
            {
                meshCornerHolder1.gameObject.AddComponent<MeshRenderer>();
            }
            if (!meshCornerHolder1.GetComponent<MeshCollider>())
            {
                meshCornerHolder1.gameObject.AddComponent<MeshCollider>();
            }

            meshCornerRenderer1 = meshCornerHolder1.GetComponent<MeshRenderer>();
            meshCornerFilter1 = meshCornerHolder1.GetComponent<MeshFilter>();
            if (meshCorner1 == null)
            {
                meshCorner1 = new Mesh();
            }
            meshCornerFilter1.sharedMesh = meshCorner1;
            #endregion

            #region Corner Road 2
            if (meshCornerHolder2 == null)
            {
                meshCornerHolder2 = new GameObject("Corner 2 Holder");
            }

            meshCornerHolder2.transform.rotation = Quaternion.identity;
            meshCornerHolder2.transform.position = Vector3.zero;
            meshCornerHolder2.transform.localScale = Vector3.one;

            // Ensure mesh renderer and filter components are assigned
            if (!meshCornerHolder2.gameObject.GetComponent<MeshFilter>())
            {
                meshCornerHolder2.gameObject.AddComponent<MeshFilter>();
            }
            if (!meshCornerHolder2.GetComponent<MeshRenderer>())
            {
                meshCornerHolder2.gameObject.AddComponent<MeshRenderer>();
            }
            if (!meshCornerHolder2.GetComponent<MeshCollider>())
            {
                meshCornerHolder2.gameObject.AddComponent<MeshCollider>();
            }

            meshCornerRenderer2 = meshCornerHolder2.GetComponent<MeshRenderer>();
            meshCornerFilter2 = meshCornerHolder2.GetComponent<MeshFilter>();
            if (meshCorner2 == null)
            {
                meshCorner2 = new Mesh();
            }
            meshCornerFilter2.sharedMesh = meshCorner2;
            #endregion
        }

        void AssignMaterials()
        {
            if (roadMaterial != null && undersideMaterial != null)
            {
                //main
                meshRenderer.sharedMaterials = new Material[] { roadMaterial, undersideMaterial, undersideMaterial };
                meshRenderer.sharedMaterials[0].mainTextureScale = new Vector3(1, textureTiling);

                //corner 1
                meshCornerRenderer1.sharedMaterials = new Material[] { roadMaterial, undersideMaterial, undersideMaterial };
                meshCornerRenderer1.sharedMaterials[0].mainTextureScale = new Vector3(1, textureTiling);

                //corner 2
                meshCornerRenderer2.sharedMaterials = new Material[] { roadMaterial, undersideMaterial, undersideMaterial };
                meshCornerRenderer2.sharedMaterials[0].mainTextureScale = new Vector3(1, textureTiling);
            }
        }

    }
}