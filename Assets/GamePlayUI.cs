
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private TextMeshProUGUI _diceText;

    [SerializeField]
    private TextMeshProUGUI _aiDiceText;

    private PlayerWayFinder _player;
    private PlayerWayFinder _ai;

    private void Awake()
    {
        _button.onClick.AddListener(OnDiceButtonClicked);
    }

    public void SetPlayers(PlayerWayFinder player, PlayerWayFinder ai)
    {
        _player = player;
        _ai = ai;
    }

    private void OnDiceButtonClicked()
    {
        StartCoroutine(DiceAnimation());
        _button.interactable = false;
    }

    private IEnumerator DiceAnimation()
    {

        for (int i = 0; i < 15; i++)
        {
            int randomNumber = UnityEngine.Random.Range(1, 7);
            _diceText.text = randomNumber.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        int diceNumber = UnityEngine.Random.Range(1, 7);
        _diceText.text = diceNumber.ToString();
        _player.SetTargetCount(diceNumber, OnPlayerMoveDone);

    }


    private void OnPlayerMoveDone()
    {
        StartCoroutine(AiDiceAnimation());
    }

    private IEnumerator AiDiceAnimation()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 15; i++)
        {
            int randomNumber = UnityEngine.Random.Range(1, 7);
            _aiDiceText.text = randomNumber.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        int diceNumber = UnityEngine.Random.Range(1, 7);
        _aiDiceText.text = diceNumber.ToString();
        _ai.SetTargetCount(diceNumber, () =>
        {
            _button.interactable = true
            ;
        });
    }
}
