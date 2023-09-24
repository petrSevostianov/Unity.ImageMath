using ImageMath;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class ImageDisplay : MonoBehaviour {

    //public float MultiplierPower = 0;

    Texture _texture;
    public MaterialPropertyBlock MaterialPropertyBlock;
    public Texture Texture {
        get {
            return _texture;
        }
        set {
            _texture = value;
            AssignTexture();
        }    
    }


    void Start(){
        
    }

    void AssignTexture() { 
        if (MaterialPropertyBlock == null) {
            MaterialPropertyBlock = new MaterialPropertyBlock();
        }
        var texture = Texture;
        if (!texture) texture = Texture2D.redTexture;


        MaterialPropertyBlock.SetTexture("Image", texture);
        GetComponent<MeshRenderer>().SetPropertyBlock(MaterialPropertyBlock);
    }


    void Update(){
       
        
    }
}
