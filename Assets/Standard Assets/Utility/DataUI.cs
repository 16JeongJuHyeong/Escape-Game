using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataUI : MonoBehaviour
{
    public void Open() { gameObject.SetActive(true); }
    public void Close() { gameObject.SetActive(false); }
}
