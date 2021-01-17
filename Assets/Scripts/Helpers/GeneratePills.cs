using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

public class GeneratePills : EditorWindow {

    GameObject pill;
    Vector2 dirs;
    float stepSize;
    List<GameObject> pills = new List<GameObject>();

    [MenuItem("comp280-unity-ai/GeneratePills")]
    private static void ShowWindow() 
    {
        var window = GetWindow<GeneratePills>();
        window.titleContent = new GUIContent("GeneratePills");
        window.Show();
    }

    private void OnGUI() 
    {
        pill = (GameObject)EditorGUILayout.ObjectField(pill, typeof(GameObject));
        dirs = EditorGUILayout.Vector2Field("Dirs", dirs);
        stepSize = EditorGUILayout.FloatField("Step Size", stepSize);

        if (GUILayout.Button("GenPills"))
        {
            while (pills.Count > 0)
            {
                GameObject temp = pills[0];
                DestroyImmediate(temp);
                pills.RemoveAt(0);
            }

            for (float i = 0; i < dirs.x; i += stepSize)
            {
                for (float j = 0; j < dirs.y; j += stepSize)
                {
                    pills.Add(GameObject.Instantiate(pill, new Vector3(i, 0.0f, j), new Quaternion()));
                }
            }
        }
    }
}