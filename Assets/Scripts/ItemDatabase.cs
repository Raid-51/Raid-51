using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    [System.Serializable]
    public class item
    {
        public string ObjectName;
        public Sprite ObjectIcon;
        public GameObject ObjectPrefab;
    }

    public item[] Items;
}
