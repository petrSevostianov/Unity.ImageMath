using SharpEXR;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace ImageMath {
    public static class Static {


        private static RenderTextureDescriptor DefaultRenderTextureDescriptor => new RenderTextureDescriptor(0, 0) {
            autoGenerateMips = false,
        };

        public static RenderTextureDescriptor GetDefaultRenderTextureDescriptor(int width, int height = 0) {
            if (height <= 0) height = width;
            var descriptor = DefaultRenderTextureDescriptor;
            descriptor.width = width;
            descriptor.height = height;
            descriptor.colorFormat = RenderTextureFormat.ARGBFloat;
            return descriptor;
        }


        public static void FlipVertical(this float[] a, int rowLength) {
            int height = a.Length / rowLength;
            for (int y0 = 0; y0 < (height / 2); y0++) {
                int y1 = height - y0 - 1;
                for (int x = 0; x < rowLength; x++) {
                    int i0 = y0 * rowLength + x;
                    int i1 = y1 * rowLength + x;
                    var t = a[i0];
                    a[i0] = a[i1];
                    a[i1] = t;
                }
            }
        }


        public static Texture2D LoadEXR(string filePath) {
            var exrFile = EXRFile.FromFile(filePath);
            var part = exrFile.Parts[0];
            part.Open(filePath);

            var width = part.Header.DataWindow.Width;
            var height = part.Header.DataWindow.Height;
            float[] subpixels = part.GetFloats(ChannelConfiguration.RGB, false, GammaEncoding.Linear, true);

            subpixels.FlipVertical(4 * width);

            //return new ReadonlyImageFloat4(width, height, subpixels);
            var result = new Texture2D(width, height, TextureFormat.RGBAFloat, false, true);
            result.SetPixelData(subpixels, 0);
            result.Apply(false);

            return result;

        }
        public static TempRenderTexture NewTempFloat4(Texture texture) {
            return NewTempFloat4(texture.width, texture.height);
        }

        public static TempRenderTexture NewTempFloat4(int width, int height = 0) {
            return new TempRenderTexture(GetDefaultRenderTextureDescriptor(width, height));
        }

        public static RenderTexture NewFloat4(Texture texture) {
            return NewFloat4(texture.width, texture.height);
        }

        public static RenderTexture NewFloat4(int width, int height = 0) {
            return new RenderTexture(GetDefaultRenderTextureDescriptor(width,height));
        }


        public static Color[] GetPixels(this RenderTexture renderTexture) {
            Texture2D texture = renderTexture.ToTexture();
            Color[] pixels = texture.GetPixels();
            UnityEngine.Object.DestroyImmediate(texture);
            return pixels;
        }

        public static Texture2D ToTexture(this RenderTexture renderTexture, bool apply = false) {
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, renderTexture.graphicsFormat, TextureCreationFlags.None);
            renderTexture.ToTexture(texture, apply);
            return texture;
        }

        public static Texture2D ToTexture(this RenderTexture renderTexture, Texture2D texture, bool apply = false) {
            if (texture == null) {
                return renderTexture.ToTexture(apply);
            }
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            if (apply)
                texture.Apply();
            RenderTexture.active = currentRT;
            return texture;
        }


        /*public static void ToTexture(this RenderTexture renderTexture, Texture texture, bool apply = false) {
             if (texture == null)
                 texture = renderTexture.ToTexture(apply);
             else {
                 if (texture is Texture2D texture2D) {
                     renderTexture.ToTexture(texture2D, apply);
                 } else {
                     throw new NotImplementedException();                
                 }
             }                
         }*/


        public static byte[] SaveSimpleFormat(this RenderTexture renderTexture) {
            var pixels = renderTexture.GetPixels();
            var width = renderTexture.width;
            return SaveSimpleFormat(width, pixels);
        }

        public static byte[] SaveSimpleFormat(this Texture2D texture) {
            var pixels = texture.GetPixels();
            var width = texture.width;
            return SaveSimpleFormat(width, pixels);
        }

        public static byte[] SaveSimpleFormat<T>(int width, T[] pixels) {
            var pixelsSize = pixels.Length * 4 *sizeof(float);
            var widthSize = sizeof(int);
            var result = new byte[widthSize + pixelsSize];

            BitConverter.GetBytes(width).CopyTo(result, 0);

            GCHandle handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            try {
                Marshal.Copy(handle.AddrOfPinnedObject(), result, widthSize, pixelsSize);
            }
            finally {
                handle.Free();
            }
            return result;
        }

        public static (int width, Color[] pixels) LoadSimpleFormat(byte[] data) {
            return LoadSimpleFormat<Color>(data);
        }

        public static Texture2D CreateFloat4Texture2D(int width, Color[] pixels, bool apply = true) {
            var height = pixels.Length / width;
            Texture2D texture = new Texture2D(width, height, GraphicsFormat.R32G32B32A32_SFloat, TextureCreationFlags.None );
            texture.SetPixels(pixels);
            if (apply)
                texture.Apply();
            return texture;
        }

        public static (int width, T[] pixels) LoadSimpleFormat<T>(byte[] data) {
            if (data == null || data.Length < sizeof(int)) {
                throw new Exception("Invalid data: The input data is either null or too short to contain the width information.");
            }

            int width = BitConverter.ToInt32(data, 0);

            var pixelSize = 4 * sizeof(float);

            int pixelsDataLength = data.Length - sizeof(int);

            if (pixelsDataLength % pixelSize != 0) {
                throw new Exception("Invalid data: The length of the pixel data is not a multiple of the expected pixel size.");
            }

            int pixelCount = pixelsDataLength / pixelSize;

            if (pixelCount % width != 0) {
                throw new Exception("Invalid data: The pixel count is not a multiple of the provided width.");
            }

            T[] pixels = new T[pixelCount];

            GCHandle handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            try {
                Marshal.Copy(data, sizeof(int), handle.AddrOfPinnedObject(), pixelsDataLength);
            }
            finally {
                handle.Free();
            }

            return (width, pixels);
        }


    }



}
