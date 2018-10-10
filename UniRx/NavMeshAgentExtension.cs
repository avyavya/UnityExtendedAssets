using System;
using UnityEngine;
using UnityEngine.AI;
using UniRx;


namespace ExtendedAssets.UniRx
{
    public static class NavMeshAgentExtension
    {
        public static IObservable<NavMeshAgent> SetDestinationAsObservable(this NavMeshAgent agent, Vector3 position)
        {
            agent.SetDestination(position);

            var o = agent.ObserveEveryValueChanged(x => x.hasPath)
                .SkipWhile(x => !agent.hasPath)
                .Where(x => !x)
                .First()
                .Select(_ => agent);

            return o;
        }
    }
}
