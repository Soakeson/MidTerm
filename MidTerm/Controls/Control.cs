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
        [DataMember()]
        public bool keyPressOnly { get; set; }

        public Control(SceneContext sc, ControlContext cc, Keys key, bool keyPressOnly)
        {
            this.sceneContext = sc;
            this.controlContext = cc;
            this.key = key;
            this.keyPressOnly = keyPressOnly;
        }
    }
}
