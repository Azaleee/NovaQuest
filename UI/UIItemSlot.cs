using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using System;

namespace NovaQuest.UI
{
    public class UIItemSlot : UIElement
    {
        private Item _item;
        private string _questName;
        private string _questDesc;
        //private Item item;

        public UIItemSlot(Item item, string name, string desc)
        {
            _item = item;
            _questName = name;
            _questDesc = desc;
            Width.Set(40f, 0f);
            Height.Set(40f, 0f);
        }

        public UIItemSlot(Item item)
        {
            this._item = item;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var dimensions = GetDimensions();
            var position = new Vector2(dimensions.X, dimensions.Y);

            // Dessine le fond de l'emplacement de l'objet
            spriteBatch.Draw(TextureAssets.InventoryBack.Value, position, Color.White);

            // Dessine l'objet
            if (_item != null && !_item.IsAir)
            {
                var itemTexture = TextureAssets.Item[_item.type].Value;
                spriteBatch.Draw(itemTexture, position + new Vector2(10f, 10f), Color.White);

                // Dessine la quantité de l'objet si supérieure à 1
                if (_item.stack > 1)
                {
                    Utils.DrawBorderString(spriteBatch, _item.stack.ToString(), position + new Vector2(10f, 26f), Color.White);
                }
            }

            // Affiche le tooltip si l'objet est survolé
            if (IsMouseHovering && _item != null && !_item.IsAir)
            {
                DrawTooltip(spriteBatch, position);
            }
        }
        private void DrawTooltip(SpriteBatch spriteBatch, Vector2 position)
        {
            if (_questDesc != null && _questName != null)
            {
                var tooltipPosition = new Vector2(position.X + Width.Pixels, position.Y); // Position du tooltip à droite de l'élément
                var tooltipSize = new Vector2(240f, 200f); // Taille approximative du tooltip
                var tooltipBackgroundColor = new Color(0, 0, 0, 180); // Couleur de fond du tooltip
                var tooltipTextColor = Color.White; // Couleur du texte

                // Dessine le fond du tooltip
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)tooltipPosition.X, (int)tooltipPosition.Y, (int)tooltipSize.X, (int)tooltipSize.Y), tooltipBackgroundColor);

                // Dessine le texte du tooltip
                var nameText = _questName;
                var descText = _questDesc;
                var namePosition = tooltipPosition + new Vector2(10f, 10f);
                var descPosition = namePosition + new Vector2(0f, 30f);

                Utils.DrawBorderString(spriteBatch, nameText, namePosition, tooltipTextColor);
                Utils.DrawBorderString(spriteBatch, descText, descPosition, tooltipTextColor);
            }
        }

        public void SetItem(Item item)
        {
            _item = item;
        }
    }
}