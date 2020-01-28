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

    public void CreateHighlightParticles(HighlightColor color)
    {
        if (instantiatedPrefab == null)
            InstantiateAndSetupPrefab(color);
        else
        {
            if(instantiatedPrefab.GetComponent<ParticleSystem>().main.startColor.color != GetHighlightColor(color))
            {
                DestroyHighlightParticles();
                InstantiateAndSetupPrefab(color);
            }
        }
    }

    private void InstantiateAndSetupPrefab(HighlightColor color)
    {
        instantiatedPrefab = Instantiate(HighlightPrefab, Vector3.zero, Quaternion.identity);
        instantiatedPrefab.transform.parent = transform;
        instantiatedPrefab.transform.localPosition = new Vector3(0, 0, -1);
        instantiatedPrefab.transform.localScale = HighlightPrefab.transform.localScale;

        SetHighlightColor(color);
    }

    public void DestroyHighlightParticles()
    {
        if (instantiatedPrefab != null)
            Object.Destroy(instantiatedPrefab);
    }

    private void SetHighlightColor(HighlightColor color)
    {
        var main = instantiatedPrefab.GetComponent<ParticleSystem>().main;
        main.startColor = GetHighlightColor(color);
    }

    private Color GetHighlightColor(HighlightColor color)
    {
        switch (color)
        {
            case HighlightColor.Default:
                return DefaultHighlightColor;
            case HighlightColor.Scribble:
                return ScribbleHighlightColor;
            case HighlightColor.Called:
                return CalledHighlightColor;
            default:
                return Color.black;
        }
    }

}
