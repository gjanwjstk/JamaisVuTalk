using UnityEngine;
using System.Collections;
//리스트를 사용하기 위한 namespace
using System.Collections.Generic;

	//------수정 시간: 2016년 10월 28일--------//

public class UIChatBot : MonoBehaviour
{
	//--------------FIELD---------------------//
	

	//UIChat.AddMessage 메소드를 사용하기 위한 _chat
	[SerializeField] private UIChat _chat;

	//Bot이 말하는 내용을 담는 string 배열
    public string[] Messages = new string[4];

	//메시지를 몇개나 보냈는지 확인하기 위한 int값 정의
    private int _currentMessage = -1;

    private bool offline;

	//------------EVENTMETHOD-----------------//
	void Start()
	{
		//CSVReader 클래스가 test2를 읽었을 때의 Dictionary값을 가져옵니다.
		List<Dictionary<string, object>> Dic_CSV_Data = CSVReader.Read("test2");

		for (int i = 0; i < 4; i++)
		{
			Messages[i] = Dic_CSV_Data[i]["Log"].ToString();
		}
	}

	//--------------METHOD--------------------//

	/// <summary>
	/// Add a new message to the chat from the bot.봇이 가지고 
	/// </summary>
	/// <param name="message"></param>
	public void Reply(string message)
    {
        if (!offline)
        {
			//_currentMessage = -1, 4보다 작을 때
			if (_currentMessage < 4)
            {
				//현재 int값 추가
                _currentMessage++;
            }
            else
            {
				//_currentMessage가 4고, 플레이어가 'hi'라고 말할 떄
				if (message.ToLower() == "hi")
                {
					//int 메시지 값 추가
                    _currentMessage++;
					//상대 NPC가 답장을 하지 않게 된다.
                    offline = true;
                }
            }

            StartCoroutine(CoReply(Messages[_currentMessage]));
        }
    }

    /// <summary>
    /// Wait a random time before sending the reply.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private IEnumerator CoReply(string message)
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

        _chat.AddMessage(message, false);	
    }
}
