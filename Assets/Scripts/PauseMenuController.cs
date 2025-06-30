using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject PlayerRef;
    [SerializeField] private GameObject PauseCanvasRef;
    [SerializeField] private ContinuousMoveProviderBase PlayerMovementRef;

    private Vector3 playerPos;

    public void EnterPauseMenu()
    {
        PauseCanvasRef.SetActive(true);
        playerPos = PlayerRef.transform.position;
        PlayerRef.transform.position = spawnPoint.position;
    }

    public void LeavePause()
    {
        PauseCanvasRef.SetActive(false);
        PlayerRef.transform.position = playerPos;
    }
}
