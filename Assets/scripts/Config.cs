using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

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
        private OtherItems[] otherItems;
        [SerializeField]
        private int minAge;
        [SerializeField] 
        private Color defaultColor;

        

        public Config(Items[] items, OtherItems[] otherItems, string minAge, string defaultColor)
        {
            this.items = items;
            this.otherItems = otherItems;
            this.minAge = int.Parse(minAge);
            this.defaultColor = defaultColor.StringToColor();
        }

    }

    [Serializable]
    public class Items
    {
        [SerializeField] private string id;
        [SerializeField] private int cost;
        [SerializeField] private string tags;

        public Items(string id,string cost,string tags)
        {
            this.id = id;
            this.cost = int.Parse(cost);
            this.tags = tags;
        }

        public string Id => id; 
        public int Cost => cost;
        public string Tags => tags;
    }

    [Serializable]
    public class OtherItems
    {
        [SerializeField] private string name;
        [SerializeField] private bool isTrue;
        [SerializeField] private Color color;

        public OtherItems(string name,string isTrue,string color)
        {
            this.name = name;
            this.isTrue = bool.Parse(isTrue);
            this.color = color.StringToColor();
        }

        public string Name => name;
        public bool IsTrue => isTrue;
        public Color Color => color;
    }
}
