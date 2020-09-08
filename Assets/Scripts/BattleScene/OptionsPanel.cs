using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OptionsPanel", menuName = "OptionsPanel")]
public class OptionPanel : ScriptableObject

{
    public int turn;
    public Sprite artwork;

    public GameObject optionOne;
    public GameObject optionTwo;
    public GameObject optionThree;
}
