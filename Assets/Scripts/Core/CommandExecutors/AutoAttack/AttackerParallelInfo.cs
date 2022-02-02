using UnityEngine;

public partial class AutoAttackEvaluator : MonoBehaviour
{
    #region Nested classes

    public struct AttackerParallelInfo
    {
        #region Fields

        public float VisionRadius;
        public ICommand CurrentCommand;

        #endregion

        #region Constructors

        public AttackerParallelInfo(float visionRadius, ICommand currentCommand)
        {
            VisionRadius    = visionRadius;
            CurrentCommand  = currentCommand;
        }

        #endregion
    }

    #endregion
}