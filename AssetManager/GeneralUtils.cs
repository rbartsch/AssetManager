using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AssetManagerDemo {
    public static class GeneralUtils {
        static string logFile = "log.txt";


        public static void SetLogFile(string file) {
            if (File.Exists(file)) {
                File.Delete(file);
            }
            else {
                logFile = file;
            }
        }


        public static void SetResolution(Core core, int width, int height, bool fullScreen, bool vSync, bool mouseVisible = true) {
            GraphicsDeviceManager graphics = core.Services.GetService<GraphicsDeviceManager>();

            core.IsMouseVisible = mouseVisible;

            // Full screen alt-tab bug in 3.6. 
            // This is borderless window switch for temp workaround https://github.com/MonoGame/MonoGame/issues/5885
            graphics.HardwareModeSwitch = false;

            graphics.SynchronizeWithVerticalRetrace = vSync;
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.IsFullScreen = fullScreen;
            graphics.ApplyChanges();

            if (!graphics.IsFullScreen) {
                core.Window.Position = new Point(
                    (graphics.GraphicsDevice.DisplayMode.Width / 2) - graphics.PreferredBackBufferWidth / 2,
                    (graphics.GraphicsDevice.DisplayMode.Height / 2) - graphics.PreferredBackBufferHeight / 2);
            }
        }


        public static void WriteToFile(string file, string message, bool append = true) {
            using (StreamWriter sw = new StreamWriter(file, append)) {
                sw.WriteLine(message);
                sw.Close();
            }
        }


        public static void LogInfo(string message, bool toFile = false) {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[INFO] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{message}\n");

            if (toFile) {
                WriteToFile(logFile, $"{DateTime.UtcNow.ToString()} [INFO] {message}");
            }
        }


        public static void LogError(string message, bool toFile = false) {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{message}\n");

            if (toFile) {
                WriteToFile(logFile, $"{DateTime.UtcNow.ToString()} [ERROR] {message}");
            }
        }


        public static void LogWarning(string message, bool toFile = false) {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WARN] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{message}\n");

            if (toFile) {
                using (StreamWriter sw = new StreamWriter(logFile, true)) {
                    sw.WriteLine(logFile, $"{DateTime.UtcNow.ToString()} [WARN] {message}");
                    sw.Close();
                }
            }
        }
    }
}