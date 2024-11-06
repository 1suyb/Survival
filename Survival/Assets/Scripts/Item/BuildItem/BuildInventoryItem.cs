using UnityEngine;

public class BuildInventoryItem
{
	private BuildItemData _data;
	private int _count;
	public bool IsEquiped { get; private set; }

	public BuildItemData Data => _data;
	public int Count => _count;


    private PreviewObject Preview => PlayerManager.Instance.Player.preview;
    private Transform PlayerTransform => PlayerManager.Instance.Player.preview.TfPlayer;


    public BuildInventoryItem() { }
	public BuildInventoryItem(BuildItemData data, int count)
	{
		_data = data;
	
	}
	public void Init(BuildItemData data, int count)
	{
		_data = data;

	}

	public void SetData(BuildItemData data)
	{
		_data = data;
		_count = 1;
	}


	public void Use()
	{

        // 아이템 플레이어 한테 할당 소환 

        //아이템 사용 버튼 클릭 시 활성화.

		//Preview.GoPreview = Object.Instantiate(, PlayerTransform.position + PlayerTransform.forward, Quaternion.identity);
        Preview.GoPreview.GetComponent<GameObject>();
        Preview.Item = Preview.GoPreview.GetComponent<InstallableItem>();

    }
}