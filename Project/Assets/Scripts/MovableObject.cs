using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Currently un-used
interface IMovableObject
{
    bool IsMoving { get; set; }
    bool WasMoving { get; set; }

    void OnDrop();
    void OnPickUp();
    void OnMoving();

}
