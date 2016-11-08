﻿using UnityEngine;
using System.Collections;
//리스트를 위한 namespace
using System.Collections.Generic;
using UnityEngine.UI;

//------수정 시간: 2016년 11월 07일--------//

public class UIChat : MonoBehaviour
{
    //--------------FIELD---------------------//
    public int logLength = 45;


    [Header("플레이어 메시지 프리팹")]
    [SerializeField]
    private UIChatMessageItem _messageItemPlayerPrefab;
    [Header("NPC 메시지 프리팹")]
    [SerializeField]
    private UIChatMessageItem _messageItemOtherPrefab;
    [Header("분기 프리팹")]
    [SerializeField]
    private UISelectionGroup _messageItemRamifyPrefab;

    private bool ramifyA_Selected;
    private bool ramifyB_Selected;
    private bool ramifyC_Selected;
    private string selectedARamifystring;
    private string selectedBRamifystring;
    private string selectedCRamifystring;

    private GameObject RamifyA;

    [Header("메시지를 자식으로 두는 레이아웃 프리팹")]
    [SerializeField]
    private VerticalLayoutGroup _vLayout;

    [Header("내가 쓰는 부분을 생성하는 InputField")]
    [SerializeField]
    private InputField _inputField;

    private string[] Message_Log;
    private string[] Message_Log2;
    private string[] Message_Log3;
    private string[] Message_Name;
    private string[] Message_IsRamify;

    private int startLog;

    private int TextCount;
    private List<UIChatMessageItem> _messageItems = new List<UIChatMessageItem>();
    private List<UISelectionGroup> _messageItemsR = new List<UISelectionGroup>();

    //------------EVENTMETHOD-----------------//
    void Start()
    {
        InitializeList();
        startLog = 0;

        //선택지 초기화
        ramifyA_Selected = true;
        ramifyB_Selected = false;
        ramifyC_Selected = false;
        //몇번째 대사인지 확인하기 위한 카운트값 초기화
        TextCount = 0;
        //CSV 파일 불러오는 메소드 실행
        ReadCsvFile();
        StartCoroutine(Co_AddMessage());
    }
    //--------------METHOD--------------------//
    IEnumerator Co_AddMessage()
    {
        for (int i = startLog; i < 200; i++)
        {
            if (ramifyA_Selected)
            {
                yield return new WaitForSeconds(0.2f);
                //주인공이 말하고 분기가 아닐 때
                if ((Message_Name[i].ToString() == "도련") && (Message_IsRamify[i].ToString() == "FALSE"))
                {
                    //주인공쪽(파란색)으로 로그를 생성
                    AddMessage(Message_Log[i], true);
                    print("Name::도련 ," + "IsRamify::" + Message_IsRamify[i] + ", Log:: " + i + " ::" + Message_Log[i]);
                }
                //주인공이 말하고 분기일 때
                if ((Message_Name[i].ToString() == "도련") && (Message_IsRamify[i].ToString() == "TRUE"))
                {
                    //분기용 함수 생성
                    AddMessageForRamify(Message_Log[i], Message_Log2[i], Message_Log3[i]);
                    print("Name::도련 ," + "IsRamify::" + Message_IsRamify[i] + ", Log:: " + i + " ::" + Message_Log[i]);
                    print("Name::도련 ," + "IsRamify::" + Message_IsRamify[i] + ", Log:: " + i + " ::" + Message_Log2[i]);
                    print("Name::도련 ," + "IsRamify::" + Message_IsRamify[i] + ", Log:: " + i + " ::" + Message_Log3[i]);
                    ramifyA_Selected = false;
                }
                //NPC가 말할 때
                if (Message_Name[i].ToString() == "서린")
                {
                    //NPC쪽(회색)쪽으로 로그를 생성
                    AddMessage(Message_Log[i], false);
                    print("Name::서린 ," + "IsRamify::" + Message_IsRamify[i] + ", Log:: " + i + " ::" + Message_Log[i]);
                }
                startLog++;
            }
        }
    }
    public void AddMessageForRamify(string message, string message1, string message2)
    {
        if (Message_IsRamify[TextCount].ToString() == "TRUE")
        {
            UISelectionGroup messageItemR = CreateRamifyMessageItem(_vLayout);
            messageItemR.SetText(message, message1, message2);
            selectedARamifystring = message;
            selectedBRamifystring = message1;
            selectedCRamifystring = message2;
            _messageItemsR.Add(messageItemR);
        }
    }

