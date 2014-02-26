using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public static SceneManager Instance;

    public Transform redCell;
    public Transform blueCell;
    public Transform spawnPointTopLeft;
    public Transform spawnPointBottomLeft;
    public Transform spawnPointTopRight;

    public GameObject mainArea;
    public GameObject destroyer;
    public GameObject Gate1;
    public GameObject Gate2;
    public GameObject Gate3;

    public int gridX = 15;
    public int gridY = 20;
    public int fontSize;
    public GUISkin guiSkin;
    
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var clone = Instantiate(redCell, spawnPointBottomLeft.position, Quaternion.LookRotation(new Vector3(1f, 0f, 0f))) as Transform;
            clone.parent = this.transform;

            clone = Instantiate(redCell, spawnPointTopLeft.position, Quaternion.LookRotation(new Vector3(1f, 0f, 0f))) as Transform;
            clone.parent = this.transform;

            clone = Instantiate(blueCell, spawnPointTopRight.position, Quaternion.LookRotation(new Vector3(1f, 0f, 0f))) as Transform;
            clone.parent = this.transform;
        }
	}

    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUILayout.BeginArea(new Rect(0f, 0f, Screen.width, Screen.height));
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        for (int i = 0; i < gridX; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            for (int j = 0; j < gridY; j++)
            {
                GUILayout.Button(i + "," + j, GUILayout.Width(Screen.width / gridY), GUILayout.Height(Screen.height / gridX));
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
