using UnityEngine;
using System.Collections;
//리스트를 위한 namespace
using System.Collections.Generic;
using UnityEngine.UI;

	//------수정 시간: 2016년 10월 30일--------//


public class UIChat : MonoBehaviour
{
	//--------------FIELD---------------------//

	[Header("플레이어 메시지 프리팹")]
	[SerializeField] private UIChatMessageItem _messageItemPlayerPrefab;
	[Header("NPC 메시지 프리팹")]
	[SerializeField] private UIChatMessageItem _messageItemOtherPrefab;

	[Header("메시지를 자식으로 두는 레이아웃 프리팹")]
    [SerializeField] private VerticalLayoutGroup _vLayout;

	[Header("내가 쓰는 부분을 생성하는 InputField")]
	[SerializeField] private InputField _inputField;

	[Header("_bot.Reply를 사용하기 위한 class형")]
	[SerializeField] private UIChatBot _bot;

	[Header("플레이어 메시지 스트링")]
	public string[] Player_Messages = new string[3];

	private List<UIChatMessageItem> _messageItems;

	//------------EVENTMETHOD-----------------//
	void Start()
	{
		//CSVReader 클래스가 test2를 읽었을 때의 Dictionary값을 가져옵니다.
		List<Dictionary<string, object>> Dic_CSV_Data = CSVReader.Read("test2");

		for (int i = 0; i < 3; i++)
		{
			Player_Messages[i] = Dic_CSV_Data[i+10]["Log"].ToString();
		}
	}
	
	//--------------METHOD--------------------//
	/// <summary>
	/// Input field가 공란이 아닐 때 쓰인 메시지를 보냅니다.
	/// </summary>
	private void SendMessage()
	{
		string message = _inputField.text;

		if (!string.IsNullOrEmpty(message))
		{
			AddMessage(message, true);

			_inputField.text = "";
		}
	}
	#region UI EVENT Send Message      
	/// <summary>
	/// UI이벤트: 메시지 보내기 버튼이 부르는 메소드
	/// </summary>
	public void OnClickSendMessageButton()
	{
		SendMessage();
	}
	#endregion

	/// <summary>
	/// 메시지 아이템에 UI를 추가합니다.
	/// </summary>
	/// <param name="message">추가할 메시지</param>
	/// <param name="playerMessage">플레이어에게서 온 메시지인지 아닌지</param>
	public void AddMessage(string message, bool playerMessage)
    {
        UIChatMessageItem messageItem =  CreateMessageItem(_vLayout, playerMessage);
        messageItem.SetMessage(message);

		//_messageItems이 null일때 초기화
		if (_messageItems == null)
            _messageItems = new List<UIChatMessageItem>();

        _messageItems.Add(messageItem);

        if (playerMessage)
            _bot.Reply(message);
    }

	/// <summary>
	/// 메시지를 생성하고 레이아웃에 넣는 함수
	/// </summary>
	/// <param name="vLayout">메시지 항목을 추가하기 위한 The Vertical Layout .</param>
	/// <param name="playerMessage">플레이어에게서 온 메시지인지 아닌지.</param>
	/// <returns></returns>
	private UIChatMessageItem CreateMessageItem(VerticalLayoutGroup vLayout, bool playerMessage)
    {
		//플레이어가 보내는거면 _messageItemPlayerPrefab.gameObject, 
		//플레이어가 보내는게 아니면 _messageItemOtherPrefab.gameObject를 prefab에 넣는다.
		GameObject prefab = playerMessage ? _messageItemPlayerPrefab.gameObject : _messageItemOtherPrefab.gameObject;

		//위의 prefab을 position rotation 0 0 0으로 하는 instance를 생성한다.
        GameObject instance = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);

		//생성한 instance의 부모를 transform형 vLayout을 둡니다.
        instance.transform.SetParent(vLayout.transform);
		//instance의 localScale을 1,1,1로 맞춥니다.
        instance.transform.localScale = Vector3.one;
        instance.SetActive(true);

		//instance가 가지고 있는 UIChatMessageItem 컴포넌트를 반환합니다.
		return instance.GetComponent<UIChatMessageItem>();
    }
}
