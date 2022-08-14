namespace CodeBase
{
    //TODO: Сделать EventBus для планет
    //TODO: Убрать Awake из планет и прочего
    public class PlanetEvents
    {
        public delegate void PlanetCreated(Planet planet, IGravitable gravitableObject);
        public event PlanetCreated planetCreatedAction;
        
        

    }
}