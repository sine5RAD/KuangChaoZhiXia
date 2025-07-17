using System;
using UnityEngine;
using System.Collections.Generic;

namespace KCGame
{
 
    /// <summary>
    /// 事件参数 
    /// </summary>
    public struct KCGameEvent
    {
        static KCGameEvent e;

        public string EventName;
        public int IntParameter;
        public Vector2 Vector2Parameter;
        public Vector3 Vector3Parameter;
        public bool BoolParameter;
        public string StringParameter;

        public static void Trigger(string eventName, int intParameter = 0, Vector2 vector2Parameter = default(Vector2), Vector3 vector3Parameter = default(Vector3), bool boolParameter = false, string stringParameter = "")
        {
            e.EventName = eventName;
            e.IntParameter = intParameter;
            e.Vector2Parameter = vector2Parameter;
            e.Vector3Parameter = vector3Parameter;
            e.BoolParameter = boolParameter;
            e.StringParameter = stringParameter;
            KCEventManager.TriggerEvent(e);
        }
    }

    /// <summary>
    /// 1 - 声明你的类为该类型的事件实现了 KCEventListener 接口。
    /// 例如：public class GUIManager : Singleton<GUIManager>, KCEventListener<KCGameEvent>
    /// 你可以有多个这样的声明（每种事件类型一个）。
    ///
    /// 2 - 在启用和禁用时，分别开始和停止监听该事件：
    ///void OnEnable ()
    /// {
    /// this.KCEventStartListening<KCGameEvent>();
    /// }
    /// void OnDisable()
    /// {
    /// this.KCEventStopListening<KCGameEvent>();
    /// }
    ///
    /// 3 - 为该事件实现 KCEventListener 接口。例如：
    ///public void OnEvent (MMGameEvent gameEvent)
    /// {
    /// if (gameEvent.EventName == "GameOver")
    /// {
    /// // 执行某些操作
    /// }
    /// }
    /// 这将捕获游戏中任何地方发出的所有 KCGameEvent 类型的事件，如果事件名为 GameOver 则执行某些操作。
    /// </summary>
    [ExecuteAlways]
    public static class KCEventManager
    {
        private static Dictionary<Type, List<KCEventListenerBase>> _subscribersList;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InitializeStatics()
        {
            _subscribersList = new Dictionary<Type, List<KCEventListenerBase>>();
        }

        static KCEventManager()
        {
            _subscribersList = new Dictionary<Type, List<KCEventListenerBase>>();
        }

       
        public static void AddListener<KCEvent>(KCEventListener<KCEvent> listener) where KCEvent : struct
        {
            Type eventType = typeof(KCEvent);

            if (!_subscribersList.ContainsKey(eventType))
            {
                _subscribersList[eventType] = new List<KCEventListenerBase>();
            }

            if (!SubscriptionExists(eventType, listener))
            {
                _subscribersList[eventType].Add(listener);
            }
        }

         
        public static void RemoveListener<MMEvent>(KCEventListener<MMEvent> listener) where MMEvent : struct
        {
            Type eventType = typeof(MMEvent);

            if (!_subscribersList.ContainsKey(eventType))
            {
#if EVENTROUTER_THROWEXCEPTIONS
					throw new ArgumentException( string.Format( "Removing listener \"{0}\", but the event type \"{1}\" isn't registered.", listener, eventType.ToString() ) );
#else
                return;
#endif
            }

            List<KCEventListenerBase> subscriberList = _subscribersList[eventType];

#if EVENTROUTER_THROWEXCEPTIONS
	            bool listenerFound = false;
#endif

            for (int i = subscriberList.Count - 1; i >= 0; i--)
            {
                if (subscriberList[i] == listener)
                {
                    subscriberList.Remove(subscriberList[i]);
#if EVENTROUTER_THROWEXCEPTIONS
					    listenerFound = true;
#endif

                    if (subscriberList.Count == 0)
                    {
                        _subscribersList.Remove(eventType);
                    }

                    return;
                }
            }

#if EVENTROUTER_THROWEXCEPTIONS
		        if( !listenerFound )
		        {
					throw new ArgumentException( string.Format( "Removing listener, but the supplied receiver isn't subscribed to event type \"{0}\".", eventType.ToString() ) );
		        }
#endif
        }

        /// <summary>
        /// Triggers an event. All instances that are subscribed to it will receive it (and will potentially act on it).
        /// </summary>
        /// <param name="newEvent">The event to trigger.</param>
        /// <typeparam name="MMEvent">The 1st type parameter.</typeparam>
        public static void TriggerEvent<MMEvent>(MMEvent newEvent) where MMEvent : struct
        {
            List<KCEventListenerBase> list;
            if (!_subscribersList.TryGetValue(typeof(MMEvent), out list))
#if EVENTROUTER_REQUIRELISTENER
			            throw new ArgumentException( string.Format( "Attempting to send event of type \"{0}\", but no listener for this type has been found. Make sure this.Subscribe<{0}>(EventRouter) has been called, or that all listeners to this event haven't been unsubscribed.", typeof( MMEvent ).ToString() ) );
#else
                return;
#endif

            for (int i = list.Count - 1; i >= 0; i--)
            {
                (list[i] as KCEventListener<MMEvent>).OnKCEvent(newEvent);
            }
        }

        /// <summary>
        /// Checks if there are subscribers for a certain type of events
        /// </summary>
        /// <returns><c>true</c>, if exists was subscriptioned, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        /// <param name="receiver">Receiver.</param>
        private static bool SubscriptionExists(Type type, KCEventListenerBase receiver)
        {
            List<KCEventListenerBase> receivers;

            if (!_subscribersList.TryGetValue(type, out receivers)) return false;

            bool exists = false;

            for (int i = receivers.Count - 1; i >= 0; i--)
            {
                if (receivers[i] == receiver)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }
    }

    /// <summary>
    /// Static class that allows any class to start or stop listening to events
    /// 静态扩展类运行任何类去监听一个事件
    /// </summary>
    public static class EventRegister
    {
        public delegate void Delegate<T>(T eventType);

        public static void KCEventStartListening<EventType>(this KCEventListener<EventType> caller) where EventType : struct
        {
            KCEventManager.AddListener<EventType>(caller);
        }

        public static void KCEventStopListening<EventType>(this KCEventListener<EventType> caller) where EventType : struct
        {
            KCEventManager.RemoveListener<EventType>(caller);
        }
    }

    /// <summary>
    /// Event listener basic interface
    /// </summary>
    public interface KCEventListenerBase { };

    /// <summary>
    /// A public interface you'll need to implement for each type of event you want to listen to.
    /// </summary>
    public interface KCEventListener<T> : KCEventListenerBase
    {
        void OnKCEvent(T eventType);
    }

    public class KCEventListenerWrapper<TOwner, TTarget, TEvent> : KCEventListener<TEvent>, IDisposable
        where TEvent : struct
    {
        private Action<TTarget> _callback;

        private TOwner _owner;
        public KCEventListenerWrapper(TOwner owner, Action<TTarget> callback)
        {
            _owner = owner;
            _callback = callback;
            RegisterCallbacks(true);
        }

        public void Dispose()
        {
            RegisterCallbacks(false);
            _callback = null;
        }

        protected virtual TTarget OnEvent(TEvent eventType) => default;
        public void OnKCEvent(TEvent eventType)
        {
            var item = OnEvent(eventType);
            _callback?.Invoke(item);
        }

        private void RegisterCallbacks(bool b)
        {
            if (b)
            {
                this.KCEventStartListening<TEvent>();
            }
            else
            {
                this.KCEventStopListening<TEvent>();
            }
        }
    }
}