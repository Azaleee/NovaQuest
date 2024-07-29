using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace NovaQuest
{
    [Autoload(Side = ModSide.Client)]
    public class UISystem : ModSystem
    {
        internal static QUIPanel mainPanel;
        private UserInterface _userInterface;
        public static bool _isVisible = false;
        public override void Load()
        {
            mainPanel = new QUIPanel();
            _userInterface = new UserInterface();
            _userInterface.SetState(mainPanel);
            Parsing.LoadQuests(ref NovaQuest.quests);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (_isVisible)
                _userInterface?.Update(gameTime);
            if (NovaQuest.ToogleUI.JustPressed)
            {
                _isVisible = !_isVisible;
                if (_isVisible)
                    _userInterface.SetState(mainPanel);
                else
                    _userInterface.SetState(null);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "YourMod: A Description",
                    delegate
                    {
                        if (_isVisible)
                            _userInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        public override void Unload()
        {
            mainPanel = null;
            _userInterface = null;
        }

    }
}