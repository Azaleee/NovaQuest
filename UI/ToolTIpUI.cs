using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent;

namespace NovaQuest.UI
{
    public class ToolTipUI : UIElement
    {
        private readonly UIText _text;
        private Texture2D _backgroundTexture;

        public ToolTipUI()
        {
            _text = new UIText(string.Empty, 1.2f)
            {
                HAlign = 0.5f,
                VAlign = 0.5f,
                TextColor = Color.White
            };
            Append(_text);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var dimensions = GetDimensions();
            var position = new Vector2(dimensions.X, dimensions.Y);
            var size = new Vector2(dimensions.Width, dimensions.Height);

            // Draw the background
            _backgroundTexture ??= CreateBackgroundTexture(spriteBatch.GraphicsDevice, (int)size.X, (int)size.Y);
            spriteBatch.Draw(_backgroundTexture, position, Color.White);

            // Draw the text
            _text.Draw(spriteBatch);
        }

        private static Texture2D CreateBackgroundTexture(GraphicsDevice graphicsDevice, int width, int height)
        {
            var texture = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Black * 0.75f; // Semi-transparent black
            }
            texture.SetData(data);
            return texture;
        }

        public void SetText(string text)
        {
            _text.SetText(text);
            _text.Recalculate();
            Width.Set(_text.MinWidth.Pixels + 20, 0);
            Height.Set(_text.MinHeight.Pixels + 20, 0);
            Recalculate();
        }
    }
}
