using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static GXPEngine.ECS.EntityComponent;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace GXPEngine.Systems
{
    class LevelLoader
    {
        static Bitmap bmp;
        static string bmpPath = "D:/07 School/ProjectFinalApproach/GXPEngine2022BB/GXPEngine/bin/Debug/level.bmp";

        static Dictionary<Color, int> colorRegistry = new Dictionary<Color, int>();

        static int pixelSize = 32;
        public static void LoadLevel(MyGame myGame)
        {
            colorRegistry.Add(Color.FromArgb(255), 3);
            colorRegistry.Add(Color.FromArgb(255,0,0,0), 2);

            bmp = new Bitmap(bmpPath);

            for (int x = 0; x < bmp.Width; x++)
            {
                for(int y = 0; y < bmp.Height; y++)
                {
                    Color color = bmp.GetPixel(x, y);
                    CreateObject(myGame, color, x, y);
                }
            }
        }
        static void CreateObject(MyGame myGame,Color color,int x,int y)
        {
            //x *= pixelSize;
            //y *= pixelSize;
            int id = 0;
            if (colorRegistry.ContainsKey(color))
            {
                id = colorRegistry[color];
            }
            Console.WriteLine(id + " : " + color.ToString());
            Console.WriteLine(Color.FromArgb(0).ToString());



            switch (id)
            {
                case 0:
                    break;
                case 1:
                    Player player = new Player(myGame.camera, "circle.png");
                    player.x = x;
                    player.y = y;
                    break;
                case 2:
                    Entity entity = new Entity("square.png");
                    myGame.manager.addEntity(entity);
                    Vec2[] points = entity.GetExtentsVec2();
                    entity.width = 32;
                    entity.height = 32;
                    int[] lines =
                    {
                        0,1,
                        1,2,
                        2,3,
                        3,0
                    };
                    entity.addComponent<PolygonCollider>(points,lines);
                    break;
                default:
                    break;
            }
        }
    }
}
