using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HighlightColor
{
    Default,
    Scribble,
    Called
}

public class HighlightParticle : MonoBehaviour
{
    public Color DefaultHighlightColor;
    public Color ScribbleHighlightColor;
    public Color CalledHighlightColor;

    public GameObject HighlightPrefab;

    private GameObject instantiatedPrefab;

    private void Awake()
    {

    }

    public void CreateHighlightParticles(HighlightColor color)
    {
        //particleSystem.gameObject.SetActive(true);

        instantiatedPrefab = Instantiate(HighlightPrefab, Vector3.zero, Quaternion.identity);
        instantiatedPrefab.transform.parent = transform;
        instantiatedPrefab.transform.localPosition = new Vector3(0,0,-1);
        instantiatedPrefab.transform.localScale = HighlightPrefab.transform.localScale;

        SetHighlightColor(color);
    }

    public void DestroyHighlightParticles()
    {

        //particleSystem.gameObject.SetActive(false);

        if (instantiatedPrefab)
            Object.Destroy(instantiatedPrefab);

    }

    private void SetHighlightColor(HighlightColor color)
    {
        var main = instantiatedPrefab.GetComponent<ParticleSystem>().main;

        switch (color)
        {
            case HighlightColor.Default:
                main.startColor = DefaultHighlightColor;
                break;
            case HighlightColor.Scribble:
                main.startColor = ScribbleHighlightColor;
                break;
            case HighlightColor.Called:
                main.startColor = CalledHighlightColor;
                break;
            default:
                break;
        }
    }

}
