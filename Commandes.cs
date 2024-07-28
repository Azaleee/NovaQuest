using System;
using System.Drawing;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NovaQuest
{
    public class Commandes : ModCommand
    {
        public override string Command => "reloadquests";
        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            try
            {
                NovaQuest.quests = [];
                Parsing.LoadQuests(ref NovaQuest.quests);
                if (NovaQuest.quests != null)
                    Main.NewText("Reload done !");
                foreach (var quest in NovaQuest.quests)
                    Main.NewText($"Quest ID : {quest.Uid} ; Name : {quest.Name} ; Desc : {quest.Desc}");
            }
            catch (Exception ex)
            {
                Main.NewText($"{ex.Message}");
            }
        }
    }
}