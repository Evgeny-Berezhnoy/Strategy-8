using UnityEngine;

public partial class AutoAttackEvaluator : MonoBehaviour
{
    #region Nested classes

    public struct Command
    {
        #region Fields

        public GameObject Attacker;
        public GameObject Target;

        #endregion

        #region Constructors

        public Command(GameObject attacker, GameObject target)
        {
            Attacker    = attacker;
            Target      = target;
        }

        #endregion
    }

    #endregion
}