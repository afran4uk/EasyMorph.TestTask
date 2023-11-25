using System;

namespace TestApp.PositionManager.Dto.Base
{
    [Serializable]
    public abstract class BasePositionInfo
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
    }
}
