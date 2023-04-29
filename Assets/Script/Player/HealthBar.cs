using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerStates playerStates;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = playerStates.currenHealth / 20;
    }
    private void Update()
    {
        currenthealthBar.fillAmount = playerStates.currenHealth / 20;
    }
}
