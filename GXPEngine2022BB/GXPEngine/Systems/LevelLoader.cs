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
        static string bmpPath = "level (2).bmp";

        static Dictionary<Color, int> colorRegistry = new Dictionary<Color, int>();

        static int pixelSize = 64;
        public static void LoadLevel(MyGame myGame)
        {
            colorRegistry.Add(Color.FromArgb(255, 234, 89, 190), 3);
            colorRegistry.Add(Color.FromArgb(255, 124, 46, 100), 2);
            colorRegistry.Add(Color.FromArgb(255, 255, 0, 0), 4);

            bmp = new Bitmap(bmpPath);

            Sprite sprite = new Sprite("Attic.png");
            myGame.AddChild(sprite);
            sprite.width = bmp.Width * pixelSize;
            sprite.height = bmp.Height * pixelSize;

            for (int x = 0; x < bmp.Width; x++)
            {
                for(int y = 0; y < bmp.Height; y++)
                {
                    Color color = bmp.GetPixel(x, y);
                    CreateObject(myGame, color, x, y);
                }
            }

            Player player = new Player(myGame.camera, "circle.png");
            myGame.manager.addEntity(player);
        }
        static void CreateObject(MyGame myGame,Color color,int x,int y)
        {
            x *= pixelSize;
            y *= pixelSize;
            int id = 0;
            if (colorRegistry.ContainsKey(color))
            {
                id = colorRegistry[color];
            }

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
                    Entity entity = new Entity("Stone_Platform.png");
                    myGame.manager.addEntity(entity);
                    entity.SetOrigin(entity.width / 2, entity.height / 2);
                    entity.width = pixelSize;
                    entity.height = pixelSize;
                    entity.SetXY(x, y);
                    Vec2[] points = entity.GetExtentsVec2();
                    int[] lines =
                    {
                        1,0,
                        2,1,
                        3,2,
                        0,3
                    };
                    entity.addComponent<PolygonCollider>(points,lines);
                    break;
                case 3:
                    Sprite sprite = new Sprite("Stone_Platform.png");
                    sprite.SetOrigin(sprite.width / 2, sprite.height / 2);
                    myGame.AddChild(sprite);
                    sprite.width = pixelSize;
                    sprite.height = pixelSize;
                    sprite.x = x;
                    sprite.y = y;
                    break;
                case 4:
                    Entity box = new Entity("colors.png");
                    Rigidbody rigid = box.addComponent<Rigidbody>();
                    myGame.manager.addEntity(box);
                    box.SetOrigin(box.width / 2, box.height / 2);
                    box.width = pixelSize;
                    box.height = pixelSize;
                    rigid.position = new Vec2(x, y);
                    break;
                default:
                    break;
            }
        }
    }
}
