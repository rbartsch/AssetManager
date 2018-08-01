using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssetManagerDemo {
    public static class TextureUtils {
        public static Texture2D CreateFlatTexture(GraphicsDevice graphicsDevice, int width, int height, Color color) {
            Texture2D t = new Texture2D(graphicsDevice, width, height);
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Count(); i++) {
                pixels[i] = color;
            }

            t.SetData(pixels);
            return t;
        }
    }
}