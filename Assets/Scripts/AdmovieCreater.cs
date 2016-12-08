using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AdmovieCreater : MonoBehaviour
{
    private bool isClickClockBtn;
    public Text disPlayLeftTimeText;
    public GameObject clockImage;
    private float _LeftTime;
    void Awake()
    {
        _LeftTime = 5f;
        isClickClockBtn = false;
    }
    void Update()
    {
        if (isClickClockBtn)
        {
            _LeftTime -= Time.deltaTime;
            disPlayLeftTimeText.text = string.Format("{0:F0}", _LeftTime) + "초 후 시간이 변경됩니다.";
            if(_LeftTime <0)
            {
                isClickClockBtn = false;
                _LeftTime = 30f;
                clockImage.gameObject.SetActive(true);
                disPlayLeftTimeText.gameObject.SetActive(false);
            }
        }
    }
    public void ClickClockBtn()
    {
        isClickClockBtn = true;
        disPlayLeftTimeText.gameObject.SetActive(true);
        clockImage.SetActive(false);
    }
}
