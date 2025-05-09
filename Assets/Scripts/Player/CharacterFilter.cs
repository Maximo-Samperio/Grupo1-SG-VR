using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterFilter : MonoBehaviour
{
    [SerializeField] private TunnelingVignetteController _cameraFilter;
    [SerializeField] private Color _bigAreaColor;
    [SerializeField] private Color _smallAreaColor;

    public void ActivateFilter(bool areaSize)
    {
        _cameraFilter.gameObject.SetActive(true);

        if (areaSize)
        {
            _cameraFilter.defaultParameters.vignetteColor = _bigAreaColor;
            _cameraFilter.defaultParameters.vignetteColorBlend = _bigAreaColor;
        }
        else
        {
            _cameraFilter.defaultParameters.vignetteColor = _smallAreaColor;
            _cameraFilter.defaultParameters.vignetteColorBlend = _smallAreaColor;
        }
    }

    public void DeactivateFilter()
    {
        _cameraFilter.gameObject.SetActive(false);
    }
}
