using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler,  IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;
    [SerializeField]
    ProgressBarCircle bar;
    [SerializeField]
    private ToolTip theToolTip;

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        theToolTip.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theToolTip.HideToolTip();
    }

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Data)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0) ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(item != null)
            {
                if(item.itemType == Item.ItemType.Data) 
                {
                    switch (item.itemName)
                    {
                        case "예방 수칙": //case "BasicBehavior":
                            GameObject.Find("Canvas").transform.Find("S1DataUI").gameObject.SetActive(true);
                            break;
                        case "증상":  //case "Symptoms":
                            GameObject.Find("Canvas").transform.Find("S1DataUI2").gameObject.SetActive(true);
                            break;
                        case "행동 요령":  //case "Behavioral Tip"
                            GameObject.Find("Canvas").transform.Find("S1DataUI3").gameObject.SetActive(true);
                            break;
                        case "백신 개발의 어려움":  //case "Hard"
                            GameObject.Find("Canvas").transform.Find("S3DataUI").gameObject.SetActive(true);
                            break;
                        case "RNA와 DNA 차이":  //case "Difference"
                            GameObject.Find("Canvas").transform.Find("S3DataUI2").gameObject.SetActive(true);
                            break;
                        case "9가지 이유":  //case "NineReasons"
                            GameObject.Find("Canvas").transform.Find("S3DataUI3").gameObject.SetActive(true);
                            break;
                        case "치료법":  //case "Treatment"
                            GameObject.Find("Canvas").transform.Find("S3DataUI4").gameObject.SetActive(true);
                            break;
                        case "음성과 양성":  //case "PosNeg"
                            GameObject.Find("Canvas").transform.Find("S3DataUI5").gameObject.SetActive(true);
                            break;
                        case "Quiz1.1":
                            GameObject.Find("Canvas").transform.Find("S1QuizUI").gameObject.SetActive(true);
                            break;
                        case "Quiz1.2":
                            GameObject.Find("Canvas").transform.Find("S1QuizUI2").gameObject.SetActive(true);
                            break;
                        case "Quiz1.3":
                            GameObject.Find("Canvas").transform.Find("S1QuizUI3").gameObject.SetActive(true);
                            break;
                        case "Quiz3.1":
                            GameObject.Find("Canvas").transform.Find("S3QuizUI").gameObject.SetActive(true);
                            break;
                        case "Quiz3.2":
                            GameObject.Find("Canvas").transform.Find("S3QuizUI2").gameObject.SetActive(true);
                            break;
                        case "Quiz3.3":
                            GameObject.Find("Canvas").transform.Find("S3QuizUI3").gameObject.SetActive(true);
                            break;
                        case "Quiz3.4":
                            GameObject.Find("Canvas").transform.Find("S3QuizUI4").gameObject.SetActive(true);
                            break;
                        case "Quiz3.5":
                            GameObject.Find("Canvas").transform.Find("S3QuizUI5").gameObject.SetActive(true);
                            break;
                        case "매뉴얼":
                            GameObject.Find("Canvas").transform.Find("ManualUI").gameObject.SetActive(true);
                            break;
                        case "경제적 영향":
                            GameObject.Find("Canvas").transform.Find("S2DataUI").gameObject.SetActive(true);
                            break;
                        case "독감과의 차이점":
                            GameObject.Find("Canvas").transform.Find("S2DataUI2").gameObject.SetActive(true);
                            break;
                        case "코로나 의미":
                            GameObject.Find("Canvas").transform.Find("S2DataUI3").gameObject.SetActive(true);
                            break;
                        case "바이러스 종류":
                            GameObject.Find("Canvas").transform.Find("S2DataUI4").gameObject.SetActive(true);
                            break;
                        case "Quiz2.1":
                            GameObject.Find("Canvas").transform.Find("S2QuizUI").gameObject.SetActive(true);
                            break;
                        case "Quiz2.2":
                            GameObject.Find("Canvas").transform.Find("S2QuizUI2").gameObject.SetActive(true);
                            break;
                        case "Quiz2.3":
                            GameObject.Find("Canvas").transform.Find("S2QuizUI3").gameObject.SetActive(true);
                            break;
                        case "Quiz2.4":
                            GameObject.Find("Canvas").transform.Find("S2QuizUI4").gameObject.SetActive(true);
                            break;
                        default:
                            break;
                    }
                    
                }
                else
                {
                    switch (item.itemName)
                    {
                        case "알약" : //case "Capsule"
                            bar.barValue += 10;
                            if(bar.barValue > bar.Alert) { bar.flag1 = false; }
                            else if (bar.barValue > bar.Alert2 && bar.barValue < bar.Alert) { bar.flag2 = false; }
                            break;
                        case "진통제" : //case "PainKiller"
                            bar.totaltime = -10;
                            break;
                        case "주사기" :  //case "Syringe"
                            break;
                        default:
                            break;
                    }
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
            ShowToolTip(item, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }
}
