using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace WindowsGame1
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        //ALO MUNDO FALTA COLOCAR MENU
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState teclado;
        SoundEffect som;
        SoundEffect gameover; 

        public static int ScreenW;
        public static int ScreenH;

        public SpriteFont font;
     

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            ScreenW = GraphicsDevice.Viewport.Width;
            ScreenH = GraphicsDevice.Viewport.Height;
            teclado = Keyboard.GetState();

            Player1 = new Player();
            Player2 = new Player();
            ball = new Bola();
           
            base.Initialize();
           
        }

       
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
                    Player1.Texture = Content.Load<Texture2D>("Content/Paddle");
                    Player2.Texture = Content.Load<Texture2D>("Content/Paddle");


                    Player1.Position = new Vector2(PADDLE_OFFSET, ScreenH / 2 - Player1.Texture.Height / 2);
                    Player2.Position = new Vector2(ScreenW - Player2.Texture.Width - PADDLE_OFFSET, ScreenH / 2 - Player2.Texture.Height / 2);

                    ball.Texture = Content.Load<Texture2D>("Content/Ball");
                    ball.Lauch(BALL_START_SPEED);


                    font = Content.Load<SpriteFont>("Content/Score"); 
                   
        }

        const int PADDLE_OFFSET = 70;
        const float BALL_START_SPEED = 8f;

        Player Player1;
        Player Player2;
        Bola ball;
    
        protected override void Update(GameTime gameTime)
        {
            KeyboardState vai = Keyboard.GetState();
            gameover = Content.Load<SoundEffect>("Content/GameOver");
            som = Content.Load<SoundEffect>("Content/BallWallCollision");
           
            ScreenW = GraphicsDevice.Viewport.Width; 
            ScreenH = GraphicsDevice.Viewport.Height;
            ball.Move(ball.Velocity);
            Player1.Move(Player1.Velocity);
            Player2.Move(Player2.Velocity);


           if (ball.Position.X + ball.Texture.Width < 0)
            {
                ball.Lauch(BALL_START_SPEED);
                Player2.Score++;
                gameover.Play(); 
            }

            if (ball.Position.X > ScreenW)
            {
                ball.Lauch(BALL_START_SPEED);
                Player1.Score++;
                gameover.Play(); 
            }


            if (Player1.Position.Y + Player1.Texture.Height > ScreenH)
            {
                Player1.Position.Y = ScreenH - Player1.Texture.Height;
            }

            if (Player1.Position.Y < 0)
            {
                Player1.Position.Y = 0;
            }

            if (Player2.Position.Y + Player1.Texture.Height > ScreenH)
            {
                Player2.Position.Y = ScreenH - Player1.Texture.Height;
            }

            if (Player2.Position.Y < 0)
            {
                Player2.Position.Y = 0;
            }
            
           
            //COLISSÃO BRIOCADA
            if (ball.Position.X < Player1.Position.X + Player1.Texture.Width && ball.Position.X > Player1.Position.X && ball.Position.Y > Player1.Position.Y && ball.Position.Y < Player1.Position.Y + Player1.Texture.Height)
            {
               ball.Velocity *= -1;
               som.Play();
               
            }

            if (ball.Position.X < Player2.Position.X + Player2.Texture.Width && ball.Position.X > Player2.Position.X && ball.Position.Y > Player2.Position.Y && ball.Position.Y < Player2.Position.Y + Player2.Texture.Height)
            {
                ball.Velocity *= -1;
                som.Play();
            }

           
           //FALTA ORIENTAR 
            if (vai.IsKeyDown(Keys.W))
            {

                if (!teclado.IsKeyDown(Keys.W))
               { 
                    Player1.Position.Y += -3;

               } 

            }

            if (vai.IsKeyDown(Keys.S))
            {

                if (!teclado.IsKeyDown(Keys.S))
                {
                    Player1.Position.Y += 3;

                }

            }


            if (vai.IsKeyDown(Keys.Up))
            {

                if (!teclado.IsKeyDown(Keys.Up))
                {
                    Player2.Position.Y += -3;

                }

            }

            if (vai.IsKeyDown(Keys.Down))
            {

                if (!teclado.IsKeyDown(Keys.Down))
                {
                    Player2.Position.Y += 3;

                }

            }
          

            Draw(gameTime); 

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            Player1.Draw(spriteBatch);
            Player2.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Score  " + Player1.Score, new Vector2(58, 30), Color.White);
            spriteBatch.DrawString(font, "Score  " + Player2.Score, new Vector2(680, 30), Color.White);
            spriteBatch.End(); 
            base.Draw(gameTime);
        }
    }
}
