using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImageMath {

    namespace Operations {





        public class Combination: IEnumerable {

            List<(Texture, Vector4)> _pairs = new();
            public Combination() {}

            public void Add((Texture, Vector4) pair) {
                _pairs.Add(pair);
            }
            public void Add((Texture, float) pair) {
                _pairs.Add((pair.Item1, new Vector4(pair.Item2, pair.Item2, pair.Item2, pair.Item2)));
            }

            public void RenderTo(RenderTexture renderTexture) {
                if (_pairs.Count == 0) {
                    new TextureMultipliedByVector {
                        Texture = Texture2D.blackTexture,
                        Vector = Vector4.zero,
                    }.AssignTo(renderTexture);
                } else {
                    new TextureMultipliedByVector {
                        Texture = _pairs[0].Item1,
                        Vector = _pairs[0].Item2,
                    }.AssignTo(renderTexture);
                }

                for (int i = 1; i < _pairs.Count; i++) {
                    new TextureMultipliedByVector {
                        Texture = _pairs[i].Item1,
                        Vector = _pairs[i].Item2,
                    }.AddTo(renderTexture);
                }
            }

            public IEnumerator GetEnumerator() {
                return _pairs.GetEnumerator();
            }
        }
    }



}
