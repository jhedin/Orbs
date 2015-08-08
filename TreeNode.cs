using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZuneGame1
{
    class TreeNode
    {
        Circle item;
        TreeNode[] children;
        TreeNode parent;
        List<Circle> updateList;
        List<Circle> drawList;
        bool ready;

        public TreeNode(Vector2 locXY, List<Circle> update, List<Circle>draw)
        {
            updateList = update;
            drawList = draw;
            Main.nodeCount++;
            
            item = new Circle( this, Main.getRandomColour(), locXY, 240, 0);
            updateList.Add(item);
            drawList.Add(item);
            ready = true;
        }
        public TreeNode(TreeNode par, List<Circle> update, List<Circle> draw, int no)
        {
            parent = par;
            updateList = update;
            drawList = draw;
            Main.nodeCount++;

            item = new Circle(this, parent.item.getColour(), parent.item.getLocXY(), parent.item.getDia(), no);
            updateList.Add(item);
            drawList.Add(item);
            ready = false;
        }

        public bool inCircle(Vector2 locXY)
        {
            float rad = item.getDia()/2;
            locXY = item.getLocXY() - locXY + new Vector2(rad,rad);
            if (locXY.Length() <= rad)
                return true;
            return false;
        }

        public TreeNode search(Vector2 cmpLoc)
        {
            Vector2 locXY = item.getLocXY();
            locXY.X += item.getDia() / 2;
            locXY.Y += item.getDia() / 2;

            //check done
            if (cmpLoc.Equals(locXY) || children == null)
                return this;

            //figure out which child to use
            else if (cmpLoc.X > locXY.X)
            {
                if (cmpLoc.Y > locXY.Y)
                {
                    //search through the child, and its children
                    return children[4].search(cmpLoc);
                }
                else
                    return children[2].search(cmpLoc);
            }
            else
            {
                if (cmpLoc.Y > locXY.Y)
                {
                    return children[3].search(cmpLoc);
                }
                else
                    return children[1].search(cmpLoc);
            }
        }

        public void split()
        {
            if (children == null && item.getDia() > 2 && ready && item.getColour() != Color.Black)
            {
                children = new TreeNode[5];
                //children[0] = new TreeNode(new Vector2(item.getLocXY().X + item.getDia() / 2, item.getLocXY().X + item.getDia() / 2), updateList, drawList);
                //children[0].item.setDia(item.getDia() / 2);
                for (int i = 1; i < 5; i++)
                {           
                    children[i] = new TreeNode(this, updateList, drawList, i);
                }
                drawList.Remove(item);
                item.setColour(Color.Black);

                Main.nodeCount--;
            }
        }

        public void readyToSplit()
        {
            ready = true;
        }

        public void erase()
        {
            if (children != null && children[0] != null)
            {
                children[0].item.setColour(Color.White);
                drawList.Remove(children[0].item);
                children[0] = null;
            }
        }
    }
}