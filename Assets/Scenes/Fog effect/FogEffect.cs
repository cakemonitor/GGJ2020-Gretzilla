
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class FogEffect : MonoBehaviour
{
    public Pollution PollutionScript;
    public Material _mat;
    public Color _fogColor;
    public float _depthStart = 0;
    public float _depthDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        //_depthDistance = 500;
    }

    // Update is called once per frame
    void Update()
    {
        if (_depthDistance != PollutionScript.PollutionLevel)
        {
            _depthDistance = PollutionScript.PollutionLevel;
        }

        _mat.SetColor("_FogColor", _fogColor);
        _mat.SetFloat("_DepthStart", _depthStart);
        _mat.SetFloat("_DepthDistance", _depthDistance);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _mat);
    }
}