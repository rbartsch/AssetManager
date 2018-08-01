using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagerDemo {
    public class AssetDefinition {
        public string Name { get; set; }
        public string Path { get; set; }
        public Type Type { get; set; }


        public AssetDefinition(string name, string path, Type type) {
            Name = name;
            Path = path;
            Type = type;
        }


        public AssetDefinition(string path, Type type) {
            Name = path.Split('\\').Last();
            Path = path;
            Type = type;
        }
    }
}