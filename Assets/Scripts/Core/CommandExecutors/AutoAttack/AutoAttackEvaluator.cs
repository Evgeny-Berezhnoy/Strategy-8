using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;

public partial class AutoAttackEvaluator : MonoBehaviour
{
    #region Static fields

    public static ConcurrentDictionary<GameObject, AttackerParallelInfo> ATTACKERS_INFO             = new ConcurrentDictionary<GameObject, AttackerParallelInfo>();
    public static ConcurrentDictionary<GameObject, FactionMemberParallelInfo> FACTION_MEMBERS_INFO  = new ConcurrentDictionary<GameObject, FactionMemberParallelInfo>();
    public static Subject<Command> AUTO_ATTACKS_COMMANDS                                            = new Subject<Command>();

    #endregion

    #region Methods

    private void Update()
    {
        Parallel.ForEach(ATTACKERS_INFO, kvp => Evaluate(kvp.Key, kvp.Value));
    }

    private void Evaluate(GameObject targetGameobject, AttackerParallelInfo info)
    {
        var factionInfo = default(FactionMemberParallelInfo);
        
        if (info.CurrentCommand is MoveCommand) return;
        if ((info.CurrentCommand is IAttackCommand)) return;
        if (!FACTION_MEMBERS_INFO.TryGetValue(targetGameobject, out factionInfo)) return;

        foreach (var (otherGameobject, otherFactionInfo) in FACTION_MEMBERS_INFO)
        {
            var distance = Vector3.Distance(factionInfo.Position, otherFactionInfo.Position);

            if (distance > info.VisionRadius) continue;
            if (factionInfo.Faction == otherFactionInfo.Faction) continue;
            
            AUTO_ATTACKS_COMMANDS.OnNext(new Command(targetGameobject, otherGameobject));
        };
    }

    #endregion
}