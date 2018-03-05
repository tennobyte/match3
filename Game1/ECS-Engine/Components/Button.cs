using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class Button: Component
    {
        public SceneType SceneToOpen { get; set; }

        public Button()
        {

        }

        public Button(SceneType scene)
        {
            SceneToOpen = scene;
        }
    }
}
