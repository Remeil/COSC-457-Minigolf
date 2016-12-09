using UnityEngine;

[ExecuteInEditMode]
public class TextureScale : MonoBehaviour
{
    private Material original;
    public float tilingX = .5f;
    public float tilingY = .5f;

	void Start()
    {
        UpdateTiling();
    }

    [ContextMenu("Update Tiling")]
    void UpdateTiling()
    {
        //New Material trick from http://answers.unity3d.com/questions/283271/material-leak-in-editor.html
        var material = new Material(GetComponent<Renderer>().sharedMaterial);

        var scale = transform.localScale;
        var textureScale = new Vector2(scale.x * tilingX, scale.z * tilingY);
        material.mainTextureScale = textureScale;

        GetComponent<Renderer>().sharedMaterial = material;
    }
}
