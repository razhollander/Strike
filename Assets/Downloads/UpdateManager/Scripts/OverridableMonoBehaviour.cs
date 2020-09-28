﻿using UnityEngine;

public class OverridableMonoBehaviour : MonoBehaviour, IUpdatable
{
	[SerializeField]
	protected bool updateWhenDisabled = false;
	public bool UpdateWhenDisabled { get { return updateWhenDisabled; } }

	protected virtual void Awake()
	{
		UpdateManager.AddItem(this);
	}
    //protected virtual void OnDisable()
    //{
    //    updateWhenDisabled = false;
    //}
	/// <summary>
	/// If your class uses the Awake function, please use  protected override void Awake() instead.
	/// Also don't forget to call OverridableMonoBehaviour.Awake(); first.
	/// If your class does not use the Awake function, this object will be added to the UpdateManager automatically.
	/// Do not forget to replace your Update function with public override void UpdateMe()
	/// </summary>
	public virtual void UpdateMe() {}

	/// <summary>
	/// If your class uses the Awake function, please use  protected override void Awake() instead.
	/// Also don't forget to call OverridableMonoBehaviour.Awake(); first.
	/// If your class does not use the Awake function, this object will be added to the UpdateManager automatically.
	/// Do not forget to replace your Fixed Update function with public override void FixedUpdateMe()
	/// </summary>
	public virtual void FixedUpdateMe() {}

	/// <summary>
	/// If your class uses the Awake function, please use  protected override void Awake() instead.
	/// Also don't forget to call OverridableMonoBehaviour.Awake(); first.
	/// If your class does not use the Awake function, this object will be added to the UpdateManager automatically.
	/// Do not forget to replace your Late Update function with public override void LateUpdateMe()
	/// </summary>
	public virtual void LateUpdateMe() {}
	public bool IsEnabled
	{
		get { return enabled && gameObject.activeInHierarchy; }
	}
}

public interface IUpdatable
{
    bool UpdateWhenDisabled { get;}
	void UpdateMe();
	void FixedUpdateMe();
	void LateUpdateMe();
	bool IsEnabled { get; }
}
