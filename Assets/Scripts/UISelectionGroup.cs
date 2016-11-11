using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISelectionGroup : MonoBehaviour {

	public Text[] _labelMessage;
	
	public void SetText(string selection1, string selection2, string selection3)
	{
		_labelMessage[0].text = selection1;
		_labelMessage[1].text = selection2;
		_labelMessage[2].text = selection3;
	}
}
