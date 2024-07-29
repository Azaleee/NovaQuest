using System.Collections.Generic;
using System.Threading;
using NovaQuest.UI;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace NovaQuest
{
    public class QuestDef(int uid, string name, string desc, int posTop, int posLeft)
    {
        public int Uid = uid;
        public ItemIcon ItemIcon { get; set; }
        public string Name = name;
        public string Desc = desc;
        public int PosTop = posTop;
        public int PosLeft = posLeft;
        public Dictionary<int, int> ItemsToGet = [];
        public List<int> Depend = [];
        public void AddItemToGet(int id, int quantity) => ItemsToGet[id] = quantity;
    }
    public class ItemIcon
    {
        public int ItemType;
        public Item GetIconType()
        {
            Item item = new();
            item.SetDefaults(ItemType);
            return item;
        }
    }
    public class QuestDisplay
    {
        public QuestDef Quest { get; }
        public UIItemSlot Icon { get; }

        public QuestDisplay(QuestDef quest)
        {
            Quest = quest;
            Icon = new UIItemSlot(new Item { type = quest.ItemIcon.ItemType });
            // You can position the icon as needed
        }
    }
}