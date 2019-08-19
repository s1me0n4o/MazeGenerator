using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Canvas restartCanvas;

    [Header("Inputs")]
    [SerializeField]
    private InputField inputRows;
    [SerializeField]
    private InputField inputCols;
    [SerializeField]

    [Header("Buttons")]
    private GameObject bl;
    [SerializeField]
    private Text error;

    private int intRows;
    private int intCols;

    private InputField rows;
    private InputField cols;

    private MazeBuilder mb;

    private Camera mainCamera;

    private void Start()
    {
        mb = GetComponent<MazeBuilder>();
        mainCamera = Camera.main;
    }

    public void HuntAndKillGameStart()
    {
        GameStart(1);
    }

    public void GrowingTreeAlg()
    {
        GameStart(2);
    }

    public void GameStart(int algorithmChoise)
    {

        if (int.TryParse(rows.text, out intRows) && int.TryParse(cols.text, out intCols))
        {
            canvas.GetComponent<Canvas>().enabled = false;
            restartCanvas.gameObject.SetActive(true);
            mb.BuildBasicMaze(algorithmChoise, intRows, intCols);
            MainCameraPos();
        }
        else
        {
            error.gameObject.SetActive(true);
            Debug.Log("Convert to Int failed");
        }

    }

    public void GameRestart()
    {
        //GrowingTree mb = GetComponent<GrowingTree>().mazeInstance;
        //StopAllCoroutines();
        //if (mb.mazeInstance != null)
        //{
        //    Destroy(mb.mazeInstance.gameObject);
        //}
        SceneManager.LoadScene(0);
    }

    public void SetMazeSize()
    {
        rows = inputRows;
        cols = inputCols;

        if (int.TryParse(rows.text, out intRows) && int.TryParse(cols.text, out intCols))
        {
            error.gameObject.SetActive(false);
            bl.GetComponent<ButtonLockers>().EnableButoons();
        }
        else
        {
            error.gameObject.SetActive(true);
            Debug.Log("Convert to Int failed");
        }

    }

    private void MainCameraPos()
    {
        if (int.TryParse(rows.text, out intRows) && int.TryParse(cols.text, out intCols))
        {
            float frows = float.Parse(rows.text);
            float fcols = float.Parse(cols.text);

            float xpos = fcols * 6 * 0.38f;
            float ypos = 12;
            float zpos = frows * 6 * 0.5f;

            mainCamera.transform.position = new Vector3(xpos, ypos, zpos);
            // cell width(6) * maze width(cols * 6) / rows * 6 * 0.5
                float sum = ((6f * fcols * 6f) / (frows * 6f * 0.5f));
            mainCamera.orthographicSize = sum;
        }
        else
        {
            error.gameObject.SetActive(true);
            Debug.Log("Convert to Int failed");
        }
    }
}
