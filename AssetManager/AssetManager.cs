using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace AssetManagerDemo {
    // TODO (rbartsch): Multithreaded loading, more exception handling
    public class AssetManager {
        ContentManager content;
        Dictionary<string, Asset<dynamic>> assets = new Dictionary<string, Asset<dynamic>>();


        public AssetManager(IServiceProvider services, string topDirectory = "Content") {
            content = new ContentManager(services, topDirectory);
        }


        /// <summary>
        /// Loads an individual asset 
        /// </summary>
        /// <param name="asset"></param>
        public void Load(AssetDefinition assetDef) {
            //Console.WriteLine($"Loading asset: {assetDef.Name} ({assetDef.Path})");
            GeneralUtils.LogInfo($"Loading asset: {assetDef.Name} ({assetDef.Path})");

            Asset<dynamic> asset = new Asset<dynamic>(assetDef);

            try {
                if (assetDef.Type == typeof(Texture2D)) {
                    asset.Assign(content.Load<Texture2D>(assetDef.Path));
                }
                else if (assetDef.Type == typeof(SpriteFont)) {
                    asset.Assign(content.Load<SpriteFont>(assetDef.Path));
                }
                else if (assetDef.Type == typeof(SoundEffect)) {
                    asset.Assign(content.Load<SoundEffect>(assetDef.Path));
                }
                else if (assetDef.Type == typeof(Song)) {
                    asset.Assign(content.Load<Song>(assetDef.Path));
                }
                else if (assetDef.Type == typeof(Model)) {
                    asset.Assign(content.Load<Model>(assetDef.Path));
                }
                else if (assetDef.Type == typeof(Effect)) {
                    asset.Assign(content.Load<Effect>(assetDef.Path));
                }
                else if (assetDef.Type == typeof(TextureCube)) {
                    asset.Assign(content.Load<TextureCube>(assetDef.Path));
                }
                else {
                    throw new NotSupportedException();
                }

                assets.Add(assetDef.Name, asset);
            }
            catch (Exception ex) {
                if (ex is ContentLoadException) {
                    GeneralUtils.LogError($" => Could not find/load {assetDef.Name} ({assetDef.Path}). {ex}");
                }
                else if (ex is NotSupportedException) {
                    GeneralUtils.LogError($" => The type {assetDef.Type.ToString().Split('.').Last()} for {assetDef.Name} ({assetDef.Path}) isn't supported.");
                }
                else if (ex is InvalidCastException) {
                    GeneralUtils.LogWarning($" => Skipping {assetDef.Name} ({assetDef.Path}). {ex.Message}");
                }
                else {
                    GeneralUtils.LogError($" => An unknown error has occured. {ex}");
                }
            }
        }


        /// <summary>
        /// Load all assets of type in the specified directory
        /// </summary>
        /// <param name="type"></param>
        /// <param name="directory"></param>
        /// <param name="searchOption"></param>
        public void LoadAll(Type type, string directory, SearchOption searchOption) {
            GeneralUtils.LogInfo($"Loading all assets from: {directory}");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int prevAssetTotal = TotalLoaded();

            if (!Directory.Exists(content.RootDirectory + "\\" + directory)) {
                GeneralUtils.LogError("Invalid directory");
                throw new DirectoryNotFoundException("Invalid directory");
            }

            string[] files = Directory.GetFiles(content.RootDirectory + "\\" + directory, "*", searchOption);

            foreach (string file in files) {
                string ext = Path.GetExtension(file);
                string path = file.Substring(file.IndexOf('\\') + 1);
                path = path.Substring(0, path.Length - ext.Length);

                Load(new AssetDefinition(path, type));
            }

            stopwatch.Stop();
            GeneralUtils.LogInfo($"=> Assets loaded: {TotalLoaded() - prevAssetTotal} (updated total: {TotalLoaded()})");
            GeneralUtils.LogInfo($" => Time elapsed: { stopwatch.Elapsed.TotalSeconds.ToString("0.0000")}s");
        }


        /// <summary>
        /// Get the loaded (cached and shared) asset
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object Get(string name) {
            GeneralUtils.LogInfo($"Getting cached asset: {name}");

            assets.TryGetValue(name, out Asset<dynamic> asset);

            if (asset == null) {
                throw new Exception($"Could not get asset: {name}");
            }

            return asset.LoadedAsset;
        }


        /// <summary>
        /// Get the loaded (cached and shared) asset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Get<T>(string name) {
            GeneralUtils.LogInfo($"Getting cached asset: {name}");

            assets.TryGetValue(name, out Asset<dynamic> asset);

            if (asset == null) {
                throw new Exception($"Could not get asset: {name}");
            }

            return (T)asset.LoadedAsset;
        }


        /// <summary>
        /// Get a copy of a Texture2D so that it can be manipulated like changing its pixels
        /// without affecting other assets that share the original asset that don't need to
        /// </summary>
        /// <param name="name"></param>
        /// <param name="graphicsDeviceManager"></param>
        /// <returns></returns>
        public Texture2D GetCopyTexture2D(string name, GraphicsDeviceManager graphicsDeviceManager) {
            GeneralUtils.LogInfo($"Getting copy of Texture2D: {name}");

            Texture2D original = Get<Texture2D>(name);
            Texture2D copy = new Texture2D(graphicsDeviceManager.GraphicsDevice, original.Width, original.Height);
            Color[] data = new Color[copy.Width * copy.Height];
            original.GetData(data);
            copy.SetData(data);

            return copy;
        }


        public int TotalLoaded() => assets.Count;


        public void UnloadAll() {
            GeneralUtils.LogInfo($"Unloading content");
            content.Unload();
        }
    }
}
