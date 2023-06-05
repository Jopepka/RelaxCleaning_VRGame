using UnityEngine;
using UnityEngine.Profiling;

public class PaintWithMouse : MonoBehaviour
{
    [SerializeField] private Shader drawShader;
    [SerializeField][Range(1, 500)] private float size;

    private RenderTexture _splatMap;
    private Material _currentMaterial, _drawMaterial;
    private RaycastHit _hit;
    private bool _paintMode;
    private bool _cleaning;

    private static readonly int SplatMap = Shader.PropertyToID("SplatMap");
    private static readonly int Coordinates = Shader.PropertyToID("_Coordinates");
    private static readonly int Size = Shader.PropertyToID("_Size");

    private void Start()
    {
        _drawMaterial = new Material(drawShader);

        _currentMaterial = GetComponent<MeshRenderer>().material;

        _splatMap = new RenderTexture(256, 256, 0, RenderTextureFormat.ARGBFloat);

        _currentMaterial.SetTexture(SplatMap, _splatMap);
    }

    private void Update()
    {
        if (!UnityEngine.Input.GetMouseButton(0))
            return;

        if (!Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out _hit))
            return;

        _drawMaterial.SetVector(Coordinates, new Vector4(_hit.lightmapCoord.x, _hit.lightmapCoord.y, 0, 0));
        _drawMaterial.SetFloat(Size, size);

        var temp = RenderTexture.GetTemporary(_splatMap.width, _splatMap.height, 0, RenderTextureFormat.ARGBFloat);

        Graphics.Blit(_splatMap, temp);
        Graphics.Blit(temp, _splatMap, _drawMaterial);

        RenderTexture.ReleaseTemporary(temp);
    }
}