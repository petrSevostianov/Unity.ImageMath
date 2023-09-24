using UnityEngine;

namespace ImageMath {

    namespace Operations {
        public class RGBSaturation : TextureVectorOperation {
            private static Material material = new Material(Shader.Find("ImageMath/Operations/RGBSaturation"));
            protected override Material Material => material;

        }


    }



}
