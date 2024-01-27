using System;

public class Duck : SingletonBehaviour<Duck>
{
    private Action _onDeathAction = null;

    private void OnDestroy()
    {
        _onDeathAction = null;
    }

    public void SetOnDeathAction(Action onDeathAction)
    {
        _onDeathAction += onDeathAction;
    }

    private void Die()
    {
        // TODO: finish this
        _onDeathAction?.Invoke();
    }
}
