using UnityEngine;
using System.Collections;


	public class ZDGRemoveAfterTime : MonoBehaviour 
	{
        [Tooltip("How many seconds to wait before removing this object")]
        public float removeAfterTime = 1;

		void Start() 
		{
			// Remove this object after a delay
			Destroy( gameObject, removeAfterTime);
		}
	}

