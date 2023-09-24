using System.Collections.Generic;
using UnityEngine;

namespace ImageMath {
    public abstract class Operation {

        protected abstract Material Material { get; }

        /*protected Dictionary<string, object> parameters = new();
        public TypedAdapter<float> Floats => new (parameters);
        public TypedAdapter<Vector4> Vectors => new (parameters);
        public TypedAdapter<Color> Colors => new (parameters);
        public TypedAdapter<Texture> Textures => new(parameters);


        public abstract void AssignShaderProperties();*/

        //public abstract void RenderTo(RenderTexture renderTexture);
    }



}
