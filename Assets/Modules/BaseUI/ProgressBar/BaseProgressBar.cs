using UnityEngine;
using UnityEngine.UI;

namespace Modules.BaseUI
{
    public abstract class BaseProgressBar : BaseUIView
    {
        public Image ProgressImage => _progressImage;
        
        [SerializeField] protected Image _progressImage;
        
        public abstract void SetProgress(float progress);
    }
}