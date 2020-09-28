using UnityEngine;

/// <summary>
/// Made by Feiko Joosten
/// 
/// I have based this code on this blogpost. Decided to give it more functionality. http://blogs.unity3d.com/2015/12/23/1k-update-calls/
/// Use this to speed up your performance when you have a lot of update, fixed update and or late update calls in your scene
/// Let the object you want to give increased performance inherit from OverridableMonoBehaviour
/// Replace your void Update() for public override void UpdateMe()
/// Or replace your void FixedUpdate() for public override void FixedUpdateMe()
/// Or replace your void LateUpdate() for public override void LateUpdateMe()
/// OverridableMonoBehaviour will add the object to the update manager
/// UpdateManager will handle all of the update calls
/// </summary>

public class UpdateManager : MonoBehaviour
{
	private static UpdateManager instance;
    [SerializeField]
	private int regularUpdateArrayCount = 0;
    [SerializeField]
	private int fixedUpdateArrayCount = 0;
    [SerializeField]
	private int lateUpdateArrayCount = 0;
	private IUpdatable[] regularArray = new IUpdatable[0];
	private IUpdatable[] fixedArray = new IUpdatable[0];
	private IUpdatable[] lateArray = new IUpdatable[0];

	//public UpdateManager()
	//{
	//	instance = this;
	//}
	public void Awake()
	{
		instance = this;
	}
	public static void AddItem(IUpdatable behaviour)
	{
		instance.AddItemToArray(behaviour);
	}

	public static void RemoveSpecificItem(IUpdatable behaviour)
	{
		instance.RemoveSpecificItemFromArray(behaviour);
	}

	public static void RemoveSpecificItemAndDestroyIt(IUpdatable behaviour)
	{
		instance.RemoveSpecificItemFromArray(behaviour);

		if (behaviour is MonoBehaviour)
		{
			Destroy(((MonoBehaviour)behaviour).gameObject);
		}
	}

	private void AddItemToArray(IUpdatable behaviour)
	{
		if (behaviour.GetType().GetMethod("UpdateMe").DeclaringType != typeof(IUpdatable))
		{
			regularArray = ExtendAndAddItemToArray(regularArray, behaviour);
			regularUpdateArrayCount++;
		}

		if (behaviour.GetType().GetMethod("FixedUpdateMe").DeclaringType != typeof(IUpdatable))
		{
			fixedArray = ExtendAndAddItemToArray(fixedArray, behaviour);
			fixedUpdateArrayCount++;
		}

		if (behaviour.GetType().GetMethod("LateUpdateMe").DeclaringType == typeof(IUpdatable))
			return;

		lateArray = ExtendAndAddItemToArray(lateArray, behaviour);
		lateUpdateArrayCount++;
	}

	public IUpdatable[] ExtendAndAddItemToArray(IUpdatable[] original, IUpdatable itemToAdd)
	{
		int size = original.Length;
		IUpdatable[] finalArray = new IUpdatable[size + 1];
		for (int i = 0; i < size; i++)
		{
			finalArray[i] = original[i];
		}
		finalArray[finalArray.Length - 1] = itemToAdd;
		return finalArray;
	}

	private void RemoveSpecificItemFromArray(IUpdatable behaviour)
	{
		if (CheckIfArrayContainsItem(regularArray, behaviour))
		{
			regularArray = ShrinkAndRemoveItemToArray(regularArray, behaviour);
			regularUpdateArrayCount--;
		}

		if (CheckIfArrayContainsItem(fixedArray, behaviour))
		{
			fixedArray = ShrinkAndRemoveItemToArray(fixedArray, behaviour);
			fixedUpdateArrayCount--;
		}

		if (!CheckIfArrayContainsItem(lateArray, behaviour)) return;

		lateArray = ShrinkAndRemoveItemToArray(lateArray, behaviour);
		lateUpdateArrayCount--;
	}

	public bool CheckIfArrayContainsItem(IUpdatable[] arrayToCheck, IUpdatable objectToCheckFor)
	{
		int size = arrayToCheck.Length;

		for (int i = 0; i < size; i++)
		{
			if (objectToCheckFor == arrayToCheck[i]) return true;
		}

		return false;
	}

	public IUpdatable[] ShrinkAndRemoveItemToArray(IUpdatable[] original, IUpdatable itemToRemove)
	{
		int size = original.Length;
		IUpdatable[] finalArray = new IUpdatable[size - 1];
		for (int i = 0; i < size; i++)
		{
			if (original[i] == itemToRemove) continue;

			finalArray[i] = original[i];
		}
		return finalArray;
	}

	private void Update()
	{
		if (regularUpdateArrayCount == 0) return;

		for (int i = 0; i < regularUpdateArrayCount; i++)
		{
			if (regularArray[i] == null) continue;

			if (regularArray[i].UpdateWhenDisabled == false
#if UNITY_3 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5
				&& (regularArray[i].enabled == false || regularArray[i].gameObject.active == false)
#else
				&& !regularArray[i].IsEnabled
#endif
			) continue;

			regularArray[i].UpdateMe();
		}
	}

	private void FixedUpdate()
	{
		if (fixedUpdateArrayCount == 0) return;

		for (int i = 0; i < fixedUpdateArrayCount; i++)
		{
			if (fixedArray[i] == null) continue;
			if (fixedArray[i].UpdateWhenDisabled == false
#if UNITY_3 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5
				&& (fixedArray[i].enabled == false || fixedArray[i].gameObject.active == false)
#else
				&& !fixedArray[i].IsEnabled
#endif
			) continue;

			fixedArray[i].FixedUpdateMe();
		}
	}

	private void LateUpdate()
	{
		if (lateUpdateArrayCount == 0) return;

		for (int i = 0; i < lateUpdateArrayCount; i++)
		{
			if (lateArray[i] == null) continue;
			if (lateArray[i].UpdateWhenDisabled == false
#if UNITY_3 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5
				&& (lateArray[i].enabled == false || lateArray[i].gameObject.active == false)
#else
				&& !lateArray[i].IsEnabled
#endif
			) continue;

			lateArray[i].LateUpdateMe();
		}
	}
}











