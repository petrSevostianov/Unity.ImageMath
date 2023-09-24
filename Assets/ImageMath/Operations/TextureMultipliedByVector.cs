using UnityEngine;

namespace ImageMath {

    namespace Operations {

        public class TextureMultipliedByVector : TextureVectorOperation {
            private static Material material = new Material(Shader.Find("ImageMath/Operations/TextureMultipliedByVector"));
            protected override Material Material => material;

        }


    }



}
