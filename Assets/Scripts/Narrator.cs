using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Narrator
{
    [TextArea(3, 10)]
    public string[] sentences;
    public string name;
}
