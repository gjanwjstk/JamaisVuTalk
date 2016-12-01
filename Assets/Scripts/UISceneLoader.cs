using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UISceneLoader : MonoBehaviour
{
    private bool isLoadFriendScene;
    void Awake()
    {
        isLoadFriendScene = false;
    }
    public void LoadFriendScene()
    {
        if(!isLoadFriendScene)
        {
        StartCoroutine(StartGameProcess());
            isLoadFriendScene = true;
        }
    }
    public void LoadFrindSceneMaybe()
    {
        
    }
    IEnumerator StartGameProcess()
    {
        yield return new WaitForSeconds(1f);
        //다 끝날때까지 게임은 게임대로 돌아가고 로딩은 로딩대로
        //로딩스크린 만드는 용도로 사용
        AsyncOperation operation = SceneManager.LoadSceneAsync("Friend", LoadSceneMode.Additive);

        yield return operation;
        yield return new WaitForEndOfFrame();

        //setactivescene 은 활성화 비활성화
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Friend"));
    }
}
