using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JuiceKit
{
    [Serializable]
    public class Config : BaseConfig
    {
        [SerializeField] private Items items;
        [SerializeField] private OtherItems otherItems;
        private int minAge;
        [SerializeField] 
        private string defaultColor;

        public Config(Items items, OtherItems otherItems, int minAge, string defaultColor)
        {
            this.items = items;
            this.otherItems = otherItems;
            this.minAge = minAge;
            this.defaultColor = defaultColor;
        }
    }

    public class Items
    {
        [SerializeField] private List<string> id;
        [SerializeField] private List<int> cost;
        [SerializeField] private List<string> tags;

        public Items(List<string> id,List<int> cost,List<string> tags)
        {
            this.id = id;
            this.cost = cost;
            this.tags = tags;
        }

        public List<string> Id => id; 
        public List<int> Cost => cost;
        public List<string> Tags => tags;
    }

    public class OtherItems
    {
        [SerializeField] private List<string> name;
        [SerializeField] private List<bool> isTrue;
        [SerializeField] private List<string> color;

        public OtherItems(List<string> name,List<bool> isTrue,List<string> color)
        {
            this.name = name;
            this.isTrue = isTrue;
            this.color = color;
        }

        public List<string> Name => name;
        public List<bool> IsTrue => isTrue;
        public List<string> Color => color;
    }
}
