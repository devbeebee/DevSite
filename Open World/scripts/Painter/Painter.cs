using UnityEngine;
using UnityEngine.InputSystem;


public class Painter : MonoBehaviour
{
    public string dirName = "Saved2D_Textures";
    public string fileName = "textToPng";
    public enum PaintBrush { Pixel , CircleOutLine, Texture};
    public PaintBrush brush;
    public ColorPicker colorPicker = new ColorPicker();
    public Texture2D spriteToDraw;
    public Material materialToPaint;
    public int picSize = 128;
    public int brushSize = 4;
    public int circleSize = 4;
    public float end;
    public bool newImg;
    public bool saveImg;
    Texture2D texture;
    void Start() 
    {
        colorPicker = new ColorPicker();
        NewImage(); 
    }
    
    void NewImage()
    {
        texture = new Texture2D(picSize, picSize);
        materialToPaint.mainTexture = texture;
    }
    void Update()
    {
        if (newImg == true)
        {
            NewImage();
            newImg = false;
        }
        if (saveImg == true)
        {
            texture.SaveTxt2D(dirName, fileName);
            saveImg = false;
        }
        if (NewInput.Instance.RawFire==1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                HandleRaycastHit(hit);
            }
        }
    }

    private void HandleRaycastHit(RaycastHit hit)
    {
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= texture.width;
        pixelUV.y *= texture.height;
        switch (brush)
        {
            case PaintBrush.Pixel:
                materialToPaint.mainTexture = PaintPixel(pixelUV, brushSize);
                break;
            case PaintBrush.CircleOutLine:
                materialToPaint.mainTexture = PaintCircleOutline(pixelUV, circleSize);
                break;
            case PaintBrush.Texture:
                materialToPaint.mainTexture = PaintTextureOnTextue(pixelUV, spriteToDraw);
                break;
        }
    }

    public Texture2D PaintTextureOnTextue(Vector2 pixelUV, Texture2D spriteTexture)
    {
        for (int x = -spriteTexture.width / 2; x < spriteTexture.width / 2; x++)
        {
            for (int y = -spriteTexture.height / 2; y < spriteTexture.height / 2; y++)
            {
                int mainX = x + spriteTexture.width / 2;
                int mainY = y + spriteTexture.height / 2;
                if (spriteTexture.GetPixel(mainX, mainY).a != 0)
                {
                    texture.SetPixel((int)(pixelUV.x + x), (int)(pixelUV.y + y), spriteTexture.GetPixel(mainX, mainY));
                }
            }
        }
        texture.Apply();
        return texture;
    }
    Texture2D PaintPixel(Vector2 pixelUV, int size)
    {
        for (int x = -size / 2; x < size / 2; x++)
        {
            for (int y = -size / 2; y < size / 2; y++)
            {
                texture.SetPixel((int)(pixelUV.x + x), (int)(pixelUV.y + y), colorPicker.colorPicked);
            }
        }

        texture.Apply();
        return texture;
    }

    Texture2D PaintCircleOutline(Vector2 pixelUV, int radious)
    {
        

        //center
        int cx = (int)pixelUV.x;
        int cy = (int)pixelUV.y;
        var y = radious;
        var d = 1 / 4 - y;
        float end = Mathf.Ceil(y / Mathf.Sqrt(2));

        Color col = colorPicker.colorPicked;
        for (int x = 0; x <= end; x++)
        {
            texture.SetPixel(cx + x, cy + y, col);
            texture.SetPixel(cx + x, cy - y, col);
            texture.SetPixel(cx - x, cy + y, col);
            texture.SetPixel(cx - x, cy - y, col);
            texture.SetPixel(cx + y, cy + x, col);
            texture.SetPixel(cx - y, cy + x, col);
            texture.SetPixel(cx + y, cy - x, col);
            texture.SetPixel(cx - y, cy - x, col);

            d += 2 * x + 1;
            if (d > 0)
            {
                d += 2 - 2 * y--;
            }

        }

        texture.Apply();
        return texture;
    }
}