using UnityEngine;

public class CleanableObject : MonoBehaviour
{
    [SerializeField] private Texture2D _dirtMaskBase;
    private Texture2D _templateDirtMask;

    private void Start()
    {
        CreateTexture();
    }

    public void Change(RaycastHit hit, Texture2D brush)
    {
        Vector2 textureCoord = hit.textureCoord;

        int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
        int pixelY = (int)(textureCoord.y * _templateDirtMask.height);

        int pixelXOffset = pixelX - (brush.width / 2);
        int pixelYOffset = pixelY - (brush.height / 2);

        for (int x = 0; x < brush.width; x++)
        {
            for (int y = 0; y < brush.height; y++)
            {
                Color pixelDirt = brush.GetPixel(x, y);
                Color pixelDirtMask = _templateDirtMask.GetPixel(pixelXOffset + x,
                    pixelYOffset + y);

                _templateDirtMask.SetPixel(pixelXOffset + x,
                    pixelYOffset + y,
                    new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
            }
        }
        Debug.Log($"Object changed {gameObject.name}");

        _templateDirtMask.Apply();
    }

    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();

        gameObject.GetComponent<Renderer>().material.SetTexture("_DirtMask", _templateDirtMask);
    }
}
