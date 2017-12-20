using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraShot : EditorWindow
{
    #region Private and Protected Members

    #endregion
    #region Public Members

    #endregion
    public Transform editorCam;

    #region Public void

    #endregion

    #region System
    [MenuItem("Toolbox/CamShot")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CameraShot));
    }
    private void OnGUI()
    {
        editorCam = GetEditorCamera(SceneView.GetAllSceneCameras());

        if (GUILayout.Button("Shot"))
        {
            DoShot();
        }
    }
    private void OnFocus()
    {
        
    }
    #endregion

    #region Tools Debug and Utility
    public Transform DoShot()
    {
        return editorCam;
    }
    private Transform GetEditorCamera(Camera[] _cams)
    {
        
        foreach (Camera c in _cams)
        {
            if (c.cameraType == CameraType.SceneView)
            {
                return c.transform;
            }
        }

        return null;
    }
    #endregion


}
