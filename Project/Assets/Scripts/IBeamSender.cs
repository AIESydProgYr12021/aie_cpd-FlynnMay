using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBeamSender 
{
    public List<CustomGameObject> SendBeam(Vector3 pos, Vector3 dir);
    public List<IBeamSender> Visited { get; set; }
    public List<CustomGameObject> Found { get; set; }
}
