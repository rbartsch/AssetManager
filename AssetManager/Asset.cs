using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagerDemo {
    public class Asset<T> {
        public AssetDefinition AssetDefinition { get; private set; }
        public T LoadedAsset { get; private set; }


        public Asset(AssetDefinition assetDef) {
            AssetDefinition = assetDef;
        }


        public void Assign(T t) {
            LoadedAsset = t;
        }
    }
}