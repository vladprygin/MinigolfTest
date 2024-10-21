using System;

public class BallManager : IManager
{
    private Action _onBallHitEvent;
    
    public event Action OnBallHitEvent
    {
        add => _onBallHitEvent += value;
        remove => _onBallHitEvent -= value;
    }

    public void Init()
    {
    }

    public void Release()
    {
        
    }

    public void BallHitConfirmed()
    {
        _onBallHitEvent?.Invoke();
    }
}
