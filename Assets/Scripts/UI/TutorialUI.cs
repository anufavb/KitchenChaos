using System;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    private void Start()
    {
        PlayerInput.Instance.OnBindingRebind += PlayerInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        UpdateVisual();

        Show();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void PlayerInput_OnBindingRebind(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        keyMoveUpText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.MoveUp);
        keyMoveDownText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.MoveDown);
        keyMoveLeftText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.MoveLeft);
        keyMoveRightText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.MoveRight);
        keyInteractText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Interact);
        keyInteractAlternateText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.InteractAlternate);
        keyPauseText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Pause);
        gamepadInteractText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.GamepadInteract);
        gamepadInteractAlternateText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.GamepadInteractAlternate);
        gamepadPauseText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.GamepadPause);
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
