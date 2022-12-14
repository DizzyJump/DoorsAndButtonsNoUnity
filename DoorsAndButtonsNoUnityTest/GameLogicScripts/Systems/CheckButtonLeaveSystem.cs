using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

// System check are all entities with ability to interact are far enough
// from button and mark that button deactivated by removing Activated component
public class CheckButtonLeaveSystem : IEcsInitSystem, IEcsRunSystem
{
    EcsFilter actorsFilter;
    EcsFilter buttonsFilter;

    EcsPool<Position> positionPool;
    EcsPool<Radius> radiusPool;
    EcsPool<Activated> activatedPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        actorsFilter = world.Filter<CanInteractWithButtons>().Inc<Position>().End();
        buttonsFilter = world.Filter<Button>().Inc<Position>().Inc<Radius>().Inc<Activated>().End();

        positionPool = world.GetPool<Position>();
        radiusPool = world.GetPool<Radius>();
        activatedPool = world.GetPool<Activated>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var button in buttonsFilter)
        {
            bool nobodyInside = true;
            foreach (var actor in actorsFilter)
            {
                var buttonPosition = positionPool.Get(button).Value;
                var actorPosition = positionPool.Get(actor).Value;
                var buttonRadius = radiusPool.Get(button).Value;
                if (math.lengthsq(buttonPosition - actorPosition) <= (buttonRadius * buttonRadius))
                {
                    nobodyInside = false;
                    break;
                }
            }
            if(nobodyInside)
            {
                activatedPool.Del(button);
            }
        } 
    }
}
