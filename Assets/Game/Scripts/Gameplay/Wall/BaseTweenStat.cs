using System;
using DG.Tweening;

[Serializable]
public class BaseTweenStat
{
    public float TargetValue;
    public float Duration;

    //Switch to easing in your tweener
    public Ease Easing;
}