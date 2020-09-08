using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Option", menuName = "Option")]
public class Option : ScriptableObject
{
    public new string name;
    public string dialouge;
    public Sprite artwork;
    public int percentage;
}

