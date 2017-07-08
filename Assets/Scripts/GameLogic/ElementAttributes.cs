using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAttributes : MonoBehaviour {
    [SerializeField] private bool isUnique = false;
    public bool IsUnique {
        get {
            return isUnique;
        }
    }

    [SerializeField] private bool isPersistent = false;
    public bool IsPersistent {
        get {
            return isPersistent;
        }
    }

    [SerializeField] private int minLevel = 0;
    [SerializeField] private int maxLevel = 2;

    public bool IsValidForLevel(int currentLevel) {
        return (currentLevel >= minLevel) && ((maxLevel == -1) || (currentLevel <= maxLevel));
    }
}
