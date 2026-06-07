using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class BpInputHandler
{
    private const int MaxBoostPointsPerAction = 3;
    private readonly View _view;

    public BpInputHandler(View view)
    {
        _view = view;
    }

    public int GetValidBoostPoints(Traveler actor)
    {
        if (actor.CurrentBp == 0)
            return 0;

        int maxAllowed = Math.Min(actor.CurrentBp, MaxBoostPointsPerAction);
        while (true)
        {
            int bpRequested = _view.GetHowManyBoostPointsToUse();
            if (bpRequested <= maxAllowed)
                return bpRequested;
            _view.ShowInsuficientBp(actor.Name, bpRequested);
        }
    }
}