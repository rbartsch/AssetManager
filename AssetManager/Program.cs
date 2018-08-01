using System;

namespace AssetManagerDemo {
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            int w = -1;
            int h = -1;
            bool fullScreen = false;
            bool vSync = true;

            if (args.Length > 0) {
                Int32.TryParse(args[0], out w);
                Int32.TryParse(args[1], out h);
                Boolean.TryParse(args[2], out fullScreen);
                Boolean.TryParse(args[3], out vSync);
            }

            using (var game = new Core(w, h, fullScreen, vSync))
                game.Run();
        }
    }
#endif
}
