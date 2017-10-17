using UnityEngine;

static public class MethodExtensionForMonoBehaviourTransform 
{
	/// <summary>
	/// Gets or add a component. Usage example:
	/// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
	/// </summary>
	static public T GetOrAddComponent<T> (this Component aChild) where T: Component 
	{
		T result = aChild.GetComponent<T>();
		if (result == null) {
			result = aChild.gameObject.AddComponent<T>();
		}
		return result;
	}
}