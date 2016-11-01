using UnityEngine;
using System.Collections;
//리스트를 사용하기 위한 namespace
using System.Collections.Generic;

	//------수정 시간: 2016년 10월 30일--------//

public class UIChatBot : MonoBehaviour
{
	//--------------FIELD---------------------//
	

	//UIChat.AddMessage 메소드를 사용하기 위한 _chat
	[SerializeField] private UIChat _chat;

	[Header("NPC 메시지 스트링")]
	public string[] Npc_Messages = new string[5];

	//메시지를 몇개나 보냈는지 확인하기 위한 int값 정의
    private int _currentMessage = -1;

    private bool offline;

	//------------EVENTMETHOD-----------------//
	void Start()
	{
		//CSVReader 클래스가 test2를 읽었을 때의 Dictionary값을 가져옵니다.
		List<Dictionary<string, object>> Dic_CSV_Data = CSVReader.Read("test2");

		for (int i = 0; i < 5; i++)
		{
			Npc_Messages[i] = Dic_CSV_Data[i+5]["Log"].ToString();
		}
	}

	//--------------METHOD--------------------//
	void Reply01()
	{
		Reply(Npc_Messages[0]);
	}
	void Reply02()
	{
		Reply(Npc_Messages[1]);
	}
	void Reply03()
	{
		Reply(Npc_Messages[2]);
	}
	void Reply04()
	{
		Reply(Npc_Messages[3]);
	}
	/// <summary>
	/// Add a new message to the chat from the bot.봇이 가지고 
	/// </summary>
	/// <param name="message"></param>
	public void Reply(string message)
    {
			if (_currentMessage < 4)
				_currentMessage++;
            
            StartCoroutine(CoReply(Npc_Messages[_currentMessage]));
    }

    /// <summary>
    /// Wait a random time before sending the reply.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private IEnumerator CoReply(string message)
    {
        yield return new WaitForSeconds(1f);

        _chat.AddMessage(message, false);	
    }
	//public void 백업Reply(string message)
	//{
	//	if (!offline)
	//	{
	//		//_currentMessage = -1, 4보다 작을 때
	//		if (_currentMessage < 4)
	//		{
	//			//현재 int값 추가
	//			_currentMessage++;
	//		}
	//		else
	//		{
	//			//_currentMessage가 4고, 플레이어가 'hi'라고 말할 떄
	//			if (message.ToLower() == "hi")
	//			{
	//				//int 메시지 값 추가
	//				_currentMessage++;
	//				//상대 NPC가 답장을 하지 않게 된다.
	//				offline = true;
	//			}
	//		}

	//		StartCoroutine(CoReply(Npc_Messages[_currentMessage]));
	//	}
	//}
}
