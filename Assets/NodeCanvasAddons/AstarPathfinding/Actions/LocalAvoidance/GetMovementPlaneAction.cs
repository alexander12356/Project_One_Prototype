using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding.RVO;
using Pathfinding.Util;

namespace NodeCanvasAddons.AStarPathfinding.LocalAvoidance
{
    [Category("A* Pathfinding Pro/RVOController")]
    [Name("Get Movement Plane")]
    [Description("Gets the movement plane for the RVOController")]
    [ParadoxNotion.Design.Icon("PathfindingNWaypoint")]
    public class GetMovementPlaneAction : ActionTask<RVOController>
    {
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<IMovementPlane> MovementPlane;

        [GetFromAgent]
        private RVOController _rvoController = default;

        protected override void OnExecute()
        {
            MovementPlane.value = _rvoController.movementPlane;
            EndAction(true);
        }
    }
}