using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace NovaQuest
{
	public class NovaQuest : Mod
	{
		public static List<QuestDef> quests;
		public override void Load()
		{
			quests = [];
			Parsing.LoadQuests(ref quests);
			if (quests != null)
				Main.NewText("Load quests complete");
		}
	}
}
