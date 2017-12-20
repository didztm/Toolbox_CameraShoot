using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CameraSpot 
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
        string json = "";
        /*foreach (Spot s in _MyShotSpots)
        {
           json += JsonUtility.ToJson(s);
        }*/
        json = JsonUtility.ToJson(_MyShotSpots);
        
        StreamWriter file = new StreamWriter( path);
        file.Write(json);
        file.Close();
    }

    private List<Spot> FromJson(string path)
    {

        string stream = new FileStream(path, FileMode.Open).ToString();
        _MyShotSpots = JsonUtility.FromJson<List<Spot>>(stream);
        return _MyShotSpots;
    }
    #endregion



}
