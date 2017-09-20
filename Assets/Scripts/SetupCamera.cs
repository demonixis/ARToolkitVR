using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetupCamera : MonoBehaviour
{
    [SerializeField]
    private RawImage _sceneImage = null;
    [SerializeField]
    private RawImage _videoImage = null;

    private IEnumerator Start()
    {
        for (var i = 0; i < 3; i++)
            yield return new WaitForEndOfFrame();

        var sceneRT = new RenderTexture(1280, 720, 24);
        var videoRT = new RenderTexture(1280, 720, 24);

        var arCamera = GetComponentInChildren<Camera>();
        arCamera.clearFlags = CameraClearFlags.SolidColor;
        arCamera.backgroundColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        arCamera.stereoTargetEye = StereoTargetEyeMask.None;
        arCamera.targetTexture = sceneRT;

        var videoSource0 = GameObject.Find("Video source 0");

        var videoBackground = GameObject.Find("Video background");
        var videoBackgroundCamera = videoBackground.GetComponent<Camera>();
        videoBackgroundCamera.targetTexture = videoRT;
        videoBackgroundCamera.stereoTargetEye = StereoTargetEyeMask.None;
        videoBackgroundCamera.allowHDR = false;
        videoBackgroundCamera.allowMSAA = false;
        videoBackgroundCamera.rect = new Rect(0.13f, 0.25f, 0.75f, 0.5f);
        videoBackgroundCamera.clearFlags = CameraClearFlags.SolidColor;
        videoBackgroundCamera.backgroundColor = Color.black;
        videoBackgroundCamera.rect = arCamera.rect;

        _sceneImage.texture = sceneRT;
        _videoImage.texture = videoRT;
    }
}
