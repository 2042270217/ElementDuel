

using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ElementDuel
{
	public class InputSystem : IGameSystem
	{
		public InputSystem(ElementDuelGame edGame) : base(edGame)
		{
			Initialize();
		}

		public override void Initialize()
		{

		}


		public override void Release()
		{

		}

		public override void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				HandleClick();
			}
		}

		void HandleClick()
		{
			var eventData = new PointerEventData(EventSystem.current);
			eventData.position = Input.mousePosition;

			var results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventData, results);

			foreach (var result in results)
			{
				var receiver = result.gameObject.GetComponent<IClickReceiver>();
				if (receiver == null)
				{
					receiver = result.gameObject.GetComponentInParent<IClickReceiver>(true);
				}

				if (receiver != null)
				{
					receiver.OnClick();
				}
			}
		}

	}
}
