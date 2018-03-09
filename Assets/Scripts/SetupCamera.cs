using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public sealed class SetupCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject m_XRPlayer = null;
    [SerializeField]
    private RawImage m_SceneImage = null;
    [SerializeField]
    private RawImage m_VideoImage = null;

#if UNITY_EDITOR
    [Header("Editor")]
    [SerializeField]
    [Tooltip("Forces the preview even if VR is not enabled")]
    private bool m_ForcePreview = false;
#endif

    private IEnumerator Start()
    {
        var disable = !XRSettings.enabled;
#if UNITY_EDITOR
        if (m_ForcePreview)
            disable = false;
#endif

        if (disable)
        {
            if (m_XRPlayer != null)
                m_XRPlayer.SetActive(false);

            Destroy(this);
            yield break;
        }

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

        m_SceneImage.texture = sceneRT;
        m_VideoImage.texture = videoRT;
    }
}
