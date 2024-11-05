using UnityEngine;

public class ResourceObject : MonoBehaviour, IInteractable, IDamagable
{

	public void ClosePrompt()
	{
		throw new System.NotImplementedException();
	}

	public void Interact()
	{
		throw new System.NotImplementedException();
	}

	public void ShowPrompt()
	{
		UIInfoDisplay promptUI = UIManager.Instance.OpenUI<UIInfoDisplay>("ResourcePrompt");
	}

	public void TakePhysicalDamage(int damage)
	{
		throw new System.NotImplementedException();
	}
}
