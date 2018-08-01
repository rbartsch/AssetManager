using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssetManagerDemo {
    public class Sprite {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Rectangle? Source { get; private set; } = null;
        public Color Color { get; set; } = Color.White;
        public float Rotation { get; set; } = 0f;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0.5f;
        public bool EnableDraw { get; set; } = true;


        public Sprite(Texture2D texture) {
            Texture = texture;
        }


        public Sprite(Texture2D texture, Rectangle source) {
            Texture = texture;
            Source = source;
        }


        public Sprite(Texture2D texture, Vector2 position) {
            Texture = texture;
            Position = position;
        }


        public Sprite(Texture2D texture, Vector2 position, float layerDepth) {
            Texture = texture;
            Position = position;
            LayerDepth = layerDepth;
        }


        public Sprite(Texture2D texture, Vector2 position, Rectangle source, Color color, float rotation, float layerDepth, bool enableDraw = true) {
            Texture = texture;
            Position = position;
            Source = source;
            Color = color;
            Rotation = rotation;
            LayerDepth = layerDepth;
            EnableDraw = enableDraw;
        }


        public Sprite(Texture2D texture, Vector2 position, Rectangle source, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects, float layerDepth, bool enableDraw = true) {
            Texture = texture;
            Position = position;
            Source = source;
            Color = color;
            Rotation = rotation;
            Origin = origin;
            Scale = scale;
            SpriteEffects = spriteEffects;
            LayerDepth = layerDepth;
            EnableDraw = enableDraw;
        }


        public void Draw(SpriteBatch spriteBatch) {
            if (Texture != null && EnableDraw) {
                spriteBatch.Draw(Texture, Position, Source, Color, Rotation, Origin, Scale, SpriteEffects, LayerDepth);
            }
        }
    }
}