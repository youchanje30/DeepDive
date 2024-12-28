using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capture : MonoBehaviour
{
    public int size;
    public Vector3 pos;

    public Camera captureCam;

    public void Cap()
    {


        
        captureCam.gameObject.SetActive(true);
        
        Vector3 camPos = pos;
        camPos.z -= 10;
        captureCam.transform.position = camPos;

        RenderTexture rt = new RenderTexture(size, size, 24);
        captureCam.targetTexture = rt;

        captureCam.Render();

        Texture2D screenshot = new Texture2D(size, size, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, size, size), 0, 0);
        screenshot.Apply();

        captureCam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        captureCam.gameObject.SetActive(false);

        
        // Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        GameObject obj = new GameObject("face");
        
        Sprite sprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));
        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }
}
