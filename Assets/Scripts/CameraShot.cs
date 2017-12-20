using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CameraShot : EditorWindow
{
    #region Private and Protected Members

    #endregion
    #region Public Members

    #endregion
    public Transform editorCam;
    public List<Transform> _MyShotSpots=new List<Transform>();

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

        if (GUILayout.Button("Shot"))
        {
            Transform curCam = GetEditorCamera(SceneView.GetAllSceneCameras());
            editorCam = curCam;
            DoShot( curCam);
            
            
        }
    }
    private void ToJson()
    {

    }

    private List<Transform> FromJson(string path) {

       string stream = new FileStream(path, FileMode.Open).ToString();

        _MyShotSpots= JsonUtility.FromJson<List<Transform>>(stream);


        return _MyShotSpots;
    }
    private void OnFocus()
    {
        
    }
    #endregion

    #region Tools Debug and Utility
    public List<Transform> DoShot(Transform _curCam)
    {
        if (editorCam.position != _curCam.position && editorCam.rotation != _curCam.rotation)
        {
            editorCam = _curCam;
            _MyShotSpots.Add(editorCam);
        }
        
        return _MyShotSpots;
       
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
