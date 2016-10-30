using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIChatMessageItem : MonoBehaviour
{
    [SerializeField] private Text _labelMessage;

    /// <summary>
    /// Set the message label text.
    /// </summary>
    /// <param name="message"></param>
    public void SetMessage(string message)
    {
        _labelMessage.text = message;
    }
}