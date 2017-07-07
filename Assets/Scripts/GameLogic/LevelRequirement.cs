using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRequirement : MonoBehaviour {
    [SerializeField] private int minLevel;

    public bool IsValidForLevel(int currentLevel) {
        return currentLevel >= minLevel;
    }
}
