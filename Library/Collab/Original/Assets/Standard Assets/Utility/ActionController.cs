using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;

    private bool pickupActivated = false;
    private bool upActivated = false; // 다음 스테이지용
    private bool CabinetDoorActivated = false; // 숨기 엑티베이트
    private RaycastHit hitinfo;


    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theInventory;
    [SerializeField]
    private string sceneName;


    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckItem();
            CanPickUp();
            CanUp();
            CanHide();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitinfo.transform != null)
            {
                theInventory.AcquireItem(hitinfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitinfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CanUp()
    {
        if (upActivated)
        {
            if (hitinfo.transform != null)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
    private void CanHide()
    {
        if (CabinetDoorActivated)
        {
            if (hitinfo.transform != null)
            {
                hitinfo.transform.GetComponent<Door>().ChangeDoorState();
            }
        }
    }



    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, range, layerMask))
        {
            if (hitinfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
            else if (hitinfo.transform.tag == "Interaction")
            {
                EnterInfoAppear();
            }
            else if(hitinfo.transform.tag == "Hidecabinet"){
                CabinetInfoAppear();
            }
        }
        else
        {
            InfoDisappear();
            EnterInfoDisAppear();
            CabinetInfoDisAppear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        if(hitinfo.transform.GetComponent<ItemPickUp>().item.itemName == "Manual")
        {
            actionText.text = "매뉴얼" + "<color=yellow>" + " 마우스 왼쪽 클릭으로 획득 " + "</color>";
            //actionText.text = "Manual" + "<color=yellow>" + " (Inventory - I)" + "</color>";
        }
        else actionText.text = hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + "<color=yellow>" + " 마우스 왼쪽 클릭으로 획득" + "</color>";
        //else actionText.text = hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + "<color=yellow>" + " (Mouse L)" + "</color>";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }


    private void EnterInfoAppear()
    {
        upActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "다음 층으로 올라가기" + "<color=yellow>" + " 마우스 왼쪽 클릭으로 이동 " + "</color>";
        //actionText.text = "Go up stair" + "<color=yellow>" + " Mouse L to use" + "</color>";
    }


    private void CabinetInfoAppear()
    {
        CabinetDoorActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "<color=yellow>" + "마우스 왼쪽 클릭으로 열기/닫기" + "</color>";
    }


    private void CabinetInfoDisAppear()
    {
        CabinetDoorActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void EnterInfoDisAppear()
    {
        upActivated = false;
        actionText.gameObject.SetActive(false);
    }

}