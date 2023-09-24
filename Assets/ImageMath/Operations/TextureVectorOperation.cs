using UnityEngine;

namespace ImageMath {

    namespace Operations {
        public abstract class TextureVectorOperation : Operation {
            

            public Texture Texture;
            public Vector4 Vector;

            private void RenderTo(RenderTexture renderTexture, int pass) {
                Shader.SetGlobalTexture("ImageMath_T0", Texture);
                Shader.SetGlobalVector("ImageMath_V0", Vector);
                Graphics.Blit(null, renderTexture, Material, pass);
            }


            public void AssignTo(RenderTexture renderTexture) {
                RenderTo(renderTexture, 0);
            }

            public void AddTo(RenderTexture renderTexture) {
                RenderTo(renderTexture, 1);
            }

            public void MultiplyTo(RenderTexture renderTexture) {
                RenderTo(renderTexture, 2);
            }
        }
    }



}
