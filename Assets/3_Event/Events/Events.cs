using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Event/String", fileName = "EventString")]
public class EventString : Event<string> { }

[CreateAssetMenu(menuName = "Event/Int", fileName = "EventInt")]
public class EventInt : Event<int> { }

[CreateAssetMenu(menuName = "Event/Float", fileName = "EventFloat")]
public class EventFloat : Event<float> { }

[CreateAssetMenu(menuName = "Event/Vector2", fileName = "EventVector2")]
public class EventVector2 : Event<Vector2> { }

[CreateAssetMenu(menuName = "Event/Vector3", fileName = "EventVector3")]
public class EventVector3 : Event<Vector3> { }

[CreateAssetMenu(menuName = "Event/Gyroscope", fileName = "EventGyroscope")]
public class EventGyroscope : Event<Gyroscope> { }

[CreateAssetMenu(menuName = "Event/GameComleteInfo", fileName = "EventGameComplete")]
public class EventGameComplete : Event<GameCompleteInfo> { }