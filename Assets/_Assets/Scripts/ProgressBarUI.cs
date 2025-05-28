using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private BaseCounter counter;
    [SerializeField] private Image barImage;

    private void Start()
    {
        if (counter is OvenCounter oven)
        {
            oven.OnProgressChanged += OnProgressChanged;
        }
        else if (counter is CuttingCounter cutting)
        {
            cutting.OnProgressChanged += OnProgressChanged;
        }
        else if (counter is PizzaAssemblyCounter pizza)
        {
            pizza.OnProgressChanged += OnProgressChanged;
        }

        barImage.fillAmount = 0f;
        Hide();
    }

    private void OnProgressChanged(object sender, EventArgs e)
    {
        float progress = 0f;

        if (e is OvenCounter.OnProgressChangedEventArgs ovenArgs)
        {
            progress = ovenArgs.progressNormalized;
        }
        else if (e is CuttingCounter.OnProgressChangedEventArgs cutArgs)
        {
            progress = cutArgs.progressNormalized;
        }
        else if (e is PizzaAssemblyCounter.OnProgressChangedEventArgs pizzaArgs)
        {
            progress = pizzaArgs.progressNormalized;
        }

        barImage.fillAmount = progress;

        if (progress <= 0f || progress >= 1f)
            Hide();
        else
            Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
