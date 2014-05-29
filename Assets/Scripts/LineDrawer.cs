using UnityEngine;
using System.Collections;

public class LineDrawer : MonoBehaviour {

    public static LineDrawer Instance;

    public LineRenderer[] lineRenderers;
    public Material lineColor;
    public bool drawLines = false;

	// Use this for initialization
	void Start () {
        Instance = this;

        lineRenderers = new LineRenderer[SceneManager.Instance.gridX * SceneManager.Instance.gridY];

        for (int i = 0; i < lineRenderers.Length; i++)
        {
            var go = new GameObject();
            go.name = "LineRenderer " + i;
            go.transform.parent = this.transform;
            go.AddComponent<LineRenderer>();
            lineRenderers[i] = go.GetComponent<LineRenderer>();
            lineRenderers[i].SetWidth(0.1f, 0.01f);
            lineRenderers[i].material = lineColor;
        }

        float x = Screen.width / SceneManager.Instance.gridY / 2;
        float y = Screen.height / SceneManager.Instance.gridX / 2;
        int index = 0;
        for (int i = 0; i < SceneManager.Instance.gridX; i++)
        {
            for (int j = 0; j < SceneManager.Instance.gridY; j++)
            {
                SceneManager.Instance.vectorStart[index] = new Vector2(x, y);
                if (drawLines)
                {
                    LineDrawer.Instance.lineRenderers[index].SetPosition(0, Camera.main.ScreenToWorldPoint(new Vector3(x, Screen.height - y, 10f)));
                    LineDrawer.Instance.lineRenderers[index].SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(SceneManager.Instance.vectorfield[index].x, Screen.height - SceneManager.Instance.vectorfield[index].y, 10)));
                }
                x += Screen.width / SceneManager.Instance.gridY;
                index++;
            }

            x = Screen.width / SceneManager.Instance.gridY / 2;
            y += Screen.height / SceneManager.Instance.gridX;
        }
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
