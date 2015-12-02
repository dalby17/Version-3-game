using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesProgramming_V1
{
    class GUIElement
    {
        private Texture2D GUITexture;

        private Rectangle GUIRect;

        private string assetName;

        public string AssetName
        {
            get { return assetName; }
            set { assetName = value; }
        }

        public delegate void ElementClicked(string element);

        public event ElementClicked clickEvent;

        //All main menu assets
        public GUIElement(string assetName)
        {
            this.assetName = assetName;
        }

        //Load assets and rectangles
        public void LoadContent(ContentManager content)
        {
            GUITexture = content.Load<Texture2D>(assetName);
            GUIRect = new Rectangle(0, 0, GUITexture.Width, GUITexture.Height);
        }

        public void Update()
        {
            //Click on menu
            if (GUIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))&& Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                clickEvent(assetName);
            }
        }

        //Draw Assets/Rectangles
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GUITexture, GUIRect, Color.White);
        }

        //Move assets/rectangles together
        public void MoveElement(int x, int y)
        {
            GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, GUIRect.Width, GUIRect.Height);
        }
    }
}
