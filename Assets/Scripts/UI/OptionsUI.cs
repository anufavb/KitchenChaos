using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onCloseButtonAction;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There is more than one OptionsUI instance.");
        }
        Instance = this;

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();

            onCloseButtonAction?.Invoke();
        });

        moveUpButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.MoveUp); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.MoveDown); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.MoveLeft); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.MoveRight); });
        interactButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Interact); });
        interactAlternateButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Pause); });
        gamepadInteractButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.GamepadInteract); });
        gamepadInteractAlternateButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.GamepadInteractAlternate); });
        gamepadPauseButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.GamepadPause); });
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        UpdateVisual();

        HidePressToRebindKey();

        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.MoveUp);
        moveDownText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.MoveDown);
        moveLeftText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.MoveLeft);
        moveRightText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.MoveRight);
        interactText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Interact);
        interactAlternateText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.InteractAlternate);
        pauseText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Pause);
        gamepadInteractText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.GamepadInteract);
        gamepadInteractAlternateText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.GamepadInteractAlternate);
        gamepadPauseText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.GamepadPause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(PlayerInput.Binding binding)
    {
        ShowPressToRebindKey();
        PlayerInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
