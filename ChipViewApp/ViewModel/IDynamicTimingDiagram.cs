using ChipViewApp.Model;

namespace ChipViewApp
{
    public interface IDynamicTimingDiagram
    {
        TimingParametersModel GetPathData(TimingParametersModel timingParam);        
    }
}