    public void AddMessage(string message, bool playerMessage)
    {
        //선택지가 아닐 경우에 
        if (Message_IsRamify[TextCount] == null)
        {
            print("Message_IsRamify[TextCount]가 없습니다!");
        }
        else if (Message_IsRamify[TextCount].ToString() == "FALSE")
        {
            UIChatMessageItem messageItem = CreateMessageItem(_vLayout, playerMessage);
            messageItem.SetMessage(message);
            _messageItems.Add(messageItem);
        }
    }

    private UISelectionGroup CreateRamifyMessageItem(VerticalLayoutGroup vLayout)
    {
        print("분기가 발생.CreateRamifyMessageItem를 실행합니다");
        GameObject prefab = _messageItemRamifyPrefab.gameObject;

        GameObject instance = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
        //생성한 instance의 부모를 transform형 vLayout을 둡니다.
        instance.transform.SetParent(vLayout.transform);

        //instance의 localScale을 1,1,1로 맞춥니다.
        instance.transform.localScale = Vector3.one;
        instance.SetActive(true);
        TextCount++;
        //UISelectionGroup 컴포넌트를 반환
        return instance.GetComponent<UISelectionGroup>();
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
        TextCount++;

        //instance가 가지고 있는 UIChatMessageItem 컴포넌트를 반환합니다.
        return instance.GetComponent<UIChatMessageItem>();
    }
    private void RemoveAllRamify()
    {
        //hand = GameObject.Find("Monster/Arm/Hand");
    }
    public void SelectRamifyA()
    {
        ramifyA_Selected = true;
        AddMessage(selectedARamifystring, true);
        StartCoroutine(Co_AddMessage());
    }
    public void SelectRamifyB()
    {
        ramifyA_Selected = true;
        AddMessage(selectedBRamifystring, true);
        StartCoroutine(Co_AddMessage());
    }
    public void SelectRamifyC()
    {
        ramifyA_Selected = true;
        AddMessage(selectedCRamifystring, true);
        StartCoroutine(Co_AddMessage());
    }
    private void ReadCsvFile()
    {
        //CSVReader 클래스가 test2를 읽었을 때의 Dictionary값을 가져옵니다.
        List<Dictionary<string, object>> Dic_CSV_Data = CSVReader.Read("test2");

        //CSV 파일에서 Log, Name, IsRamify 3개를 20개씩 불러옵니다.
        for (int i = 0; i < 45; i++)
        {
            Message_Log[i] = Dic_CSV_Data[i]["Log1"].ToString();
            Message_Log2[i] = Dic_CSV_Data[i]["Log2"].ToString();
            Message_Log3[i] = Dic_CSV_Data[i]["Log3"].ToString();
            Message_Name[i] = Dic_CSV_Data[i]["Name"].ToString();
            Message_IsRamify[i] = Dic_CSV_Data[i]["IsRamify"].ToString();
        }
    }
    private void InitializeList()
    {
        Message_Log = new string[logLength];
        Message_Log2 = new string[logLength];
        Message_Log3 = new string[logLength];
        Message_Name = new string[logLength];
        Message_IsRamify = new string[logLength];

        //리스트 초기화
        if (_messageItemsR == null)
            _messageItemsR = new List<UISelectionGroup>();
        if (_messageItems == null)
            _messageItems = new List<UIChatMessageItem>();
    }
}
