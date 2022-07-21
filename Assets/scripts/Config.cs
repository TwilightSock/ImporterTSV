using System;
using UnityEngine;

namespace JuiceKit
{

    public static class StringExtension
    {
        public static Color StringToColor(this string colorString)
        {
            Color color = new Color();
            ColorUtility.TryParseHtmlString(colorString, out color);
            return color;
        }
    }


    [System.Serializable]
    public class Config : BaseConfig
    {
        [SerializeField] 
        private Items[] items;
        [SerializeField] 
        private Other_Items[] other_Items;
        [SerializeField]
        private int min_Age;
        [SerializeField] 
        private Color default_Color;

        public Items[] Items => items; 
        public Other_Items[] OtherItems => other_Items;
        public int MinAge => min_Age;
        public Color DefaultColor => default_Color;


    }

    [Serializable]
    public class Items
    {
        [SerializeField] private string id;
        [SerializeField] private int cost;
        [SerializeField] private string tags;

        public string Id => id; 
        public int Cost => cost;
        public string Tags => tags;
    }

    [Serializable]
    public class Other_Items
    {
        [SerializeField] private string name;
        [SerializeField] private bool isTrue;
        [SerializeField] private Color color;

        public string Name => name;
        public bool IsTrue => isTrue;
        public Color Color => color;
    }
}
