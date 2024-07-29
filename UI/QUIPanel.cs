using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace NovaQuest
{
    public class QUIPanel : UIState
    {
        private UIPanel mainPanel;
        private UIText headerText;
        private UIPanel menuPanel;
        private UIPanel contentPanel;
        private UIText textL;

        public override void OnInitialize()
        {
            mainPanel = new UIPanel
            {
                Width = StyleDimension.FromPixels(950),
                Height = StyleDimension.FromPixels(600),
                HAlign = 0.5f,
                VAlign = 0.5f,
                BackgroundColor = new Color(63, 82, 151, 200)
            };
            Append(mainPanel);
            headerText = new UIText("Quests", 1.5f)
            {
                Top = StyleDimension.FromPixels(10),
                HAlign = 0.5f,
                TextColor = Color.White
            };
            mainPanel.Append(headerText);
            Test();
        }

        public override void OnActivate()
        {
            base.OnActivate();
            mainPanel.RemoveAllChildren();
            Test();
        }
        private void Test()
        {
            Parsing.LoadQuests(ref NovaQuest.quests);
            Main.NewText($"{NovaQuest.quests}");
            //questList.Clear;
            int i = 100;
            foreach (var quest in NovaQuest.quests)
            {
                var text = new UIText(quest.Name, 1.5f)
                {
                    Top = StyleDimension.FromPixels(i),
                    HAlign = 0.5f,
                    TextColor = Color.White
                };
                i += 40;
                mainPanel.Append(text);
            }
            textL = new UIText($"{i}", 1.5f)
            {
                Top = StyleDimension.FromPixels(50),
                HAlign = 0.5f,
                TextColor = Color.White
            };
            //Main.NewText($"{questList.Count}");
            mainPanel.Append(textL);
        }
    }
}