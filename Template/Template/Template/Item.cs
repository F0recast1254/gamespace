using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class Item : IItem
    {
        private string _name;
        private float _weight;
        private IItem _decorator;
        public string Name { get { return _name; } }

        public string LongName { get { return Name + (_decorator == null ? "" : "with " + _decorator.LongName); } }
        public float Weight { get { return _weight + (_decorator == null ? 0 : _decorator.Weight); } }

        public string Description { get { return LongName + ',' + Weight; } }

        public bool isContainer { get { return false; } }
        public Item() : this("Nameless")
        {

        }
        public Item(string name) : this(name, 1f)
        {

        }

        public Item(string name, float weight)
        {
            _name = name;

            _weight = weight;
            _decorator = null;
        }

        public void AddDecorator(IItem decorator)
        {
            if (_decorator == null)
            {
                _decorator = decorator;
            }
            else
            {
                _decorator.AddDecorator(decorator);
            }
        }
    }


    public class ItemContainer : IItemContainer
    {
        private string _name;
        private float _weight;
        private IItem _decorator;
        public Dictionary<string, IItem> _items;

        public string Name { get { return _name; } }

        public string LongName { get { return Name + (_decorator == null ? "" : " with " + _decorator.LongName); } }
        public float Weight
        {
            get
            {
                Dictionary<string, IItem>.ValueCollection values = _items.Values;
                float totalContainedWeight = 0;
                foreach (IItem item in values)
                {
                    totalContainedWeight += item.Weight;
                }
                return _weight + totalContainedWeight + (_decorator == null ? 0 : _decorator.Weight);
            }
        }

        public string Description { get { return LongName + ',' + Weight; } }

        public bool isContainer { get { return true; } }


        public ItemContainer() : this("Nameless")
        {

        }
        public ItemContainer(string name) : this(name, 1f)
        {

        }

        public ItemContainer(string name, float weight)
        {
            _name = name;

            _weight = weight;
            _decorator = null;
            _items = new Dictionary<string, IItem>();
        }

        public void AddDecorator(IItem decorator)
        {
            if (_decorator == null)
            {
                _decorator = decorator;
            }
            else
            {
                _decorator.AddDecorator(decorator);
            }
        }

        public void Add(IItem item)
        {
            _items[item.Name] = item;
        }

        public IItem Remove(string itemName)
        {
            IItem itemToRemove = null;
            _items.Remove(itemName, out itemToRemove);
            return itemToRemove;


        }
    }
}

