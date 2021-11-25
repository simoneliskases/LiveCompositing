using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class UserInterface : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Material chromaKeyMaterial;

    [SerializeField]
    private RawImage cameraDisplay;

    [SerializeField]
    private RawImage environmentDisplay;

    [SerializeField]
    private Image stroke;

    [SerializeField]
    private TextMeshProUGUI startStopText;

    [SerializeField]
    private TMP_Dropdown aspectRatioDropdown;

    [SerializeField]
    private Slider sensitivitySlider;

    [SerializeField]
    private Slider smoothnessSlider;

    #endregion

    #region Private Variables

    private Coroutine currentRecording;

    #endregion

    private void Start()
    {
        chromaKeyMaterial.SetFloat("_Smoothing", smoothnessSlider.value);
        chromaKeyMaterial.SetFloat("_Sensitivity", sensitivitySlider.value);

        OnAspectRatioChanged();
    }

    public void OnSmoothnessChanged()
    {
        chromaKeyMaterial.SetFloat("_Smoothing", smoothnessSlider.value);
    }

    public void OnSensitivityChanged()
    {
        chromaKeyMaterial.SetFloat("_Sensitivity", sensitivitySlider.value);
    }

    public void OnStartStopCamera()
    {
        CameraDisplay.StartStopCamera(cameraDisplay, startStopText);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void OnSwapCamera()
    {
        CameraDisplay.SwapCamera(cameraDisplay, startStopText);
    }

    public void OnSelectDataPath()
    {
        MediaCapture.dataPath = EditorUtility.OpenFolderPanel("Select folder", "", "");
    }

    public void OnStartRecording()
    {
        currentRecording = StartCoroutine(MediaCapture.Record(25));
    }

    public void OnSaveRecording()
    {
        StopCoroutine(currentRecording);
        MediaCapture.SaveRecording();
    }

    public void OnAspectRatioChanged()
    {
        // TODO: Change the aspect Ratio of the environment Texture without scaling it

        switch (aspectRatioDropdown.value)
        {
            case 0:
                cameraDisplay.transform.localScale = new Vector3(1, 1f / 16f * 9f, 1);
                environmentDisplay.transform.localScale = new Vector3(1, 1f / 16f * 9f, 1);
                stroke.transform.localScale = new Vector3(1, 1f / 16f * 9f, 1);
                break;
            case 1:
                cameraDisplay.transform.localScale = new Vector3(1, 1f / 3f * 2f, 1);
                environmentDisplay.transform.localScale = new Vector3(1, 1f / 3f * 2f, 1);
                stroke.transform.localScale = new Vector3(1, 1f / 3f * 2f, 1);
                break;
            case 2:
                cameraDisplay.transform.localScale = new Vector3(1, 1f / 2.4f, 1);
                environmentDisplay.transform.localScale = new Vector3(1, 1f / 2.4f, 1);
                stroke.transform.localScale = new Vector3(1, 1f / 2.4f, 1);
                break;
        }
    }
}
