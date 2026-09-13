using UnityEngine;

public class Shape : MonoBehaviour
{
    // hide size in inspector so the editor can handle warnings
    [HideInInspector][SerializeField] private float size = 1f;
}
