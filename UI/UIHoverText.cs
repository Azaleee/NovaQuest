using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace NovaQuest.UI
{
    public class UIHoverText(string text, float textScale, bool large, string hoverText) : UIText(text, textScale, large)
    {
        private readonly string _hoverText = hoverText;
        private bool _isHovering;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _isHovering = IsMouseHovering;
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (_isHovering && !string.IsNullOrEmpty(_hoverText))
            {
                /*
                var font = Terraria.GameContent.FontAssets.MouseText.Value;
                var textSize = font.MeasureString(_hoverText);
                var position = new Vector2(Main.mouseX + 10, Main.mouseY + 10);

                Utils.DrawBorderString(spriteBatch, _hoverText, position, Color.White);
                */
                Main.hoverItemName = _hoverText;
            }
        }
    }
}