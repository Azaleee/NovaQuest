using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace NovaQuest.UI
{
    public class QUIPanel : UIState
    {
        public static UIPanel mainPanel;
        public static UIText headerText;
        private readonly UIPanel menuPanel;
        private readonly UIPanel contentPanel;
        //public static UIText textL;
        private readonly UIItemSlot iconQuest;
        public static ToolTipUI toolTipUI;
        public static Dictionary<int, Vector2> _questPositions = new Dictionary<int, Vector2>();
        public static readonly List<QuestDisplay> questDisplays = new List<QuestDisplay>();
        private static bool needsRedraw = false;

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
            toolTipUI = new ToolTipUI
            {
                Width = StyleDimension.FromPixels(200),
                Height = StyleDimension.FromPixels(100),
                Top = StyleDimension.FromPixels(20),
                Left = StyleDimension.FromPixels(20),
            };
            ReloadUI();
        }
        private static void Test()
        {
            ReloadUI();
        }
        public static void ReloadUI()
        {
            mainPanel.RemoveAllChildren();
            toolTipUI.RemoveAllChildren();

            headerText = new UIText("Quests", 1.5f)
            {
                Top = StyleDimension.FromPixels(10),
                HAlign = 0.5f,
                TextColor = Color.White
            };
            var reloadButton = new UIButton("Reload")
            {
                Width = StyleDimension.FromPixels(100),
                Height = StyleDimension.FromPixels(30),
                Top = StyleDimension.FromPixels(10),
                Left = StyleDimension.FromPixels(700) // Adjust position as needed
            };
            reloadButton.OnClick += () => Test();
            mainPanel.Append(headerText);
            mainPanel.Append(reloadButton);
            mainPanel.Append(toolTipUI);
            Parsing.LoadQuests(ref NovaQuest.quests);
            Main.NewText("Salure je relqod");
            //questList.Clear;
            if (NovaQuest.quests != null)
            {
                _questPositions.Clear();
                questDisplays.Clear();
                foreach (var quest in NovaQuest.quests)
                {
                    if (quest.ItemIcon != null)
                    {
                        var icon = new UIItemSlot(new Item(), quest.Name, quest.Desc)
                        {
                            Top = StyleDimension.FromPixels(quest.PosTop),
                            Left = StyleDimension.FromPixels(quest.PosLeft),
                        };
                        icon.SetItem(quest.ItemIcon.GetIconType());
                        mainPanel.Append(icon);
                        var position = new Vector2(quest.PosLeft, quest.PosTop);
                        //Main.NewText($"UId : {quest.Uid} : X = {position.X} Y = {position.Y}");
                        _questPositions[quest.Uid] = position;
                    }
                }
                foreach (var position in _questPositions)
                {
                    Main.NewText($"UId : {position.Key} : X = {position.Value.X} Y = {position.Value.Y}");
                }
            }

            //Main.NewText($"{questList.Count}");
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            toolTipUI.RemoveAllChildren(); // Clear previous content

            bool tooltipVisible = false;

            foreach (var questDisplay in questDisplays)
            {
                var icon = questDisplay.Icon;
                if (icon.IsMouseHovering)
                {
                    toolTipUI.SetText($"{questDisplay.Quest.Name}\n{questDisplay.Quest.Desc}");
                    tooltipVisible = true;
                    break;
                }
            }

            if (tooltipVisible)
            {
                if (mainPanel.HasChild(toolTipUI))
                {
                    mainPanel.RemoveChild(toolTipUI); // Remove and re-add to ensure it is on top
                }
                mainPanel.Append(toolTipUI);
            }
            else
            {
                if (mainPanel.HasChild(toolTipUI))
                {
                    mainPanel.RemoveChild(toolTipUI); // Remove tooltip if not needed
                }
            }
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            // Draw lines to connect quest dependencies
            if (needsRedraw)
            {
                DrawQuestLines(spriteBatch);
                needsRedraw = false; // Reset the flag after redrawing
            }
        }

        private static void DrawQuestLines(SpriteBatch spriteBatch)
        {
            // Set line color and thickness
            var lineColor = Color.Yellow;
            var lineThickness = 4f;

            foreach (var quest in NovaQuest.quests)
            {
                // Check if the quest has dependencies
                if (quest.Depend != null)
                {
                    foreach (var dependency in quest.Depend)
                    {
                        Main.NewText("salut yoyo");
                        if (_questPositions.TryGetValue(quest.Uid, out var startPos) &&
                            _questPositions.TryGetValue(dependency, out var endPos))
                        {
                            Main.NewText($"{startPos}");
                            // Draw a line from the quest icon to its dependency
                            DrawLine(spriteBatch, startPos, endPos, lineColor, lineThickness);
                        }
                    }
                }
                else
                {
                    Main.NewText("salut ca marche pas");
                }
            }
        }
        private static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness)
        {
            var lineTexture = TextureAssets.MagicPixel.Value;
            var direction = end - start;
            var length = direction.Length();
            var rotation = (float)Math.Atan2(direction.Y, direction.X);

            spriteBatch.Draw(lineTexture, start, null, color, rotation, Vector2.Zero, new Vector2(length, thickness), SpriteEffects.None, 0f);
        }
    }
    public class UIButton : UIPanel
    {
        private UIText _buttonText;

        public event Action OnClick;

        public UIButton(string text)
        {
            _buttonText = new UIText(text, 1f);
            _buttonText.HAlign = 0.5f;
            _buttonText.VAlign = 0.5f;
            Append(_buttonText);
        }

        public void MouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            OnClick?.Invoke();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (IsMouseHovering)
            {
                BackgroundColor = new Color(73, 94, 171);
            }
            else
            {
                BackgroundColor = new Color(63, 82, 151);
            }
        }
    }
}