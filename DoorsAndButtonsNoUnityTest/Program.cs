using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;

namespace DoorsAndButtonsNoUnityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Mock level config
            LevelConfig levelConfig = new LevelConfig();
            float3 buttonPosition = new float3(5, 0, 5);
            float3 doorPosition = new float3(10, 0, 10);
            float3 doorClosedPosition = new float3(doorPosition.x + 10f, doorPosition.y, doorPosition.z);
            levelConfig.Actors.Add(new LevelConfig.Actor { ListenInput=true, MovementSpeed=5, Position = float3.zero, View=null });
            levelConfig.Doors.Add(new LevelConfig.DoorConfig { ButtonId = 1, MovingSpeed = 5, OpenPosition = doorPosition, ClosedPosition = doorClosedPosition, View = null });
            levelConfig.Buttons.Add(new LevelConfig.ButtonConfig { ID = 1, Position = buttonPosition, Radius = 1, View = null });

            GameLogicEngine engine = new GameLogicEngine();
            engine.Init(levelConfig);

            for(int i=0; i<10000; i++)
            {
                engine.Update(0.1f);
                switch(i)
                {
                    case 1000:
                        engine.SetInput(buttonPosition); // go to button
                        break;
                    case 2000:
                        engine.SetInput(doorPosition); // go to door
                        break;
                }
            }

            engine.Shootdown();
        }
    }
}
