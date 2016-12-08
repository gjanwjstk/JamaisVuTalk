using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class UISceneLoader : MonoBehaviour
{
    public List<GameObject> canvasList = new List<GameObject>(5);
    //ChatList를 로드합니다.
    void Awake()
    {
        //Title 띄우기
        canvasList[0].SetActive(true);
    }
    public void TapTitleImage()
    {
        SetCanvas(1);
    }
    public void TapOptionBtn()
    {
        SetCanvas(3);
    }
    public void TapChatBtn()
    {
        SetCanvas(4);
    }
    public void TapChatListBtn()
    {
        SetCanvas(2);
    }
    void SetCanvas(int activeCanvasNum)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == activeCanvasNum) continue;
            canvasList[i].SetActive(false);
        }
        canvasList[activeCanvasNum].SetActive(true);
    }
}
