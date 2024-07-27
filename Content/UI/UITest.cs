using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;

public class UITest : UIState
{
    private UIText textElement;
    public override void OnInitialize()
    {
        textElement = new UIText("Hello", 1.2f);
        textElement.Left.Set(50f, 0f);
        textElement.Top.Set(50f, 0f);
        Append(textElement);
    }
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);
    }
}