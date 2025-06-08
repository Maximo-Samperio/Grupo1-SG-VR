using UnityEngine;
using UnityEngine.XR;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject PlayerRef;
    [SerializeField] private GameObject PauseCanvasRef;
    [SerializeField] private GameObject PlayerMovementRef;

    private bool menuButtonPressed;
    private Transform playerPos;

    public void EnterPauseMenu()
    {
        PauseCanvasRef.SetActive(true);
        playerPos = PlayerRef.transform;
        PlayerRef.transform.position = spawnPoint.position;
        PlayerMovementRef.SetActive(false);
    }

    public void LeavePause()
    {
        PauseCanvasRef.SetActive(false);
        PlayerRef.transform.position = playerPos.position;
        PlayerMovementRef.SetActive(true);
    }
}
