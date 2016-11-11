using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UISceneLoader : MonoBehaviour
{
    public void LoadFrindScene()
    {
        StartCoroutine(StartGameProcess());
    }
    IEnumerator StartGameProcess()
    {
        yield return new WaitForSeconds(1f);
        //다 끝날때까지 게임은 게임대로 돌아가고 로딩은 로딩대로
        //로딩스크린 만드는 용도로 사용
        AsyncOperation operation = SceneManager.LoadSceneAsync("04.GamePlay", LoadSceneMode.Additive);

        yield return operation;

        GameObject.Find("Canvas").transform.Find("Curtain").gameObject.SetActive(true);

        yield return new WaitForEndOfFrame();

        SceneManager.UnloadScene("03.Start");
        //setactivescene 은 활성화 비활성화
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("04.GamePlay"));

    }
}
