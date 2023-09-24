using System;
using UnityEngine;

namespace ImageMath {


    public class TempRenderTexture : IDisposable {
        RenderTexture _renderTexture;

        public TempRenderTexture(RenderTextureDescriptor descriptor) {
            _renderTexture = RenderTexture.GetTemporary(descriptor);
            Debug.Log(_renderTexture.name);
        }


        

        /*public void Render(Operation operation) {
            operation.Render(_renderTexture);
        }*/

        


        public static implicit operator RenderTexture(TempRenderTexture _this) {
            return _this._renderTexture;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (_renderTexture != null) {
                if (disposing) {
                }
                RenderTexture.ReleaseTemporary(_renderTexture);
                _renderTexture = null;
            }
        }

        ~TempRenderTexture() {
            Dispose(false);
        }


    }
}