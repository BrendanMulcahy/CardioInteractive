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
    public bool viewGrid = false;

    public Vector2[] vectorStart;
    public Vector2[] vectorfield;

    private bool drawingMode;
    private int indexOfButton;
    private Vector2 startPosition;
    public LineRenderer lineRenderer;

    private float timeElapsed = 0;
    
	// Use this for initialization
	void Start () {
        Instance = this;
        //vectorfield = new Vector2[gridX * gridY];
	}

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) || timeElapsed > 1)
        {
            var clone = Instantiate(redCell, spawnPointBottomLeft.position, Quaternion.LookRotation(new Vector3(1f, 0f, 0f))) as Transform;
            clone.parent = this.transform;

            clone = Instantiate(redCell, spawnPointTopLeft.position, Quaternion.LookRotation(new Vector3(1f, 0f, 0f))) as Transform;
            clone.parent = this.transform;

            clone = Instantiate(blueCell, spawnPointTopRight.position, Quaternion.LookRotation(new Vector3(1f, 0f, 0f))) as Transform;
            clone.parent = this.transform;
            timeElapsed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            viewGrid = !viewGrid;
        }

        if (drawingMode)
        {
            lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(new Vector3(startPosition.x, Screen.height - startPosition.y, 10f)));
            lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));

            if (Input.GetMouseButtonDown(0))
            {
                drawingMode = false;
                Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                vectorfield[indexOfButton] = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                LineDrawer.Instance.lineRenderers[indexOfButton].SetPosition(1, new Vector3(temp.x, temp.y, 0f));

                lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)));
                lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)));
            }
        }
	}

    void OnGUI()
    {
        if (viewGrid)
        {
            GUI.skin = guiSkin;
            GUILayout.BeginArea(new Rect(0f, 0f, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            float x = Screen.width / gridY / 2;
            float y = Screen.height / gridX / 2;
            int index = 0;

            for (int i = 0; i < gridX; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                for (int j = 0; j < gridY; j++)
                {
                    if (!drawingMode && GUILayout.Button(i + "," + j, GUILayout.Width(Screen.width / gridY), GUILayout.Height(Screen.height / gridX)))
                    {
                        drawingMode = true;
                        indexOfButton = index;
                        startPosition = new Vector2(x, y);
                        LineDrawer.Instance.lineRenderers[index].SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(x, Screen.height - y, 10f)));
                    }
                    GUILayout.FlexibleSpace();
                    x += Screen.width / gridY;
                    index++;
                }
                GUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();

                x = Screen.width / gridY / 2;
                y += Screen.height / gridX;
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
