using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName="demoScene_free";
    [SerializeField] Texture2D cursorImg;

    private void Start()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void ClickStart()
    {
        SceneManager.LoadScene(sceneName);
    }
}

