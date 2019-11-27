using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;

namespace Puzzle_Game_27483533
{
    class ImageClass 
    {
        public ArrayList arrPics = new ArrayList();

        public ImageClass ()
        {
            
        }
        
        public ArrayList ImageTrimArray (Bitmap trimImage, int xPos, int yPos)
        {
            ArrayList arrPictures = new ArrayList();
            
            int horizontal = 0;
            int vertical = 0;

            for(int k = 0; k < 8; k++)
            {
                Bitmap image = new Bitmap(xPos, yPos);

                for(int i = 0; i < xPos; i++)
                {
                    for(int j = 0; j < yPos; j++)
                    {
                        image.SetPixel(i, j, trimImage.GetPixel((i + horizontal), (j + vertical)));
                        
                    }
                }

                arrPictures.Add(image);

                horizontal = horizontal + 100;
                if (horizontal == 300)
                {
                    horizontal = 0;
                    vertical = vertical + 100;
                }
            }

            return arrPictures;
        }
    }
}
