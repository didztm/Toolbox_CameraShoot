using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CameraSpot :EditorWindow
{
 
    public List<Spot> _MyShotSpots=new List<Spot>();

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

            DoShotSpot( curCam);
           
        }
        if (GUILayout.Button("ToJson"))
        {
            ToJson("Assets/Json/test.json");
        }
        if (GUILayout.Button("FromJson"))
        {
            FromJson("Assets/Json/test.json");
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

        foreach(Spot s in _MyShotSpots)
        {
            Debug.Log(s.angle +" " +s.position);
        }
        

    }
    private void ToJson(string path)
    {
         Debug.Log( _MyShotSpots.Count);
        string json = "{\"Spot\":[";
         for (int i=0; i < _MyShotSpots.Count; i++)
         {
            json += JsonUtility.ToJson(_MyShotSpots[i]);
            if (!(i == _MyShotSpots.Count-1)) {
                json += ",";
            }
            
         }
        json += "]}";
        StreamWriter file = new StreamWriter( path);
        file.Write(json);
        file.Close();
    }

    private List<Spot> FromJson(string path)
    {

        string text = System.IO.File.ReadAllText(path);

        _MyShotSpots = JsonUtility.FromJson<List<Spot>>(text);
        Debug.Log( _MyShotSpots.Count);
        return _MyShotSpots;
    }
    #endregion



}
