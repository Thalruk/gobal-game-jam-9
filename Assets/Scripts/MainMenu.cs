using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Texture2D normalCursor;
    [SerializeField] Texture2D clickCursor;

    [SerializeField] ParticleSystem cursorParticles;
    [SerializeField] Vector2 offset;

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
        }
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
