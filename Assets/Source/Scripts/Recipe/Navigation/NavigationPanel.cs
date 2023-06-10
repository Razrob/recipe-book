using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationPanel : MonoBehaviour
{
    [SerializeField] private NavigationButton[] _navigationButtons;

    public IReadOnlyList<NavigationButton> NavigationButtons => _navigationButtons;


    [Serializable]
    public struct NavigationButton
    {
        public RecipeWindowID WindowID;
        public Button Button;
    }
}
