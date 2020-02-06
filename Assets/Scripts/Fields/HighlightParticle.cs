using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HighlightType
{
    Default,
    Scribble,
    Called
}

public class HighlightParticle : MonoBehaviour
{
    public GameObject RegularHighlightPrefab;
    public GameObject CalledHighlightPrefab;
    public GameObject ScribbleHighlightPrefab;

    private GameObject instantiatedPrefab;

    public void CreateHighlightParticles(HighlightType highlightType)
    {
        if (instantiatedPrefab == null)
            InstantiateAndSetupPrefab(highlightType);
        else
        {
            if(instantiatedPrefab != GetHighlightType(highlightType))
            {
                DestroyHighlightParticles();
                InstantiateAndSetupPrefab(highlightType);
            }
        }
    }

    private void InstantiateAndSetupPrefab(HighlightType highlightType)
    {
        var highlightObject = GetHighlightType(highlightType);

        instantiatedPrefab = Instantiate(highlightObject, Vector3.zero, Quaternion.identity);
        instantiatedPrefab.transform.SetParent(transform, false);
        instantiatedPrefab.transform.localPosition = new Vector3(0, 0, -1);
        instantiatedPrefab.transform.localScale = highlightObject.transform.localScale;
    }

    public void DestroyHighlightParticles()
    {
        if (instantiatedPrefab != null)
            Object.Destroy(instantiatedPrefab);
    }

    private GameObject GetHighlightType(HighlightType color)
    {
        switch (color)
        {
            case HighlightType.Default:
                return RegularHighlightPrefab;
            case HighlightType.Scribble:
                return ScribbleHighlightPrefab;
            case HighlightType.Called:
                return CalledHighlightPrefab;
            default:
                return RegularHighlightPrefab;
        }
    }

}
