using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace GamesProgramming_V1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture, player, platform, flag, coin;
        Vector2 texturePos, velocity;
        Vector2 texturePos1;
        Vector2 texturePos2;
        Vector2 texturePos3;
        Vector2 texturePos4;
        Vector2 texturePos5;

        GameMenu main = new GameMenu();

        //Gravity strength and movement speed
        const float gravity = 100f;
        float moveSpeed = 100f;
        float moveSpeedback = -100f;
        bool hasJumped;

        //collision detection variables
        Point playerFrameSize = new Point(40, 41);
        Point platformFrameSize = new Point(300, 5);
        Point flagFrameSize = new Point(30, 50);
        Point coinFrameSize = new Point(40, 40);
        int playerCollisionRectOffset = 0;
        int platformCollisionRectOffset = 0;
        int flagCollisionRectOffset = 0;
        int coinCollisionRectOffset = 0;

        //Player to object collide function
        protected bool Collide()
        {
            Rectangle playerRect = new Rectangle(
                (int) texturePos.X + playerCollisionRectOffset,
                (int) texturePos.Y + playerCollisionRectOffset,
                playerFrameSize.X - (playerCollisionRectOffset *2),
                playerFrameSize.Y - (playerCollisionRectOffset *2));
            Rectangle platformRect = new Rectangle(
                (int) texturePos2.X + platformCollisionRectOffset,
                (int) texturePos2.Y + platformCollisionRectOffset,
                platformFrameSize.X - (platformCollisionRectOffset *2),
                platformFrameSize.Y - (platformCollisionRectOffset *2));

            return playerRect.Intersects(platformRect);
        }

        protected bool Collide1()
        {
            Rectangle playerRect = new Rectangle(
                (int)texturePos.X + playerCollisionRectOffset,
                (int)texturePos.Y + playerCollisionRectOffset,
                playerFrameSize.X - (playerCollisionRectOffset * 2),
                playerFrameSize.Y - (playerCollisionRectOffset * 2));
            Rectangle platformRect1 = new Rectangle(
               (int)texturePos3.X + platformCollisionRectOffset,
               (int)texturePos3.Y + platformCollisionRectOffset,
               platformFrameSize.X - (platformCollisionRectOffset * 2),
               platformFrameSize.Y - (platformCollisionRectOffset * 2));

            return playerRect.Intersects(platformRect1);
        }

        protected bool CollideFlag()
        {
            Rectangle playerRect = new Rectangle(
                (int)texturePos.X + playerCollisionRectOffset,
                (int)texturePos.Y + playerCollisionRectOffset,
                playerFrameSize.X - (playerCollisionRectOffset * 2),
                playerFrameSize.Y - (playerCollisionRectOffset * 2));
            Rectangle flagRect = new Rectangle(
               (int)texturePos4.X + flagCollisionRectOffset,
               (int)texturePos4.Y + flagCollisionRectOffset,
               flagFrameSize.X - (flagCollisionRectOffset * 2),
               flagFrameSize.Y - (flagCollisionRectOffset * 2));

            return playerRect.Intersects(flagRect);
        }

        protected bool CollideCoin()
        {
            Rectangle playerRect = new Rectangle(
                (int)texturePos.X + playerCollisionRectOffset,
                (int)texturePos.Y + playerCollisionRectOffset,
                playerFrameSize.X - (playerCollisionRectOffset * 2),
                playerFrameSize.Y - (playerCollisionRectOffset * 2));
            Rectangle coinRect = new Rectangle(
               (int)texturePos5.X + coinCollisionRectOffset,
               (int)texturePos5.Y + coinCollisionRectOffset,
               coinFrameSize.X - (coinCollisionRectOffset * 2),
               coinFrameSize.Y - (coinCollisionRectOffset * 2));

            return playerRect.Intersects(coinRect);
        }

        public Game1()
        {
            //Window Size
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";

            //Texture starting point of background, platforms, flag and coin
            texturePos1 = new Vector2(0, 0);
            texturePos2 = new Vector2(0, 300);
            texturePos3 = new Vector2(300, 400);
            texturePos4 = new Vector2(570, 352);
            texturePos5 = new Vector2(320, 350);

            //Window Title
            this.Activated += (sender, args) => { this.Window.Title = "Super Biker"; };
            this.Deactivated += (sender, args) => { this.Window.Title = "Super Biker (Paused)"; };

            //Pause button
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.P))
                this.Activated += (sender, args) => { this.Window.Title = "Super Biker (Paused)"; };

        }

        protected override void Initialize()
        {
            //Player Spawn
            texturePos = velocity = new Vector2(0, 250);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            main.LoadContent(Content);

            //Asset content
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = this.Content.Load<Texture2D>("Background");
            player = this.Content.Load<Texture2D>("Bike1");
            platform = this.Content.Load<Texture2D>("Platform");
            flag = this.Content.Load<Texture2D>("flag");
            coin = this.Content.Load<Texture2D>("coin");
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {

            main.Update();

            if (IsActive)
            {
                KeyboardState state = Keyboard.GetState();
                //Exit Game through Esc key
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                //Tells what keys are being pressed
                System.Text.StringBuilder sb = new StringBuilder();
                foreach (var key in state.GetPressedKeys())
                    sb.Append("Keys: ").Append(key).Append(" Pressed");

                if(sb.Length > 0)
                System.Diagnostics.Debug.WriteLine(sb.ToString());
                else
                System.Diagnostics.Debug.WriteLine("No Keys Pressed");

               
                //Movement of player character
                if (state.IsKeyDown(Keys.Up))
                    velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else if (state.IsKeyDown(Keys.Down))
                    velocity.X = moveSpeedback * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    velocity.X = 0;
                    velocity.Y = 1f;
                    texturePos += velocity;

                //Jump statements
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
                {
                    texturePos.Y -= 10f;
                    velocity.Y = -10f;
                    hasJumped = true;
                }

                if (hasJumped == true)
                {
                    float i = 1;
                    velocity.Y += 0.15f * i;
                }

                if (hasJumped == false)
                    velocity.Y = 0f;
                    hasJumped = false;
                
                //collision detection reaction
                if (Collide())
                {
                    velocity.Y = -1f;
                    texturePos += velocity;
                }
                if (Collide1())
                {
                    velocity.Y = -1f;
                    texturePos += velocity;
                }
                if (CollideFlag())
                {
                    Exit();
                }
                if (CollideCoin())
                {
                    texturePos5 = new Vector2(800, 800);
                }


                //collision with window size/cant go off screen
                if (texturePos.X < 0)
                {
                    texturePos.X = 0;
                }
                if (texturePos.Y < 0)
                {
                    texturePos.Y = 0;
                }

                if (texturePos.X > Window.ClientBounds.Width - playerFrameSize.X)
                {
                    texturePos.X = Window.ClientBounds.Width - playerFrameSize.X;
                }
                
                if (texturePos.Y > Window.ClientBounds.Height - playerFrameSize.Y)
                {
                    Exit();
                }

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            //Background Color
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Sprites
            spriteBatch.Begin();
            main.Draw(spriteBatch);
            //spriteBatch.Draw(texture, texturePos1);
            spriteBatch.Draw(player, texturePos);
            spriteBatch.Draw(platform, texturePos2);
            spriteBatch.Draw(platform, texturePos3);
            spriteBatch.Draw(flag, texturePos4);
            spriteBatch.Draw(coin, texturePos5);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
