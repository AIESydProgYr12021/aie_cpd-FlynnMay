using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactors
{
    public interface IBeamInteractor : IInteractor
    {
        void OnBeamEnter(RaycastHit hit, BeamSpawner sender, Vector3 lastSentPos);
        void OnBeamStay(RaycastHit hit, BeamSpawner sender, Vector3 lastSentPos);
        void OnBeamExit();
    }
}
