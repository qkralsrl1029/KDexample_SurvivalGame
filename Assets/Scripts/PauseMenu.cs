using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_Base;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.isPause)
                CloseMenu();
            else
                CallMenu();
        }
    }

    private void CallMenu()
    {
        GameManager.isPause = true;
        GameManager.canPlayerMove = false;
        go_Base.SetActive(true);
        Time.timeScale = 0;
    }

    private void CloseMenu()
    {
        GameManager.isPause = false;
        GameManager.canPlayerMove = true;
        go_Base.SetActive(false);
        Time.timeScale = 1;
    }

    public void Save()
    {

    }

    public void Load()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
