using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.UI.Controls
{
    [ExecuteAlways]
    public class ProgressBar : MonoBehaviour
    {
        public enum ProgressBarMode
        {
            ImageFill
        }

        public float Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = Mathf.Clamp01(value);
                OnFillAmountChanged();
            }
        }

        [Range(0, 1)]
        [SerializeField]
        private float amount;
        [SerializeField]
        private ProgressBarMode mode;
        [SerializeField]
        private Image image;

        private void OnValidate()
        {
            OnFillAmountChanged();
        }

        private void OnFillAmountChanged()
        {
            switch (mode)
            {
                case ProgressBarMode.ImageFill:
                    if (image != null)
                    {
                        image.fillAmount = amount;
                    }
                    break;
            }
        }
    }
}
