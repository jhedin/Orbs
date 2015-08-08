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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace ZuneGame1
{
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        static List<Color> colourList;
        static Random randGenerator;
        public static Vector2 O;//0 vector, for some space saving fun.
        public static Texture2D circle;
        List<Circle> updateList;
        List<Circle> drawList;
        static TreeNode top;
        static TreeNode bottom;

        AccelerometerState accelState;
        TouchCollection touchCollection;

        int counter;
        public static int nodeCount;//if on random mode, and it starts getting too full, reset
        int counterCmp;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Zune.
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 60.0);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            populateColourList();
            randGenerator = new Random();
            O = new Vector2();
            updateList = new List<Circle>();
            drawList = new List<Circle>();

            counter = 0;
            nodeCount = 0;
            counterCmp = 1075;

            top = new TreeNode(new Vector2(16, 0), updateList, drawList);
            bottom = new TreeNode(new Vector2(16, 240), updateList, drawList);
            
            base.Initialize();
        }
        private void populateColourList()
        {
            colourList = new List<Color>();
            colourList.Add(Color.Aqua);
            /*colourList.Add(Color.Black);
            colourList.Add(Color.Black);
            colourList.Add(Color.Black);*/
            colourList.Add(Color.Blue);
            colourList.Add(Color.Chartreuse);
            colourList.Add(Color.CadetBlue);
            colourList.Add(Color.Chocolate);
            colourList.Add(Color.Crimson);
            colourList.Add(Color.Cyan);
            colourList.Add(Color.DarkBlue);
            colourList.Add(Color.DarkCyan);
            colourList.Add(Color.DarkGoldenrod);
            colourList.Add(Color.DarkGreen);
            colourList.Add(Color.DarkMagenta);
            colourList.Add(Color.DarkOliveGreen);
            colourList.Add(Color.DarkOrange);
            colourList.Add(Color.DarkOrchid);
            colourList.Add(Color.DarkRed);
            colourList.Add(Color.DarkSalmon);
            colourList.Add(Color.DarkSeaGreen);
            colourList.Add(Color.DarkSlateBlue);
            colourList.Add(Color.DarkSlateGray);
            colourList.Add(Color.DarkTurquoise);
            colourList.Add(Color.DarkViolet);
            colourList.Add(Color.DeepPink);
            colourList.Add(Color.DodgerBlue);
            colourList.Add(Color.Firebrick);
            colourList.Add(Color.FloralWhite);
            colourList.Add(Color.ForestGreen);
            colourList.Add(Color.Fuchsia);
            colourList.Add(Color.Gainsboro);
            colourList.Add(Color.GhostWhite);
            colourList.Add(Color.Gold);
            colourList.Add(Color.Goldenrod);
            colourList.Add(Color.Green);
            colourList.Add(Color.GreenYellow);
            colourList.Add(Color.HotPink);
            colourList.Add(Color.IndianRed);
            colourList.Add(Color.Indigo);
            colourList.Add(Color.LawnGreen);
            colourList.Add(Color.LightBlue);
            colourList.Add(Color.LightCoral);
            colourList.Add(Color.LightGreen);
            colourList.Add(Color.LightPink);
            colourList.Add(Color.LightSalmon);
            colourList.Add(Color.LightSeaGreen);
            colourList.Add(Color.LightSkyBlue);
            colourList.Add(Color.Lime);
            colourList.Add(Color.LimeGreen);
            colourList.Add(Color.Magenta);
            colourList.Add(Color.Maroon);
            colourList.Add(Color.MediumOrchid);
            colourList.Add(Color.MediumVioletRed);
            colourList.Add(Color.MidnightBlue);
            colourList.Add(Color.Navy);
            colourList.Add(Color.Olive);
            colourList.Add(Color.OliveDrab);
            colourList.Add(Color.Orange);
            colourList.Add(Color.OrangeRed);
            colourList.Add(Color.Orchid);
            colourList.Add(Color.PaleGreen);
            colourList.Add(Color.PaleTurquoise);
            colourList.Add(Color.PaleVioletRed);
            colourList.Add(Color.Plum);
            colourList.Add(Color.PowderBlue);
            colourList.Add(Color.Purple);
            colourList.Add(Color.Red);
            colourList.Add(Color.RoyalBlue);
            colourList.Add(Color.Salmon);
            colourList.Add(Color.SeaGreen);
            colourList.Add(Color.SkyBlue);
            colourList.Add(Color.SlateBlue);
            colourList.Add(Color.SlateGray);
            colourList.Add(Color.SpringGreen);
            colourList.Add(Color.SteelBlue);
            colourList.Add(Color.Teal);
            colourList.Add(Color.Tomato);
            colourList.Add(Color.Turquoise);
            colourList.Add(Color.Violet);
            colourList.Add(Color.Yellow);
            colourList.Add(Color.YellowGreen);
        }
        public static Color getRandomColour()
        {
            return colourList[randGenerator.Next(colourList.Count)];
        }
        TreeNode search(int X, int Y)
        {
            if (Y < 240)
                return top.search(new Vector2(X, Y));
            return bottom.search(new Vector2(X, Y));
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentManager content = new ContentManager(Services, "Content");
            circle = content.Load<Texture2D>("circle");
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            Stack<int> done = new Stack<int>();
            Circle circ;
            for (int i = 0; i < updateList.Count; i++)
            {
                circ = updateList[i];
                if (circ.updateLoc())
                {
                    done.Push(i);
                    circ.randColour();
                }
            }
            while (done.Count != 0)
            {
                updateList.RemoveAt(done.Pop());
            }
            //random mode
            if (counter >= counterCmp)
            {
                int X = randGenerator.Next(242);
                int Y = randGenerator.Next(480);
                TreeNode tNode = search(X, Y);
                tNode.split();
                if (nodeCount > 1075)
                {
                    counterCmp = 1075 - 2 * nodeCount/counterCmp;
                }
                if (nodeCount > 4500)
                {
                    Initialize();  
                }
                counter = 1000;
            }
            counter++;

            //user input
            touchCollection = TouchPanel.GetState();
            foreach (TouchLocation touch in touchCollection)
            {
                TreeNode tNode = search((int)touch.Position.X, (int)touch.Position.Y);
                if (tNode.inCircle(touch.Position))
                    tNode.split();
                counter = 0;
                counterCmp = 1075;
            }

            accelState = Accelerometer.GetState();

            if (accelState.Acceleration.LengthSquared() > 1.5)
            {
                Initialize();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            /*test.Draw(gameTime, spriteBatch);
            test2.Draw(gameTime, spriteBatch);*/

            foreach (Circle circ in drawList)
            {
                circ.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
