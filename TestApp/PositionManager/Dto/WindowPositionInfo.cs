using System;
using System.Collections.Generic;
using TestApp.PositionManager.Dto.Base;

namespace TestApp.PositionManager.Dto
{
    [Serializable]
    public class WindowPositionInfo : BasePositionInfo
    {
        public IEnumerable<ShapePositionInfo> ShapePositionInfos { get; set; }
    }
}
