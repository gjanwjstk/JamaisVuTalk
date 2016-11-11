using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//------수정 시간: 2016년 10월 19일--------//

public class CSVPrinter : MonoBehaviour
{
	//--------------FIELD---------------------//
	[Header("주의사항 :: CSV는 인코딩을 UTP-8로 해주세요!")]
	public bool DontTouch;

	public Text ChatName;
	public Text ChatText;

	//------------EVENTMETHOD-----------------//
	void Start()
	{
		List<Dictionary<string, object>> Dic_CSV_Data = CSVReader.Read("test2");
		for (var i = 0; i < Dic_CSV_Data.Count; i++)
		{
		//	Debug.Log("index" + (i).ToString() + ": " + Dic_CSV_Data[i]["Log"]);
		}
		
			ChatText.text = Dic_CSV_Data[10]["Log"].ToString().Insert(10, "\n");
		print("10의 스트링"+ChatText.text.ToString()[10]);
			ChatText.text = Dic_CSV_Data[10]["Log"].ToString().Insert(20, "\n");
		print("20의 스트링" + ChatText.text.ToString()[20]);


		ChatName.text = Dic_CSV_Data[0]["Name"].ToString();

	}
	//--------------METHOD--------------------//
	void SetCountText()
	{
		
		
	}
}
