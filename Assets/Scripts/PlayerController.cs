using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : PawnController
{
    [System.Serializable]
    public enum InputType { UI, Game }
    public InputType inputType = InputType.Game;
}
