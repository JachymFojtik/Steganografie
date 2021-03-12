using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegan
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg = args[2]; 
            Color pixel_color;
            switch (args[1])
            {
                case "--hide":
                    if (args[3].Contains(".png"))
                    {
                        Bitmap Image = new Bitmap(args[3]); 
                        Color last = Image.GetPixel(Image.Width - 1, Image.Height - 1);
                        Image.SetPixel(Image.Width - 1, Image.Height - 1, Color.FromArgb(last.R, last.G, msg.Length));//ukládám délku zprávy na konec

                        for (int i = 0; i < Image.Width; i++) 
                        {
                            for (int j = 0; j < Image.Height; j++) 
                            {
                                pixel_color = Image.GetPixel(i, j); 
                                if (i < 1 && j < msg.Length)
                                {
                                    Image.SetPixel(i, j, Color.FromArgb(pixel_color.R, pixel_color.G, Convert.ToInt32(msg[j]))); 
                                }

                            }
                        }

                        Image.Save(args[3]); 
                    }
                    break;
                case "--show":
                    if (args[3].Contains(".png"))
                    {
                        Bitmap Image = new Bitmap(args[2]);
                        Color Lastpix = Image.GetPixel(Image.Width - 1, Image.Height - 1);            
                        int len = Lastpix.B;;
                        Color pixColor;

                        string res = ""; 
                        for (int i = 0; i < Image.Width; i++)
                        {
                            for (int j = 0; j < Image.Height; j++)
                            {
                                pixColor = Image.GetPixel(i, j);
                                if (i < 1 && j < len)
                                {
                                    res += Encoding.ASCII.GetString(new byte[] { Convert.ToByte((char)(pixColor.B))});
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            Console.ReadKey();
        }
    }
}
