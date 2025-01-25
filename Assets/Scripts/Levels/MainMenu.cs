using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Texture2D normalCursor;
    [SerializeField] Texture2D clickCursor;

    [SerializeField] ParticleSystem cursorParticles;
    [SerializeField] AudioSource cursorAudioSource;
    [SerializeField] Vector2 offset;

    [Header("OPTIONS")]
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown screenModeDropdown;
    [SerializeField] TMP_Dropdown fpsDropdown;

    private void Awake()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.ForceSoftware);
            cursorParticles.Play();
            cursorAudioSource.Play();
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorParticles.gameObject.transform.position = new Vector2(mousePos.x, mousePos.y) + offset;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
            cursorParticles.Stop();
            cursorAudioSource.Stop();
        }
    }
    public async void LoadLevel(int index)
    {
        await Task.Delay(150);
        SceneManager.LoadScene(index);
    }
    public async void LoadLevel(string name)
    {
        await Task.Delay(150);
        SceneManager.LoadScene(name);
    }
    public void Apply()
    {
        string[] values = resolutionDropdown.options[resolutionDropdown.value].text.Split('x');
        FullScreenMode mode = Screen.fullScreenMode;

        switch (screenModeDropdown.options[screenModeDropdown.value].text)
        {
            case "Fullscreen":
                mode = FullScreenMode.FullScreenWindow;
                break;
            case "Windowed":
                mode = FullScreenMode.Windowed;
                break;
            case "Borderless Window":
                mode = FullScreenMode.MaximizedWindow;
                break;
            case "Exclusive Fullscreen":
                mode = FullScreenMode.ExclusiveFullScreen;
                break;
            default:
                break;
        }
        Screen.SetResolution(int.Parse(values[0]), int.Parse(values[1]), mode);
        Application.targetFrameRate = int.Parse(fpsDropdown.options[fpsDropdown.value].text);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
