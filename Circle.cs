using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ZuneGame1
{
    class Circle
    {    
        Color circleColour;
        Vector2 destLocXY;
        Vector2 locXY;
        Vector2 velocity;
        Rectangle drawRect;
        TreeNode node;

        int diameter;

        public Circle(TreeNode tNode, Color upperColor, Vector2 upperLoc, int upperDia, int no)
        {
            circleColour = upperColor;
            diameter = upperDia / 2;
            node = tNode;
    
            if (no == 0)
            {
                locXY = upperLoc;
                destLocXY = locXY;
                diameter *= 2;
            }
            else
            {
                /*
                 1 2
                 3 4
                */
                locXY = new Vector2(upperLoc.X + diameter / 2, upperLoc.Y + diameter / 2);
                destLocXY = new Vector2();
                switch (no)
                {
                    case 1:
                        destLocXY.X = upperLoc.X;
                        destLocXY.Y = upperLoc.Y;
                        break;
                    case 2:
                        destLocXY.X = upperLoc.X + diameter;
                        destLocXY.Y = upperLoc.Y;
                        break;
                    case 3:
                        destLocXY.X = upperLoc.X;
                        destLocXY.Y = upperLoc.Y + diameter;
                        break;
                    case 4:
                        destLocXY.X = upperLoc.X + diameter;
                        destLocXY.Y = upperLoc.Y + diameter;
                        break;
                }
                locXY = (destLocXY - locXY) / 2 + locXY;
            }
            velocity = new Vector2();
            velocity.X = (destLocXY.X - locXY.X) / 16;
            velocity.Y = (destLocXY.Y - locXY.Y) / 16;

            drawRect = new Rectangle((int)locXY.X, (int)locXY.Y, diameter, diameter);

        }

        public Color getColour()
        {
            return circleColour;
        }
        public void setColour(Color newCol)
        {
            circleColour = newCol;
        }
        public int getDia()
        {
            return diameter;
        }
        public void setDia(int dia)
        {
            diameter = dia;
        }
        public Vector2 getLocXY()
        {
            return locXY;
        }

        public void randColour()
        {
            circleColour = Main.getRandomColour();
        }

        public bool updateLoc()
        {
            if (destLocXY.Equals(locXY))
            {
                velocity = Main.O;
                destLocXY = Main.O;
                drawRect.X = (int)locXY.X;
                drawRect.Y = (int)locXY.Y;
                node.erase();
                node.readyToSplit();
                return true;
            }
            
            locXY += velocity;
            drawRect.X = (int)locXY.X;
            drawRect.Y = (int)locXY.Y;

            return false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.circle, drawRect, circleColour);
        }
    }
}