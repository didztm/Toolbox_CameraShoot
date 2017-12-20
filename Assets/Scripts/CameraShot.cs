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
    public List<Spot> _MyShotSpots=new List<Spot>();

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
        if (GUILayout.Button("ToJson"))
        {
            ToJson("Assets/Json/test.json");
        }
    }
    private void ToJson(string path)
    {
        var stream = new FileStream(path, FileMode.CreateNew);
    }

    private List<Spot> FromJson(string path) {

       string stream = new FileStream(path, FileMode.Open).ToString();

        _MyShotSpots= JsonUtility.FromJson<List<Spot>>(stream);


        return _MyShotSpots;
    }
    private void OnFocus()
    {
        
    }
    #endregion

    #region Tools Debug and Utility
    public List<Spot> DoShot(Transform _curCam)
    {
        if (editorCam.position != _curCam.position && editorCam.rotation != _curCam.rotation)
        {
            editorCam = _curCam;
            Spot spot = new Spot
            {
                position = _curCam.position,
                angle = _curCam.eulerAngles
            };
            _MyShotSpots.Add(spot);
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
