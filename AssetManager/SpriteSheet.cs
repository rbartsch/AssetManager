using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssetManagerDemo {
    public class SpriteSheet {
        Texture2D texture;
        Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        GraphicsDevice graphicsDevice;


        public SpriteSheet(Texture2D texture, GraphicsDevice graphicsDevice) {
            this.texture = texture;
            this.graphicsDevice = graphicsDevice;
        }


        /// <summary>
        /// Requires Power of 2 texture
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="count">Indicates in order row left-to-right, top-to-bottom, how many to map as
        /// it is more efficient if you don't need the whole sheet</param>
        public void MapSpritesUniformly(string baseName, int w, int h, int offsetX, int offsetY, int count) {
            int totalCols = texture.Width / w;
            int totalRows = texture.Height / h;
            int xPos = offsetX;
            int yPos = offsetY;

            for(int y = 0; y < totalRows; y++) {
                for(int x = 0; x < totalCols; x++) {
                    if(x >= count) {
                        return;
                    }

                    MapSprite($"{baseName}_{x}_{y}", new Rectangle(xPos, yPos, w, h));
                    //sprites.Add($"{baseName}_{x}_{y}", new Sprite(texture, new Rectangle(xPos, yPos, w, h)));

                    xPos += w + offsetX;
                }

                xPos = offsetX;
                yPos += h + offsetY;
            }
        }


        public void MapSprite(string name, Rectangle source) {
#if DEBUG
            GeneralUtils.LogInfo($"Mapping sprite sheet: {texture.Name} => {name} {source}");
#endif
            sprites.Add(name, new Sprite(texture, source));
        }


        public Sprite GetSprite(string name) {
            if (!sprites.TryGetValue(name, out Sprite v)) {
                return new Sprite(TextureUtils.CreateFlatTexture(graphicsDevice, 16, 16, Color.Fuchsia));
            }
            else {
                return v;
            }
        }


        public Sprite GetSprite(Rectangle source) {
            foreach(Sprite s in sprites.Values) {
                if(source == s.Source) {
                    return s;
                }
            }

            return new Sprite(TextureUtils.CreateFlatTexture(graphicsDevice, 16, 16, Color.Fuchsia));
        }
    }
}