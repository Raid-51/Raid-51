using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    //Klassi fyrir hluti (items), geymir nafn, mynd og prefab fyrir hlutinn
    [System.Serializable]
    public class item
    {
        public string ObjectName;
        public Sprite ObjectIcon;
        public GameObject ObjectPrefab;
    }

    public item[] Items; //Hlutirnir
}
