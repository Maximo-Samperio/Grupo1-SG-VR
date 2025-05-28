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

    // Update is called once per frame
    void Update()
    {
        /*if(InputData.Instance._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out menuButtonPressed) && menuButtonPressed)
        {
            //Activar animación de jugador entrando a la taberna
            PauseCanvasRef.SetActive(true);
            playerPos = PlayerRef.transform;
            PlayerRef.transform.position = spawnPoint.position;
            PlayerMovementRef.SetActive(false);
        }*/
    }

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
