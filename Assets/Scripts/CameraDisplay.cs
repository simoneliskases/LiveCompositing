using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public static class CameraDisplay
{
    #region Private Variables

    static WebCamTexture tex;
    static int currentCameraIndex = 0;

    #endregion

    public static void SwapCamera(RawImage display, TextMeshProUGUI textField)
    {
        if(WebCamTexture.devices.Length > 0)
        {
            currentCameraIndex += 1;
            currentCameraIndex %= WebCamTexture.devices.Length;

            if(tex != null)
            {
                StopCamera(display);
                StartStopCamera(display, textField);
            }
        }
    }

    public static void StartStopCamera(RawImage display, TextMeshProUGUI textField)
    {
        if(tex != null)
        {
            StopCamera(display);
            textField.text = "Stop Camera";
        }
        else
        {
            WebCamDevice _device = WebCamTexture.devices[currentCameraIndex];
            tex = new WebCamTexture(_device.name);
            display.texture = tex;

            tex.Play();
            textField.text = "Start Camera";
        }
    }

    private static void StopCamera(RawImage display)
    {
        display.texture = null;
        tex.Stop();
        tex = null;
    }
}
