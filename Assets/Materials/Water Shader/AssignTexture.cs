// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class AssignTexture : MonoBehaviour
{
    [SerializeField] private Texture texture;

    private void Start()
    {
        GetComponent<MeshRenderer>().material.mainTexture = this.texture;
    }
}