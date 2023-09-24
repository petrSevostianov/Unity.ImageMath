using UnityEngine;

namespace ImageMath {

    namespace Operations {
        public class VectorDividedByTexture : TextureVectorOperation {
            private static Material material = new Material(Shader.Find("ImageMath/Operations/VectorDividedByTexture"));
            protected override Material Material => material;
        }
    }
}
