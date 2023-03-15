using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScreenCameraShader : MonoBehaviour {
  //public Shader awesomeShader = null;
  public List<Material> renderMaterial;
  int index = 0;
  void OnRenderImage(RenderTexture source, RenderTexture destination) {
    Graphics.Blit(source, destination, renderMaterial[index]);
  }
  private void Update() {
    if (Input.GetKeyDown(KeyCode.Period)) {
      index += 1;
      if (index >= renderMaterial.Count) {
        index = 0;
      }
    }
    if (Input.GetKeyDown(KeyCode.Comma)) {
      index -= 1;
      if (index < 0) {
        index = renderMaterial.Count - 1;
      }
    }
  }
}