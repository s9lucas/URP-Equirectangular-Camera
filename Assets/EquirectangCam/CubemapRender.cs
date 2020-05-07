using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubemapRender : MonoBehaviour
{
    public RenderTexture cubeMap;
    public int faceMask = 63;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (cubeMap != null)
            cam.RenderToCubemap(cubeMap, faceMask);
    }
}
