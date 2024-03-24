using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Yew
{
    [DataContract(Name = "Control")]
    public class Control
    {
        [DataMember()]
        public ControlContext controlContext { get; private set; }
        [DataMember()]
        public SceneContext sceneContext { get; private set; }
        [DataMember()]
        public Keys key { get; set; }

        public Control(SceneContext sc, ControlContext cc, Keys key)
        {
            this.sceneContext = sc;
            this.controlContext = cc;
            this.key = key;
        }
    }
}
