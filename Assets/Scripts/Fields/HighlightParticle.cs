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

    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule main;

    private void Awake()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        main = particleSystem.main;
        DisableHighlight();
    }

    public void EnableHighlight()
    {
        particleSystem.gameObject.SetActive(true);
    }

    public void DisableHighlight()
    {
        particleSystem.gameObject.SetActive(false);

    }

    public void SetHighlightColor(HighlightColor color)
    {
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
