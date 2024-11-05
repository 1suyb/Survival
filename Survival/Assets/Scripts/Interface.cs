using System;

public interface IDamagable
{
	public void TakeDamage(int  damage);
}
public interface IInteractable
{
	public void ShowPrompt();
	public void Interact();
	public void ClosePrompt();
}

public interface IUIUpdater<T>
{
	public event Action<T> OnDataUpdateEvent;
}