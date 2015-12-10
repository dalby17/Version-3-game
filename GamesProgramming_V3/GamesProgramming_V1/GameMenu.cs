using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesProgramming_V1
{
    class GameMenu
    {
        List<GUIElement> main = new List<GUIElement>();

        public GameMenu()
        {
            main.Add(new GUIElement("MainMenu"));
            main.Add(new GUIElement("MenuBoxplay"));
            main.Add(new GUIElement("MenuBoxexit"));
        }


        //Loading content
        public void LoadContent(ContentManager content)
        {
            foreach (GUIElement element in main)
            {
                element.LoadContent(content);
                element.clickEvent += OnClick;
            }
            //Button position (Finding play button element and move it alone)
            main.Find(X => X.AssetName == "MenuBoxplay").MoveElement(400, 200);
            main.Find(X => X.AssetName == "MenuBoxexit").MoveElement(400, 400);
        }

        //Menu Update
        public void Update()
        {
            foreach (GUIElement element in main)
            {
                element.Update();
            }
        }

        //Drawing of menu items
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GUIElement element in main)
            {
                element.Draw(spriteBatch);
            }
        }

        //When Buttons are clicked
        public void OnClick(string element)
        {
            if (element == "MenuBoxplay")
            {
                
            }

            if (element == "MenuBoxexit")
            {
                
            }
        }
    }
}
