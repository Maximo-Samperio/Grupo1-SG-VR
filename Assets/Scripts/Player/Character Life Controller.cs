using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterLifeController : MonoBehaviour
{
    [SerializeField] private float maxLife;
    [SerializeField] private TunnelingVignetteController playersVision;

    private float currentLife;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
    }

    public void UpdateLife(float lifeSubstract)
    {
        currentLife -= lifeSubstract;

        playersVision.defaultParameters.apertureSize -= lifeSubstract/100;

        Debug.Log(currentLife);

        if (!StillAllive())
            SceneLoader.Instance.LoadMainMenu();
    }

    private bool StillAllive() => currentLife > 0;
}
