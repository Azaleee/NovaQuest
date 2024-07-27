using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace NovaQuest
{
	public class NovaQuest : ModSystem
	{
		private UserInterface userInterface;
		private UITest test;
		public override void Load()
		{
			if (!Main.dedServ)
			{
				test = new UITest();
				test.Activate();
				userInterface = new UserInterface();
				userInterface.SetState(test);
			}
		}
		public override void UpdateUI(GameTime gameTime)
		{
			if (userInterface != null)
				userInterface.Update(gameTime);
		}
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex;

			mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != 1)
			{
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"Test lol",
					delegate
					{
						if (userInterface != null)
							userInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}
