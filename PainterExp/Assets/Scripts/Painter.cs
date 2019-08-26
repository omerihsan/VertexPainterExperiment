using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Painter : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshFilter plane;
    void Start()
    {
        Color32[] colors = new Color32[plane.mesh.vertices.Length];
        for(int i = 0; i < colors.Length; i++)
        {
            colors[i] = new Color32((byte)Random.Range(0, 200), (byte)Random.Range(0, 170), (byte)Random.Range(0, 170), 255);
        }

        plane.mesh.colors32 = colors;
    }


    Vector3 hitPoint;
    float distance;
    Color32[] colors;
    Vector3[] vertices;
    Ray ray;
    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 20))
            {
                if (hit.collider.tag == "Mesh")
                {
                    MeshFilter meshfilter = hit.collider.gameObject.GetComponent<MeshFilter>();
                    colors = meshfilter.mesh.colors32;
                    vertices = meshfilter.mesh.vertices;
                    hitPoint = hit.point;

                    for (int index = 0, length = vertices.Length; index < length; index++)
                    {
                        distance = Vector2.Distance(transform.TransformPoint(vertices[index]), hitPoint);
                        if (distance < 0.15)
                        {
                            colors[index] = new Color32(255, 255, 255, 255);
                        }
                    }
                    meshfilter.mesh.colors32 = colors;
                }
            }
        }
    }
}
