using UnityEngine;
using System.Collections;
using System.IO;

public class HighlightScript : MonoBehaviour {

    public Transform objectToTransform;
    Vector3 scale = new Vector3(1.1f,1.1f,1.1f);
    Vector3 scaleOriginal = new Vector3(1.0f, 1.0f, 1.0f);

    public void Enlarge(BoxCollider2D other)
    {
        objectToTransform.transform.localScale = scale;
    }

    public void ReturnToormalSize(BoxCollider2D other)
    {
        objectToTransform.transform.localScale = scaleOriginal;
    }
}
