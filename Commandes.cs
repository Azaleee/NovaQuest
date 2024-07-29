using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using NovaQuest.UI;
using System;

namespace NovaQuest
{
    public class Commandes : ModCommand
    {
        public override string Command => "r";
        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            try
            {
                NovaQuest.quests = [];
                QUIPanel.ReloadUI();
                if (NovaQuest.quests != null)
                    Main.NewText("Reload done !");
                /*
                foreach (var quest in NovaQuest.quests)
                    Main.NewText($"Quest ID : {quest.Uid} ; Name : {quest.Name} ; Desc : {quest.Desc}");
                */
            }
            catch (Exception ex)
            {
                Main.NewText($"{ex.Message}");
            }
        }
    }
}