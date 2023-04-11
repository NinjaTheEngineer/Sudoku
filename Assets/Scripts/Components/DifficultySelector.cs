using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelector : NinjaMonoBehaviour {
    public enum Difficulty {
        VeryEasy = 5,
        Easy = 30,
        Medium = 40,
        Hard = 45,
        Impossible = 60
    }
    public Difficulty difficulty;
}