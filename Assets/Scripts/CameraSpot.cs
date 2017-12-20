using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CameraSpot : EditorWindow
{

    public List<Spot> _MyShotSpots = new List<Spot>();

    [MenuItem("Toolbox/CamShot")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CameraSpot));
    }

    private void OnGUI()
    {

        if (GUILayout.Button("Shot"))
        {
            Transform curCam = GetEditorCamera(SceneView.GetAllSceneCameras());

            DoShotSpot(curCam);

        }
        if (GUILayout.Button("Save as Json"))
        {
            ToJson("Assets/Json/test.json");
        }
        if (GUILayout.Button("Load as Json"))
        {
            FromJson("Assets/Json/test.json");
        }
        ShowSpot();
    }
    private void ShowSpot()
    {
        foreach (Spot s in _MyShotSpots) {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Vector3Field("Position", s.position);
            EditorGUILayout.Vector3Field("Euler angle", s.angle);
            EditorGUILayout.EndHorizontal();


        }

    }

    #region Tools Debug and Utility
    public List<Spot> DoShotSpot(Transform _curCam)
    {
        Spot spot = new Spot
        {
            position = _curCam.position,
            angle = _curCam.eulerAngles
        };

        _MyShotSpots.Add(spot);

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
    private void ShowListSpot() {

        foreach (Spot s in _MyShotSpots)
        {
            Debug.Log(s.angle + " " + s.position);
        }


    }
    //Utilisé pour permettre d'accéder aux listes dans json
    [Serializable]
    private class ListWrapper
    {
        public List<Spot> spot;
    }
    


    private void ToJson(string path)
    {
        Debug.Log( _MyShotSpots.Count);
        string json = "";
        ListWrapper w = new ListWrapper
        {
            spot = _MyShotSpots
        };
        json = JsonUtility.ToJson(w);
        StreamWriter file = new StreamWriter( path);
        file.Write(json);
        file.Close();
    }

    private List<Spot> FromJson(string path)
    {
        string text = System.IO.File.ReadAllText(path);
        _MyShotSpots = JsonUtility.FromJson<ListWrapper>(text).spot;
        
        return _MyShotSpots;
    }
    #endregion



}
