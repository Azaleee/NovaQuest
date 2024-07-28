using System.Collections.Generic;
using Terraria.ID;

namespace NovaQuest
{
    public class QuestDef(int uid, string name, string desc)
    {
        public int Uid = uid;
        public string Name = name;
        public string Desc = desc;
        public Dictionary<int, int> ItemsToGet = [];
        public void AddItemToGet(int id, int quantity) => ItemsToGet[id] = quantity;
    }
}