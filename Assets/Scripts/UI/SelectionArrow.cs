using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] buttons;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform arrow;
    private int currentPosition;

    private void Awake()
    {
        arrow = GetComponent<RectTransform>();

        if (buttons == null || buttons.Length == 0)
            Debug.LogError("SelectionArrow: No buttons assigned!");
        else
            Debug.Log($"SelectionArrow: Successfully found {buttons.Length} buttons");

        if (arrow == null)
            Debug.LogError("SelectionArrow: No RectTransform found on this object! Attach this script to an UI Image with an arrow sprite.");
    }

    private void OnEnable()
    {
        // Reset position when menu becomes active
        currentPosition = 0;

        // Small delay to ensure UI layout is updated
        Invoke(nameof(DelayedPositionUpdate), 0.01f);
    }

    private void DelayedPositionUpdate()
    {
        if (arrow != null && buttons != null && buttons.Length > 0 && buttons[0] != null)
        {
            ChangePosition(0);
        }
    }

    private void Update()
    {
        // Only process input if menu is active and everything is valid
        if (!gameObject.activeInHierarchy) return;
        if (buttons == null || buttons.Length == 0) return;
        if (arrow == null) return;

        // Check if any button is interactable (optional safety)
        bool hasValidButton = false;
        foreach (var btn in buttons)
        {
            if (btn != null && btn.gameObject.activeInHierarchy)
            {
                hasValidButton = true;
                break;
            }
        }
        if (!hasValidButton) return;

        // Change position with keyboard
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);

        // Interact with current option
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        if (buttons == null || buttons.Length == 0) return;
        if (arrow == null) return;

        currentPosition += _change;

        if (_change != 0 && SoundManager.instance != null)
            SoundManager.instance.PlaySound(changeSound);

        // Wrap around
        if (currentPosition < 0)
            currentPosition = buttons.Length - 1;
        else if (currentPosition >= buttons.Length)
            currentPosition = 0;

        AssignPosition();
    }

    private void AssignPosition()
    {
        if (arrow == null)
        {
            Debug.LogError("Arrow is null in AssignPosition!");
            return;
        }

        if (buttons == null || buttons.Length == 0)
        {
            Debug.LogError("Buttons array is null or empty in AssignPosition!");
            return;
        }

        if (currentPosition >= buttons.Length)
        {
            Debug.LogError($"currentPosition ({currentPosition}) >= buttons.Length ({buttons.Length})");
            return;
        }

        if (buttons[currentPosition] == null)
        {
            Debug.LogError($"buttons[{currentPosition}] is null!");
            return;
        }

        // Safely assign position
        try
        {
            arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to assign arrow position: {e.Message}");
        }
    }

    private void Interact()
    {
        if (buttons == null || buttons.Length == 0 || currentPosition >= buttons.Length)
            return;

        if (buttons[currentPosition] == null)
            return;

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySound(interactSound);

        Button button = buttons[currentPosition].GetComponent<Button>();
        if (button != null && button.interactable)
        {
            button.onClick.Invoke();
        }
    }
}