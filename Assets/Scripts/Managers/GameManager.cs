using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace VideoPoker {
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		public List<Card> cards;
		public static GameManager Singleton { get; private set; }

		void Awake()
		{
			if (Singleton != null && Singleton != this) Destroy(this);
			else Singleton = this;
		}


		void Start()
		{

		}
		
		void Update()
		{

		}
	}
}
