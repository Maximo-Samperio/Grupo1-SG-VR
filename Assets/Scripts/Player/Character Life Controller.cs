using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterLifeController : MonoBehaviour
{
    [SerializeField] private float maxLife;
    [SerializeField] private TunnelingVignetteController playersVision;
    [SerializeField] private TunnelingVignetteController _cameraFilter;
    [SerializeField] private Color _bigAreaColor;
    [SerializeField] private Color _smallAreaColor;

    private float currentLife;

    private void Awake()
    {
        currentLife = maxLife;
    }

    public void UpdateLife(float lifeSubstract)
    {
        currentLife -= lifeSubstract; 

        playersVision.defaultParameters.apertureSize = Mathf.Lerp(1f, 0f, lifeSubstract / 100);

        if (!StillAllive())
            SceneLoader.Instance.LoadMainMenu();
    }

    private bool StillAllive() => currentLife > 0;

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
