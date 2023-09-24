using ImageMath;
using static ImageMath.Static;

using ImageMath.Operations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[ExecuteAlways]
public class Test : MonoBehaviour{


    public string FilePath;
    public InspectorButton _SelectFile;
    public void SelectFile() {
        FilePath = EditorUtility.OpenFilePanel("Select EXR", "", "exr");
    }

    public InspectorButton _Open;
    public void Open() {
        
        var texture = LoadEXR(FilePath);

        if (Loaded)
            Loaded.Texture = texture;
    }

    public ImageDisplay Loaded;


    public Vector4 Vector;

    public ImageDisplay A;
    public ImageDisplay B;
    public ImageDisplay C;

    void Update(){

        if (Loaded?.Texture == null)
            return;

        //using var mul = NewTempFloat4(Loaded?.Texture);
        

        if (A.Texture == null)
            A.Texture = NewFloat4(Loaded.Texture);

        new RGBSaturation {
            Vector = Vector,
            Texture = Loaded.Texture
        }.AssignTo(A.Texture as RenderTexture);


        var pixels = (A.Texture as RenderTexture).GetPixels();
        var width = A.Texture.width;


        B.Texture = (A.Texture as RenderTexture).ToTexture(B.Texture as Texture2D,true);

        /*if (B.Texture == null) {
            B.Texture = (A.Texture as RenderTexture).ToTexture();
        } else {
            (A.Texture as RenderTexture).ToTexture(B.Texture as Texture2D);
        }*/
            

        var data = (B.Texture as Texture2D).SaveSimpleFormat();
        var image = LoadSimpleFormat(data);

        C.Texture = CreateFloat4Texture2D(image.width, image.pixels);


        /*new Combination {
            (Loaded.Texture,new Vector4(0,1,0,0)),
            (Loaded.Texture,new Vector4(1,0,0,0)),
            (Loaded.Texture,new Vector4(0,0,1,0)),
            (Loaded.Texture,-0.7f),
        }.RenderTo(Multiply.Texture as RenderTexture);*/


        /*Multiply.Image = Loaded.Image * Multiplier;*/
    }
}
