

using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ElementDuel
{
	public class InputSystem : IGameSystem
	{

		IHoverReceiver currentHover;

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
			var eventData = new PointerEventData(EventSystem.current);
			eventData.position = Input.mousePosition;

			var results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventData, results);

			IHoverReceiver hover = null;
			IClickReceiver click = null;

			foreach (var result in results)
			{
				if (hover == null)
				{
					hover = GetComponent<IHoverReceiver>(result);
				}
				if (click == null)
				{
					click = GetComponent<IClickReceiver>(result);
				}
			}

			if (hover == null)
			{
				currentHover?.OnHoverExit();
				currentHover = null;
			}
			else
			{
				if (hover != currentHover)
				{
					currentHover?.OnHoverExit();
					hover?.OnHoverEnter();
					currentHover = hover;
				}
			}

			if (Input.GetMouseButtonDown(0))
			{
				if (click == null)
				{
					//未点击到主体，即将场景选择状态清空
					m_EDGame.ClearSelection();
				}
				else
				{
					click.OnClick();
				}

			}



		}

		public static T GetComponent<T>(RaycastResult result)
		{
			var receiver = result.gameObject.GetComponent<T>();
			if (receiver == null)
			{
				receiver = result.gameObject.GetComponentInParent<T>(true);
			}
			return receiver;
		}


	}
}
