using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActionController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera hidingCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Text actionText;
    [SerializeField] private Inventory theInventory;
    [SerializeField] private string sceneName;
    //[SerializeField] private float range;


    private bool pickupActivated;
    private bool upActivated ;
    private bool CabinetDoorActivated ;

    private float range;
    private RaycastHit hitinfo;

    //private float rangeTemp;

    public bool ishiding;

    void Start()
    {
        range = 2f;
        pickupActivated = false;
        upActivated = false;
        CabinetDoorActivated = false;
        //rangeTemp = range;
    }

    void Update()
    {
        CheckItem();
        TryAction();
        if(ishiding == true)
        {
            actionText.gameObject.SetActive(true);
            actionText.text = "<color=yellow>" + "E" + "</color>" + " 나가기";
        }
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
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //range = rangeTemp;
            CabinetInfoDisAppear();
            ishiding = false;
            mainCamera.enabled = true;
            hidingCamera.enabled = false;
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
                //range = 0;
                mainCamera.enabled = false;
                hidingCamera.enabled = true;
                ishiding = true;
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
            else if (hitinfo.transform.tag == "Hidecabinet")
            {
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
        if (hitinfo.transform.GetComponent<ItemPickUp>().item.itemName == "Manual")
        {
            actionText.text = "매뉴얼 가지기:" + "<color=yellow>" + " 마우스 왼쪽 클릭 " + "</color>";
        }
        else actionText.text = hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + "<color=yellow>" + " 마우스 왼쪽 클릭: 획득하기" + "</color>";
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
        actionText.text = "올라가기:" + "<color=yellow>" + " 마우스 왼쪽 버튼 " + "</color>";
        //actionText.text = "Go up stair" + "<color=yellow>" + " Mouse L to use" + "</color>";
    }
    private void EnterInfoDisAppear()
    {
        upActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void CabinetInfoAppear()
    {
        CabinetDoorActivated = true;
        //hidingCamera = hitinfo.transform.GetComponent<Camera>();
        actionText.gameObject.SetActive(true);
        actionText.text = "<color=yellow>" + "마우스 왼쪽 버튼: 캐비넷 열기" + "</color>";
    }
    private void CabinetInfoDisAppear()
    {
        CabinetDoorActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
