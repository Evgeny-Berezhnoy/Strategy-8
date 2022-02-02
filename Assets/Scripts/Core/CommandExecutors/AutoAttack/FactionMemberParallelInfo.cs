using UnityEngine;

public partial class AutoAttackEvaluator : MonoBehaviour
{
    #region Nested classes

    public struct FactionMemberParallelInfo
    {
        #region Fields

        public Vector3 Position;
        public int Faction;

        #endregion

        #region Constructors

        public FactionMemberParallelInfo(Vector3 position, int faction)
        {
            Position    = position;
            Faction     = faction;
        }

        #endregion
    }

    #endregion
}