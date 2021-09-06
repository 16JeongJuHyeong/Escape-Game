using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/access")]
public class Doorlock : ScriptableObject
{
    public string doorname;
    public GameObject lockPrefab;
}
