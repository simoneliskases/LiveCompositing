using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System;

public class MediaCapture : MonoBehaviour
{
    public static bool captureRender = false;
    public static string dataPath;

    private static Camera environmentCamera;
    private static int fileCounter = 0;
    private static List<Texture2D> allImages = new List<Texture2D>();
    private static float recordingTime = 0;
    private static float savingTime = 0;

    public delegate void OnImagesSaved();
    public static OnImagesSaved ImagesSavedDelegate;

    private void Start()
    {
        environmentCamera = gameObject.GetComponent<Camera>();
    }

    public static IEnumerator Record(int framesPerSecond)
    {
        while (true)
        {
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = environmentCamera.targetTexture;

            environmentCamera.Render();
            Texture2D image = new Texture2D(environmentCamera.targetTexture.width, environmentCamera.targetTexture.height);
            image.ReadPixels(new Rect(0, 0, environmentCamera.targetTexture.width, environmentCamera.targetTexture.height), 0, 0);
            image.Apply();
            allImages.Add(image);

            RenderTexture.active = currentRT;
            recordingTime += Time.deltaTime;
                
            yield return new WaitForSecondsRealtime(1 / framesPerSecond);
        }
    }

    public static void SaveRecording()
    {
        Task task = new Task(ImagesToPNGs);
        task.RunSynchronously();

        ImagesSavedDelegate += ImagesToVideo;
    }

    private static async void ImagesToPNGs()
    {
        foreach (Texture2D image in allImages)
        {
            byte[] bytes = image.EncodeToPNG();
            Destroy(image);

            File.WriteAllBytes(dataPath + "/" + fileCounter + ".png", bytes);
            fileCounter++;
            savingTime += Time.deltaTime;

            await Task.Yield(); 
        }

        ImagesSavedDelegate();
    }

    private static void ImagesToVideo()
    {
        ImagesSavedDelegate -= ImagesToVideo;
        Debug.Log("Recording Time: " + recordingTime);
        Debug.Log("Saving Time: " + savingTime);
    }
}